
using DAL.interfaces;
using DTO.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Algoritm
{
    public class DataModelTruck : DataModelTruckInterface
    {
        //אובייקט המייצג את טבלת המשאיות במסד הנתונים
        ITrucksDAL trucksDAL;
        //אובייקט המייצג את טבלת הגבלות הקיבול של המשאיות במסד הנתונים
        ISizeContainDAL SizeContainsDAL;
        //אובייקט המייצג את טבלת הספקים במסד הנתונים
        IProviderDL providerDAL;
        //רשימת יחידות הסחורה שכל משאית מסוגלת לשאת
        public List<int> capacityTruks { get; set; }
        //רשימת המשאיות
        public List<TrucksDTO> truckSource { get; set; }
        //מספר המשאיות
        public int numOfTruck { get; set; }
        public DataModelTruck(ITrucksDAL trucksDAL, ISizeContainDAL sizeContainsDAL, IProviderDL iproviderDAL)
        {
            this.trucksDAL = trucksDAL;
            this.SizeContainsDAL = sizeContainsDAL;
            this.providerDAL = iproviderDAL;
            numOfTruck = 2;
            capacityTruks = new List<int>();

        }

        // שולפת את נתוני המשאיות הרלוונטיים ממסד הנתונים
        public void InsertDatFromDBByProviderId(string providerId)
        {
            truckSource = trucksDAL.GetAll().FindAll(t => t.ProviderId == providerId);
            numOfTruck = truckSource.Count;
            //כמה סחורה נכנסת בכל ארגז
            int MaxGoodsInCrate = providerDAL.GetById(providerId).MaxGoodsInCrate;
            //כמה ארגזים נכנסים במשאית בהתאם למגבלות המשקל והנפח
            int countOfCrateInTruck;
            ProviderDTO provider = providerDAL.GetById(providerId);

            //כמה סחורה נכנסת בכל משאית
            int maxCapacityWeigth;
            int maxCapacityVolume;
            //הגבלות הגודל של המשאית
            SizeContainDTO sizeContain;
            foreach (TrucksDTO truck in truckSource)
            {
                sizeContain = SizeContainsDAL.GetById(truck.SizeId);
                maxCapacityWeigth = (int)(Math.Floor(sizeContain.WeightSize / provider.CrateWeight) * MaxGoodsInCrate);
                maxCapacityVolume = sizeContain.VolumeSqmr / provider.CrateVolume * MaxGoodsInCrate;
                capacityTruks.Add(Math.Max(maxCapacityWeigth, maxCapacityVolume));
            }
        }
    }
}
