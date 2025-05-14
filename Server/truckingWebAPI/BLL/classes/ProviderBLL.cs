using BLL.googleMaps;
using BLL.interfaces;
using DAL.interfaces;
using DAL.models;
using DTO.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.classes
{
    public class ProviderBLL : IproviderBLL
    {
        IProviderDL providersDL;
        IGoogleMapsPlaceId googleMapsPlaceId;
        public ProviderBLL(IProviderDL p, IGoogleMapsPlaceId googleMapsPlaceId)
        {
            providersDL = p;
            this.googleMapsPlaceId = googleMapsPlaceId;
        }
        public ProviderDTO Add(ProviderDTO p)
        {
            PlacesDTO place;
            if (p.placeGoogleMapsId == null)
            {
                place = googleMapsPlaceId.GetPlaceIdMapsByAddress(p.placeAddress);
                p.placeGoogleMapsId = place.IdMaps;
                p.Lat = place.Lat;

                p.Lang = place.Lang;
            }
            return providersDL.Add(p);
        }

        public List<ProviderDTO> Delete(string id)
        {
            return providersDL.Delete(id);
        }

        public List<ProviderDTO> GetAll()
        {
            return providersDL.GetAll();
        }

        public ProviderDTO GetById(string id)
        {
            return providersDL.GetById(id);
        }

        public ProviderDTO Update(ProviderDTO p)
        {
            PlacesDTO place;
            place = googleMapsPlaceId.GetPlaceIdMapsByAddress(p.placeAddress);
            p.placeGoogleMapsId = place.IdMaps;
            p.Lat = place.Lat;

            p.Lang = place.Lang;
            return providersDL.Update(p);
        }
    }
}
