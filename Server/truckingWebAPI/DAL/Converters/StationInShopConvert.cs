using DAL.models;
using DTO.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Converters
{
    public class StationInShopConvert
    {
        public static StationsInShopDTO ToDto(StationsInShopTable o)
        {
            if (o == null)
                return null;
            StationsInShopDTO od = new StationsInShopDTO
            {
                Id=o.Id,
                LicensePlateTruck=o.LicensePlateTruck,
                ProviderId=o.ProviderId,
                ShopId=o.ShopId,
                StationSerialNumber=o.StationSerialNumber,
                TimeFinish = o.TimeFinish,
                TimeStart=o.TimeStart
            };

            return od;
        }
        public static StationsInShopTable ToTable(StationsInShopDTO o)
        {
            if (o == null)
                return null;
            return new StationsInShopTable
            {
                Id = o.Id,
                LicensePlateTruck = o.LicensePlateTruck,
                ProviderId = o.ProviderId,
                ShopId = o.ShopId,
                StationSerialNumber = o.StationSerialNumber,
                TimeStart=o.TimeStart,
                TimeFinish=o.TimeFinish
            };
        }
        public static List<StationsInShopTable> ToListTable(List<StationsInShopDTO> o)
        {
            if (o == null)
                return null;
            List<StationsInShopTable> list = new List<StationsInShopTable>();
            foreach (var item in o)
            {
                list.Add(ToTable(item));
            }
            return list;
        }
        public static List<StationsInShopDTO> ToListDTO(List<StationsInShopTable> o)
        {
            if (o == null)
                return null;
            List<StationsInShopDTO> list = new List<StationsInShopDTO>();
            foreach (var item in o)
            {
                list.Add(ToDto(item));
            }
            return list;
        }
    }
}
