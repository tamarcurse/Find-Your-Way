using DAL.models;
using DTO.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Converters
{
    public class ShopConvert
    {
        public static ShopsDTO ToDto(ShopsTable o)
        {
            if (o == null)
                return null;
            ShopsDTO od = new ShopsDTO
            {
                ShopId = o.ShopId,
                placeAddress = o.Place.PlaceAddress,
                placeGoogleMapsId = o.Place.IdMaps,
                
                GoodsRequired = o.GoodsRequired,
                // TimeLimitId=o.TimeLimitId,
                ProviderId = o.ProviderId,
                HourDayStart = o.HourDayStart,
                HourDayFinish = o.HourDayFinish,
                //timeFinish = o.HourDayFinish.ToString(),
                //timeStart=o.HourDayStart.ToString()
                ShopName=o.ShopName,
                Lang = Convert.ToDouble(o.Place.Lang),
                Lat = Convert.ToDouble(o.Place.Lat)

            };

            return od;
        }
        public static ShopsTable ToTable(ShopsDTO o)
        {
            if (o == null)
                return null;
            return new ShopsTable
            {
                ShopId = o.ShopId,
                
                GoodsRequired = o.GoodsRequired,
             
                ProviderId = o.ProviderId,
                HourDayStart = o.HourDayStart,
                HourDayFinish = o.HourDayFinish,
                ShopName=o.ShopName
                
            };
        }
        public static List<ShopsTable> ToListTable(List<ShopsDTO> o)
        {
            if (o == null)
                return null;
            List<ShopsTable> list = new List<ShopsTable>();
            foreach (var item in o)
            {
                list.Add(ToTable(item));
            }
            return list;
        }
        public static List<ShopsDTO> ToListDTO(List<ShopsTable> o)
        {
            if (o == null)
                return null;
            List<ShopsDTO> list = new List<ShopsDTO>();
            foreach (var item in o)
            {
                list.Add(ToDto(item));
            }
            return list;
        }

    }
}
