using DAL.models;
using DTO.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Converters
{
    public class ProviderConvert
    {
        public static ProviderDTO ToDto(ProviderTable o)
        {
            if (o == null)
                return null;
            ProviderDTO od = new ProviderDTO
            {
                ProviderId=o.ProviderId,
                ProviderName=o.ProviderName,
                PasswordProvider=o.PasswordProvider,
                FactoryName=o.FactoryName,
                LeavingTime=o.LeavingTime,
                
                placeAddress=o.Place.PlaceAddress,
                placeGoogleMapsId=o.Place.IdMaps,
                CrateVolume=o.CrateVolume,
                CrateWeight=o.CrateWeight,
                MaxGoodsInCrate=o.MaxGoodsInCrate,
                Lang=Convert.ToDouble(o.Place.Lang),
                Lat=Convert.ToDouble(o.Place.Lat)
                
                
            };

            return od;
        }
        public static ProviderTable ToTable(ProviderDTO o)
        {
            if (o == null)
                return null;
            return new ProviderTable
            {
                ProviderId = o.ProviderId,
                ProviderName = o.ProviderName,
                PasswordProvider = o.PasswordProvider,
                FactoryName = o.FactoryName,
                LeavingTime = o.LeavingTime,
                
                CrateVolume = o.CrateVolume,
                CrateWeight = o.CrateWeight,
                MaxGoodsInCrate=o.MaxGoodsInCrate,
                
                
                
            };
        }
        public static List<ProviderTable> ToListTable(List<ProviderDTO> o)
        {
            if (o == null)
                return null;
            List<ProviderTable> list = new List<ProviderTable>();
            foreach (var item in o)
            {
                list.Add(ToTable(item));
            }
            return list;
        }
        public static List<ProviderDTO> ToListDTO(List<ProviderTable> o)
        {
            if (o == null)
                return null;
            List<ProviderDTO> list = new List<ProviderDTO>();
            foreach (var item in o)
            {
                list.Add(ToDto(item));
            }
            return list;
        }

    }
}
