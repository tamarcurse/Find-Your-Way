using DAL.models;
using DTO.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IStationsInShopDAL
    {
        List<StationsInShopDTO> GetAll();
        StationsInShopDTO GetById(int id);
        List<StationsInShopDTO> GetAllbyProviderId(string providerId);
         List<StationsInShopDTO> GetAllbyLicensePlateTruck(string licensePlateTruck);
        List<StationsInShopDTO> Delete(int id);
        List<StationsInShopDTO> Add(StationsInShopDTO p);
        List<StationsInShopDTO> Update(StationsInShopDTO p);
        List<StationsInShopDTO> DeleteAllByProviderId(string providerId);
    }
}
