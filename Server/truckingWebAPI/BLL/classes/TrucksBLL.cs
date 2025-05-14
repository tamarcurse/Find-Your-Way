using System;
using System.Collections.Generic;
using System.Text;
using BLL.interfaces;
using BLL.OR_Tools;
using DAL.interfaces;
using DAL.models;
using DTO.models;

namespace BLL.classes
{
    public class TrucksBLL:ITrucksBLL
    {
        ITrucksDAL truckDAL;
        IFindWay findWay;
        IStationsInShopDAL stationsInShopDAL;
      

        public TrucksBLL(ITrucksDAL truckDAL, IFindWay findWay, IStationsInShopDAL stationsInShopDAL)
        {
            this.truckDAL = truckDAL;
            this.findWay = findWay;
            this.stationsInShopDAL = stationsInShopDAL;
        }

        public List<TrucksDTO> Add(TrucksDTO p)
        {
            List<TrucksDTO> res = truckDAL.Add(p);
            if (res == null)
                return null;
            findWay.CalculateRoute(p.ProviderId);
            return res;
        }

        public List<TrucksDTO> Delete(string id)
        {
            TrucksDTO truck = truckDAL.GetById(id);
            if (truck == null)
                return null;
            stationsInShopDAL.DeleteAllByProviderId(truck.ProviderId);
            List<TrucksDTO> res =truckDAL.Delete(id);
            if (res == null)
                return null;
            findWay.CalculateRoute(truck.ProviderId);
            return res;
        }

        public List<TrucksDTO> GetAll()
        {
            return truckDAL.GetAll();
        }

        public List<TrucksDTO> GetAllByProviderId(string id)
        {
            return truckDAL.GetAllByProviderId(id);
        }
        public TrucksDTO GetById(string id)
        {
            return truckDAL.GetById(id);
        }
        public TrucksDTO GetByDriverId(string id)
        {
            return truckDAL.GetByDriverId(id);
        }
        public List<TrucksDTO> Update(TrucksDTO p)
        {
            List<TrucksDTO> res = truckDAL.Update(p);
            if (res == null)
                return null;
            findWay.CalculateRoute(p.ProviderId);
               return res;
        }
    }
}
