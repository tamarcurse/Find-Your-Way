using DAL.interfaces;
using DAL.models;
using System;
using System.Collections.Generic;
using System.Text;
using BLL.interfaces;
using DAL.classes;
using DTO.models;
using DTO.viewMap;


namespace BLL.classes
{
    public class StationsInShopBLL:IStationsInShopBLL
    {
        IStationsInShopDAL stationsInShopDAL;
        IShopDAL ShopDAL;
        ITrucksDAL TrucksDAL;
        IProviderDL providerDL;
        public StationsInShopBLL(IStationsInShopDAL p, IShopDAL ShopDAL, ITrucksDAL TrucksDAL, IProviderDL providerDL)
        {
            stationsInShopDAL = p;
            this.ShopDAL = ShopDAL;
            this.TrucksDAL = TrucksDAL;
            this.providerDL = providerDL;
        }
        public List<StationsInShopDTO> Add(StationsInShopDTO p)
        {
            return stationsInShopDAL.Add(p);
        }

        public List<StationsInShopDTO> Delete(int id)
        {
            return stationsInShopDAL.Delete(id);
        }

        public List<StationsInShopDTO> GetAll()
        {
            return stationsInShopDAL.GetAll();
        }

        public List<StationsInShopDTO> GetAllByProviderId(string providerId)
        {
            return stationsInShopDAL.GetAllbyProviderId(providerId);
        }
        public List<StationsInShopDTO> GetAllbyLicensePlateTruck(string licensePlateTruck)
        {
            return stationsInShopDAL.GetAllbyLicensePlateTruck(licensePlateTruck);
        }

        public StationsInShopDTO GetById(int id)
        {
            return stationsInShopDAL.GetById(id);
        }

        public List<StationsInShopDTO> Update(StationsInShopDTO p)
        {
            return stationsInShopDAL.Update(p);
        }
        public List<StationsInShopDTO> DeleteAllByProviderId(string providerId)
        {
            return stationsInShopDAL.DeleteAllByProviderId(providerId);
        }
        public List<StationInMap> GetStationInMap(string providerId)
        {
           List<StationInMap> stationInMap= new List<StationInMap> ();
            StationInMap s;
            StationShop shopStat,storeState;
            ShopsDTO shop;
            ProviderDTO providerDTO = providerDL.GetById(providerId);
            storeState = new StationShop();
            storeState.latitude = providerDTO.Lat;
            storeState.longitude = providerDTO.Lang;
            storeState.SerialNumber = -1;
            storeState.shopAddress = providerDTO.placeAddress;
            storeState.shopName = providerDTO.FactoryName;
            storeState.TimeFinish = null;
            storeState.TimeStart = null;
            foreach (var truck in TrucksDAL.GetAllByProviderId(providerId))
            {
                List<StationsInShopDTO> lists = GetAllbyLicensePlateTruck(truck.LicensePlate);
                if (lists.Count > 0)
                {
                    s = new StationInMap();
                    s.truck = truck;
                    s.stations.Add(storeState);
                    foreach (var stat in lists)
                    {
                        shop = ShopDAL.GetById(stat.ShopId);
                        shopStat = new StationShop();
                        shopStat.latitude = shop.Lat;
                        shopStat.longitude = shop.Lang;
                        shopStat.SerialNumber = stat.StationSerialNumber;
                        shopStat.shopAddress = shop.placeAddress;
                        shopStat.shopName = shop.ShopName;
                        shopStat.TimeStart = shop.HourDayStart.ToString();
                        shopStat.TimeFinish = shop.HourDayFinish.ToString();

                        s.stations.Add(shopStat);
                    }
                    stationInMap.Add(s);
                }
            }
            return stationInMap;


        }
        public List<StationOfDriver> GetStationOfDriver(string licensePlateTruck)
        {
            List<StationOfDriver> stationOfDrivers = new List<StationOfDriver>();
            StationOfDriver s;
            ShopsDTO currentShop;
            TrucksDTO truck = TrucksDAL.GetById(licensePlateTruck);
            ProviderDTO provider = providerDL.GetById(truck.ProviderId);
            int sum = 0;
         
            foreach (var stat in stationsInShopDAL.GetAllbyLicensePlateTruck(licensePlateTruck))
            {
                s = new StationOfDriver();
                currentShop = ShopDAL.GetAll().Find(s => s.ShopId == stat.ShopId);
                s.GoodsRequired = currentShop.GoodsRequired;
                s.latitude = currentShop.Lat;
                s.longitude = currentShop.Lang;
                s.SerialNumber = stat.StationSerialNumber;
                s.shopAddress = currentShop.placeAddress;
                s.shopName = currentShop.ShopName;
                s.TimeFinish = stat.TimeFinish.ToString();
                s.TimeStart = stat.TimeStart.ToString();
                stationOfDrivers.Add(s);
                sum += s.GoodsRequired;
            }
            if (stationOfDrivers.Count > 0)
            {
                s = new StationOfDriver();
                s.GoodsRequired = sum;
                s.latitude = provider.Lat;
                s.longitude = provider.Lang;
                s.SerialNumber = -1;
                s.shopAddress = provider.placeAddress;
                s.shopName = provider.FactoryName;
                stationOfDrivers.Add(s);
                stationOfDrivers.Insert(0, s);
            }
            return stationOfDrivers;
        }
    }
}
