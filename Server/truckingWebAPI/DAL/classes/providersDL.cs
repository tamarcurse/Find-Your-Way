using DAL;
using DAL.Converters;
using DAL.interfaces;
using DAL.models;
using DTO.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.classes
{
    public class providersDL : IProviderDL
    {

        FindYourWayContext DB;
        public providersDL(FindYourWayContext DB)
        {
            this.DB = DB;
        }
        public ProviderDTO Add(ProviderDTO p)
        {
            PlacesTable newPlace = DB.PlacesTables.ToList().FirstOrDefault(p1 => p1.IdMaps == p.placeGoogleMapsId);
            if (newPlace == null)
            {
                newPlace = new PlacesTable();
                newPlace.IdMaps = p.placeGoogleMapsId;
                newPlace.PlaceAddress = p.placeAddress;
               
                DB.PlacesTables.Add(newPlace);
                DB.SaveChanges();
            }
            ProviderTable newProvider = ProviderConvert.ToTable(p);
            newProvider.PlaceId = newPlace.PlaceId;
            DB.ProviderTables.Add(newProvider);
            DB.SaveChanges();
            return ProviderConvert.ToDto(newProvider);
        }

        public List<ProviderDTO> Delete(string id)
        {
            ProviderTable p = DB.ProviderTables.FirstOrDefault(x => x.ProviderId == id);
            if (p == null)
                return null;
            DB.ProviderTables.Remove(p);
            DB.SaveChanges();
            return ProviderConvert.ToListDTO(DB.ProviderTables.Include(x => x.Place).ToList());
        }



        public List<ProviderDTO> GetAll()
        {
            return ProviderConvert.ToListDTO(DB.ProviderTables.Include(x => x.Place).ToList());
        }

        public ProviderDTO GetById(string id)
        {
            return ProviderConvert.ToDto(DB.ProviderTables.Include(x => x.Place).FirstOrDefault(x => x.ProviderId == id));
        }



        public ProviderDTO Update(ProviderDTO p)
        {
            PlacesTable place = null;
            ProviderTable provider = DB.ProviderTables.Include(x => x.Place).FirstOrDefault(x => x.ProviderId == p.ProviderId);
            if (provider == null)
                return null;
            provider.ProviderName = p.ProviderName;
            provider.PasswordProvider = p.PasswordProvider;
            provider.LeavingTime = p.LeavingTime;
            provider.CrateVolume = p.CrateVolume;
            provider.CrateWeight = p.CrateWeight;
            provider.MaxGoodsInCrate = p.MaxGoodsInCrate;
            // place = DB.PlacesTables.FirstOrDefault(p1 => p1.IdMaps == p.placeGoogleMapsId);
            foreach (PlacesTable item in DB.PlacesTables)
            {
                if (item.IdMaps == p.placeGoogleMapsId)
                    place = item;
            }
            if (place == null)
            {
                place = new PlacesTable();
                place.IdMaps = p.placeGoogleMapsId;
                place.PlaceAddress = p.placeAddress;
                place.Lang = p.Lang.ToString();
                place.Lat = p.Lat.ToString();
                DB.PlacesTables.Add(place);
                DB.SaveChanges();
            }
           
            



            // place.PlaceAddress = p.placeAddress;
            // place.PlaceName = p.placeNmae;
            provider.PlaceId = place.PlaceId;


            DB.SaveChanges();
            return ProviderConvert.ToDto(provider);
        }



    }
}
