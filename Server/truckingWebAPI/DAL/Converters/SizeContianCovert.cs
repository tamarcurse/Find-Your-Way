using DAL.models;
using DTO.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Converters
{
    public class SizeContianCovert
    {
        public static SizeContainDTO ToDto(SizeContainTable o)
        {
            if (o == null)
                return null;
            SizeContainDTO od = new SizeContainDTO
            {
                SizeContainId=o.SizeContainId,
                 VolumeSqmr=o.VolumeSqmr,
                 WeightSize=o.WeightSize
            };

            return od;
        }
        public static SizeContainTable ToTable(SizeContainDTO o)
        {
            if (o == null)
                return null;
            return new SizeContainTable
            {
                SizeContainId = o.SizeContainId,
                VolumeSqmr = o.VolumeSqmr,
                WeightSize = o.WeightSize
            };
        }
        public static List<SizeContainTable> ToListTable(List<SizeContainDTO> o)
        {
            if (o == null)
                return null;
            List<SizeContainTable> list = new List<SizeContainTable>();
            foreach (var item in o)
            {
                list.Add(ToTable(item));
            }
            return list;
        }
        public static List<SizeContainDTO> ToListDTO(List<SizeContainTable> o)
        {
            if (o == null)
                return null;
            List<SizeContainDTO> list = new List<SizeContainDTO>();
            foreach (var item in o)
            {
                list.Add(ToDto(item));
            }
            return list;
        }
    }
}
