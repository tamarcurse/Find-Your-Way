using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Converters;
using DAL.interfaces;
using DAL.models;
using DTO.models;

namespace DAL.classes
{
    public class SizeContainDAL : ISizeContainDAL
    {
        FindYourWayContext DB;
        public SizeContainDAL(FindYourWayContext DB)
        {
            this.DB = DB;
        }
        public List<SizeContainDTO> Add(SizeContainDTO p)
        {
            DB.SizeContainTables.Add(SizeContianCovert.ToTable(p));
            DB.SaveChanges();
            return SizeContianCovert.ToListDTO(DB.SizeContainTables.ToList());
        }

        public List<SizeContainDTO> Delete(int id)
        {
            SizeContainTable p = DB.SizeContainTables.FirstOrDefault(X => X.SizeContainId == id);
            if (p == null)
                return null;

            DB.SizeContainTables.Remove(p);
            DB.SaveChanges();


            return SizeContianCovert.ToListDTO(DB.SizeContainTables.ToList());
        }



        public List<SizeContainDTO> GetAll()
        {
            return SizeContianCovert.ToListDTO(DB.SizeContainTables.ToList());
        }
      

        public SizeContainDTO GetById(int id)
        {
            return SizeContianCovert.ToDto(DB.SizeContainTables.FirstOrDefault(x => x.SizeContainId == id));
        }



        public List<SizeContainDTO> Update(SizeContainDTO s)
        {
            SizeContainTable size = DB.SizeContainTables.FirstOrDefault(x => x.SizeContainId == s.SizeContainId);
            if (size == null)
            {
                return null;
            }
            size.VolumeSqmr = s.VolumeSqmr;
            size.WeightSize = s.WeightSize;
            DB.SaveChanges();
            return SizeContianCovert.ToListDTO(DB.SizeContainTables.ToList());

        }


    }
}
