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
    public class StationsInShopDAL:IStationsInShopDAL
    {
        FindYourWayContext DB;
        public StationsInShopDAL(FindYourWayContext DB)
        {
            this.DB = DB;
        }
        public List<StationsInShopDTO> Add(StationsInShopDTO p)
        {
            DB.StationsInShopTables.Add(StationInShopConvert.ToTable(p));
            DB.SaveChanges();
            return StationInShopConvert.ToListDTO((DB.StationsInShopTables.ToList()));
        }

        public List<StationsInShopDTO> Delete(int id)
        {
            StationsInShopTable p = DB.StationsInShopTables.FirstOrDefault(X => X.Id == id);
            if (p == null)
                return null;

            DB.StationsInShopTables.Remove(p);
            DB.SaveChanges();


            return StationInShopConvert.ToListDTO(DB.StationsInShopTables.ToList());
        }
        public List<StationsInShopDTO> DeleteAllByProviderId(string providerId)
        {
           List< StationsInShopTable> p = DB.StationsInShopTables.ToList().FindAll(s=>s.ProviderId==providerId);
            foreach (var item in p)
            {
                DB.StationsInShopTables.Remove(item);
            }

           
            DB.SaveChanges();


            return StationInShopConvert.ToListDTO(DB.StationsInShopTables.ToList());
        }


        public List<StationsInShopDTO> GetAll()
        {
            return StationInShopConvert.ToListDTO(DB.StationsInShopTables.ToList());
        }

        public List<StationsInShopDTO> GetAllbyProviderId(string providerId)
        {
            return StationInShopConvert.ToListDTO(DB.StationsInShopTables.ToList().FindAll(s=>s.ProviderId==providerId));

        }
        public List<StationsInShopDTO> GetAllbyLicensePlateTruck(string licensePlateTruck)
        {
            return StationInShopConvert.ToListDTO(DB.StationsInShopTables.ToList().FindAll(s => s.LicensePlateTruck == licensePlateTruck));

        }

        public StationsInShopDTO GetById(int id)
        {
            return StationInShopConvert.ToDto((DB.StationsInShopTables.FirstOrDefault(x => x.Id == id)));
        }



        public List<StationsInShopDTO> Update(StationsInShopDTO s)
        {
            StationsInShopTable station = DB.StationsInShopTables.FirstOrDefault(x => x.Id == s.Id);
            if (station == null)
            {
                throw new Exception("NOT_FOUND");
            }
            station.LicensePlateTruck = s.LicensePlateTruck;
            station.ProviderId = station.ProviderId;
            station.StationSerialNumber = s.StationSerialNumber;
            station.TimeStart = s.TimeStart;
            station.TimeFinish = s.TimeFinish;
            
            DB.SaveChanges();
            return StationInShopConvert.ToListDTO(DB.StationsInShopTables.ToList());

        }

    }
}
