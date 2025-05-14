using DAL.models;
using DTO.models;
using DTO.viewMap;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface IStationsInShopBLL
    {
        List<StationsInShopDTO> GetAll();
        List<StationsInShopDTO> GetAllByProviderId(string providerId);
         List<StationsInShopDTO> GetAllbyLicensePlateTruck(string licensePlateTruck);
        StationsInShopDTO GetById(int id);
        List<StationsInShopDTO> Delete(int id);
        List<StationsInShopDTO> Add(StationsInShopDTO p);
        List<StationsInShopDTO> Update(StationsInShopDTO p);
        List<StationsInShopDTO> DeleteAllByProviderId(string providerId);
         List<StationInMap> GetStationInMap(string providerId);
         List<StationOfDriver> GetStationOfDriver(string licensePlateTruck);
    }
}
