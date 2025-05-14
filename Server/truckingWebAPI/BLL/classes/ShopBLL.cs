using BLL.googleMaps;
using BLL.interfaces;
using BLL.OR_Tools;
using DAL.interfaces;

using DTO.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.classes
{
    public class ShopBLL:IShopBLL
    {
        IShopDAL shopDAL;
        IGoogleMapsPlaceId googleMapsPlaceId;
        IFindWay findWay;
        IStationsInShopDAL stationsInShopDAL;
    

        public ShopBLL(IShopDAL shopDAL, IGoogleMapsPlaceId googleMapsPlaceId, IFindWay findWay, IStationsInShopDAL stationsInShopDAL)
        {
            this.shopDAL = shopDAL;
            this.googleMapsPlaceId = googleMapsPlaceId;
            this.findWay = findWay;
            this.stationsInShopDAL = stationsInShopDAL;
        }

        public List<ShopsDTO> Add(ShopsDTO p)
        {
            List<ShopsDTO> res;
            PlacesDTO place;
            if (p.placeGoogleMapsId == null)
            {
                place = googleMapsPlaceId.GetPlaceIdMapsByAddress(p.placeAddress);
                p.placeGoogleMapsId = place.IdMaps;
                p.Lat = place.Lat;

                p.Lang = place.Lang;
            }
             res = shopDAL.Add(p);
            if (res == null)
                return null;
         
            findWay.CalculateRoute(p.ProviderId);
            return res;
        }

        public List<ShopsDTO> Delete(int id)
        {
            ShopsDTO shop = shopDAL.GetById(id);
            if (shop == null)
                return null;
            stationsInShopDAL.DeleteAllByProviderId(shop.ProviderId);
            List<ShopsDTO> res =  shopDAL.Delete(id);
            if (res == null)
                return null;
            
            findWay.CalculateRoute(shop.ProviderId);
            return res;
        }

        public List<ShopsDTO> GetAll()
        {
            return shopDAL.GetAll();
        }

        public ShopsDTO GetById(int id)
        {
            return shopDAL.GetById(id);
        }
        public List<ShopsDTO> GetByProviderId(string id)
        {
            return shopDAL.GetByProviderId(id);
        }
        public List<ShopsDTO> Update(ShopsDTO p)
        {
            PlacesDTO place;
            place = googleMapsPlaceId.GetPlaceIdMapsByAddress(p.placeAddress);
            p.placeGoogleMapsId = place.IdMaps;
            p.Lat = place.Lat;

            p.Lang =place.Lang;
            List<ShopsDTO> res =  shopDAL.Update(p);
            if (res == null)
                return null;
            findWay.CalculateRoute(p.ProviderId);
            return res;
        }
    }
}
