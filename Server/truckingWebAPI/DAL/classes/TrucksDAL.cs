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
    public class TrucksDAL:ITrucksDAL
    {
        FindYourWayContext DB;
        public TrucksDAL(FindYourWayContext DB)
        {
            this.DB = DB;
        }
        public List<TrucksDTO> Add(TrucksDTO p)
        {
            DB.TrucksTables.Add(truckConvert.ToTable(p));
            DB.SaveChanges();
            return truckConvert.ToListDTO((DB.TrucksTables.ToList().FindAll(x => x.ProviderId == p.ProviderId)));
        }

        public List<TrucksDTO> Delete(string id)
        {
            TrucksTable p = DB.TrucksTables.FirstOrDefault(X => X.LicensePlate == id);
            if (p == null)
                return null;
            //foreach (var item in DB.StationsInShopTables)
            //{
            //    //במקרה של מחיקת משאית יש למחוק גם את רשימת התחנות של אותו ספק
            //    if (item.ProviderId == p.ProviderId)
            //        DB.StationsInShopTables.Remove(item);
            //}

            
            DB.TrucksTables.Remove(p);
            DB.SaveChanges();


            return truckConvert.ToListDTO(DB.TrucksTables.ToList().FindAll(x => x.ProviderId == p.ProviderId));
        }



        public List<TrucksDTO> GetAll()
        {
            return truckConvert.ToListDTO(DB.TrucksTables.ToList());
        }
        public List<TrucksDTO> GetAllByProviderId(string providerId)
        {
            return truckConvert.ToListDTO(DB.TrucksTables.ToList().FindAll(t=>t.ProviderId==providerId));
        }
        public TrucksDTO GetById(string id)
        {
            return truckConvert.ToDto(DB.TrucksTables.FirstOrDefault(x => x.LicensePlate == id));
        }
        public TrucksDTO GetByDriverId(string id)
        {
            return truckConvert.ToDto(DB.TrucksTables.FirstOrDefault(x => x.IdOfDriver == id));
        }


        public List<TrucksDTO> Update(TrucksDTO t)
        {
            TrucksTable truck = DB.TrucksTables.FirstOrDefault(x => x.LicensePlate == t.LicensePlate);
            if (truck == null)
            {
                return null;
            }
            truck.IdOfDriver = t.IdOfDriver;
            truck.NameOfDriver = t.NameOfDriver;
            truck.ProviderId = t.ProviderId;
           
            truck.SizeId = t.SizeId;
            
            DB.SaveChanges();
            return truckConvert.ToListDTO(DB.TrucksTables.ToList().FindAll(x => x.ProviderId == t.ProviderId));

        }


    }
}
