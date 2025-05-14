using DAL.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.interfaces;
using DTO.models;
using DAL.Converters;
using Microsoft.EntityFrameworkCore;

namespace DAL.classes
{
    public class ShopDAL:IShopDAL
    {
        FindYourWayContext DB;
        public ShopDAL(FindYourWayContext DB)
        {
            this.DB = DB;
        }
        public List<ShopsDTO> Add(ShopsDTO p)
        {
            PlacesTable newPlace = DB.PlacesTables.ToList().FirstOrDefault(p1 => p1.IdMaps == p.placeGoogleMapsId);
            if (newPlace == null)
            {
                newPlace = new PlacesTable();
                newPlace.IdMaps = p.placeGoogleMapsId;
                newPlace.PlaceAddress = p.placeAddress;
                newPlace.Lang = p.Lang.ToString();
                newPlace.Lat = p.Lat.ToString();
                DB.PlacesTables.Add(newPlace);
                DB.SaveChanges();
            }
            ShopsTable newShop = ShopConvert.ToTable(p);
            newShop.PlaceId = newPlace.PlaceId;
            DB.ShopsTables.Add(newShop);
            
            DB.SaveChanges();
            return ShopConvert.ToListDTO(DB.ShopsTables.Include(x => x.Place).ToList().FindAll(x=>x.ProviderId==p.ProviderId));
        }

        public List<ShopsDTO> Delete(int id)
        {
            
            ShopsTable p = DB.ShopsTables.FirstOrDefault(X => X.ShopId == id);
            if (p == null)
                return null;
            foreach (var item in DB.StationsInShopTables)
            {
                //במקרה של מחיקת חנות יש למחוק גם את רשימת התחנות של אותו ספק
                if (item.ProviderId == p.ProviderId)
                    DB.StationsInShopTables.Remove(item);
            }
            
            DB.ShopsTables.Remove(p);
            DB.SaveChanges();


            return ShopConvert.ToListDTO(DB.ShopsTables.Include(x => x.Place).ToList().FindAll(x => x.ProviderId == p.ProviderId));
        }



        public List<ShopsDTO> GetAll()
        {
            return ShopConvert.ToListDTO(DB.ShopsTables.Include(x => x.Place).ToList());
        }

        public ShopsDTO GetById(int id)
        {
            return ShopConvert.ToDto(DB.ShopsTables.Include(x => x.Place).FirstOrDefault(x => x.ShopId == id));
        }
        public List<ShopsDTO> GetByProviderId(string id)
        {
            return ShopConvert.ToListDTO(DB.ShopsTables.Include(x => x.Place).ToList().FindAll(s=>s.ProviderId==id));
        }


        public List<ShopsDTO> Update(ShopsDTO s)
        {
            ShopsTable shop = DB.ShopsTables.Include(x => x.Place).FirstOrDefault(x => x.ShopId == s.ShopId);
            if (shop == null)
            {
                return null;
            }
            ShopsDTO shopDto = ShopConvert.ToDto(shop);
            PlacesTable place = DB.PlacesTables.FirstOrDefault(p => p.PlaceId == shop.PlaceId);
            if (s.placeGoogleMapsId != place.IdMaps)
            {
                place = new PlacesTable();
                DB.PlacesTables.Add(place);
              
            }
            place.PlaceAddress = s.placeAddress;
            place.IdMaps = s.placeGoogleMapsId;
            shop.ProviderId = s.ProviderId;
            shop.GoodsRequired = s.GoodsRequired;
            shop.HourDayFinish = s.HourDayFinish;
            shop.HourDayStart = s.HourDayStart;
            shop.ShopName = s.ShopName;
            //shop.TimeLimitId = s.TimeLimitId;
            place.Lang = s.Lang.ToString();
            place.Lat = s.Lat.ToString();
    
            DB.SaveChanges();
            return ShopConvert.ToListDTO(DB.ShopsTables.Include(x => x.Place).ToList().FindAll(x => x.ProviderId == s.ProviderId));

        }


    }
}

