using DAL.models;
using DTO.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Converters
{
    public class PlaceConvert
    {
        public static PlacesDTO ToDto(PlacesTable o)
        {
            if (o == null)
                return null;
            PlacesDTO od = new PlacesDTO
            {
                PlaceId = o.PlaceId,

                PlaceAddress = o.PlaceAddress,
                IdMaps = o.IdMaps,
                Lang = Convert.ToDouble(o.Lang),
                Lat = Convert.ToDouble(o.Lat)
            };

            return od;
        }
        public static PlacesTable ToTable(PlacesDTO o)
        {
            if (o == null)
                return null;
            return new PlacesTable
            {
                //PlaceId = o.PlaceId,

                PlaceAddress = o.PlaceAddress,
                IdMaps = o.IdMaps,
                Lat = o.Lat.ToString(),
                Lang = o.Lang.ToString()


            };
        }
        public static List<PlacesTable> ToListTable(List<PlacesDTO> o)
        {
            if (o == null)
                return null;
            List<PlacesTable> list = new List<PlacesTable>();
            foreach (var item in o)
            {
                list.Add(ToTable(item));
            }
            return list;
        }
        public static List<PlacesDTO> ToListDTO(List<PlacesTable> o)
        {
            if (o == null)
                return null;
            List<PlacesDTO> list = new List<PlacesDTO>();
            foreach (var item in o)
            {
                list.Add(ToDto(item));
            }
            return list;
        }

    }
}
