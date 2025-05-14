using BLL.googleMaps;
using DAL.interfaces;
using DTO.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Algoritm
{
    public class DataModelVertex : DataModelVertexInterface
    { //אובייקט המייצג את טבלת החנויות במסד הנתונים
        IShopDAL shopDAL;
        //אובייקט המייצג את טבלת הספקים במסד הנתונים
        IProviderDL providerDAL;
        //אובייקט המייצר מטריצת מרחקים
        DistanceMatrixInterface timeAndDistanceMatrix;

        //מטריצת המרחקים
        public List<List<int>> distanceMatrix { get; set; }

        //רשימת המייצגת את יחידות הסחורה שכל חנות הזמינה
        public List<int> demondsShop { get; set; }

        //מספר החנויות
        public int numOfShops { get; set; }
        public DataModelVertex(IShopDAL shopDAL, IProviderDL providerDAL, DistanceMatrixInterface distanceMatrix)
        {
            this.shopDAL = shopDAL;
            this.providerDAL = providerDAL;
            this.timeAndDistanceMatrix = distanceMatrix;

            demondsShop = new List<int>();
        }
        //שולפת את נתוני החנויות ממסד הנתונים
        public void InsertFormDBByProvider(string providerId)
        {
            List<ShopsDTO> shopsDTO = shopDAL.GetAll().FindAll(s => s.ProviderId == providerId);
            foreach (ShopsDTO shop in shopsDTO)
            {
                demondsShop.Add(shop.GoodsRequired);

            }
            numOfShops = shopsDTO.Count;
            //מערך המייצג את כתובות החנויות
            List<string> x = (from shop in shopsDTO select shop.placeGoogleMapsId).ToList();
            //יצירת מטרית מרחקים
            
            timeAndDistanceMatrix.CreateTimeAndDistanceMatrix(providerDAL.GetById(providerId).placeGoogleMapsId, x);
            distanceMatrix = timeAndDistanceMatrix.GetTimeAndDistanceMatrix().distanceMatrix;
        }
    }
}

