using DAL.models;
using DTO.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Converters
{
    public class truckConvert
    {
        public static TrucksDTO ToDto(TrucksTable o)
        {
            if (o == null)
                return null;
            TrucksDTO od = new TrucksDTO
            {
                LicensePlate = o.LicensePlate,
                SizeId = o.SizeId,
                NameOfDriver = o.NameOfDriver,
                IdOfDriver = o.IdOfDriver,
                ProviderId = o.ProviderId
            };

            return od;
        }
        public static TrucksTable ToTable(TrucksDTO o)
        {
            if (o == null)
                return null;
            return new TrucksTable
            {
                LicensePlate = o.LicensePlate,
                SizeId = o.SizeId,
                NameOfDriver = o.NameOfDriver,
                IdOfDriver = o.IdOfDriver,
                ProviderId = o.ProviderId
            };
        }
        public static List<TrucksTable> ToListTable(List<TrucksDTO> o)
        {
            if (o == null)
                return null;
            List<TrucksTable> list = new List<TrucksTable>();
            foreach (var item in o)
            {
                list.Add(ToTable(item));
            }
            return list;
        }
        public static List<TrucksDTO> ToListDTO(List<TrucksTable> o)
        {
            if (o == null)
                return null;
            List<TrucksDTO> list = new List<TrucksDTO>();
            foreach (var item in o)
            {
                list.Add(ToDto(item));
            }
            return list;
        }
    }
}
