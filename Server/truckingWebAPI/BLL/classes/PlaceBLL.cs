using DAL.interfaces;
using DAL.models;
using System;
using System.Collections.Generic;
using System.Text;
using BLL.interfaces;
using DTO.models;
using BLL.googleMaps;

namespace BLL.classes
{
    public class PlaceBLL:IPlaceBLL
    {
        IPlaceDAL placeDAL;
        IGoogleMapsPlaceId GoogleMapsPlaceId;
        public PlaceBLL(IPlaceDAL p,IGoogleMapsPlaceId googleMaps)
        {
            placeDAL = p;
            GoogleMapsPlaceId = googleMaps;
        }
        public List<PlacesDTO> Add(PlacesDTO p)
        {
            PlacesDTO place;
            place = GoogleMapsPlaceId.GetPlaceIdMapsByAddress(p.PlaceAddress);
            p.IdMaps = place.IdMaps;

            p.Lat = place.Lat;
            p.Lang =place.Lang;
            return placeDAL.Add(p);
        }

        public List<PlacesDTO> Delete(int id)
        {
            return placeDAL.Delete(id);
        }

        public List<PlacesDTO> GetAll()
        {
            return placeDAL.GetAll();
        }

        public PlacesDTO GetById(int id)
        {
            return placeDAL.GetById(id);
        }


        public List<PlacesDTO> Update(PlacesDTO p)
        {
            PlacesDTO place;
            place = GoogleMapsPlaceId.GetPlaceIdMapsByAddress(p.PlaceAddress);
            p.IdMaps = place.IdMaps;
            p.Lat = place.Lat;

            p.Lang = place.Lang;
            return placeDAL.Update(p);
        }
    }
}
