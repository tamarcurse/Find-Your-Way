using DAL.models;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.interfaces;
using System.Linq;
using DTO.models;
using DAL.Converters;

namespace DAL.classes
{
    public class PlaceDAL:IPlaceDAL
    {
        FindYourWayContext DB;
        public PlaceDAL(FindYourWayContext DB)
        {
            this.DB = DB;
        }
        public List<PlacesDTO> Add(PlacesDTO p)
        {
            PlacesTable placesTable = PlaceConvert.ToTable(p);
            DB.PlacesTables.Add(placesTable);
            DB.SaveChanges();
            return PlaceConvert.ToListDTO(DB.PlacesTables.ToList());
        }

        public List<PlacesDTO> Delete(int id)
        {
            PlacesTable p = DB.PlacesTables.FirstOrDefault(X => X.PlaceId == id);
            if (p == null)
                return null;

            DB.PlacesTables.Remove(p);
            DB.SaveChanges();


            return PlaceConvert.ToListDTO(DB.PlacesTables.ToList());
        }



        public List<PlacesDTO> GetAll()
        {
            return PlaceConvert.ToListDTO(DB.PlacesTables.ToList());
        }

        public PlacesDTO GetById(int id)
        {
            PlacesTable p = DB.PlacesTables.FirstOrDefault(x => x.PlaceId == id);
            if (p == null)
                return null;
            return PlaceConvert.ToDto(p);
        }



        public List<PlacesDTO> Update(PlacesDTO s)
        {
            PlacesTable places = DB.PlacesTables.FirstOrDefault(x => x.PlaceId == s.PlaceId);
            if (places == null)
            {
                return null;
            }
            places.PlaceAddress = s.PlaceAddress;
        
            places.IdMaps = s.IdMaps;
            places.Lang = s.Lang.ToString();
            places.Lat = s.Lat.ToString();
            DB.SaveChanges();
            return PlaceConvert.ToListDTO(DB.PlacesTables.ToList());

        }


    }
}

