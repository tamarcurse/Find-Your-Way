using BLL.googleMaps;
using BLL.interfaces;
using DAL.interfaces;
using DTO.models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.OR_Tools
{
    public class DataModel:IDataModel
    {
        //מטריצת מרחקים
        
        public long[,] DistanceMatrix { get; set; }
        //מטריצת זמנים
        public long[,] TimeMatrix { get; set; }
        //מטריצת חלונות זמן
        public long[,] TimeWindows { get; set; }
        //מספר המשאיות המשתתפות בהובלה
        public int VehicleNumber { get; set; }
        //אינדקס המחסן במטריצות
        public int Depot { get; set; }
        //מערך הדרישות של החנויות
        public long[] Demands { get; set; }
        public List<TrucksDTO> truckSource { get; set; }
        public List<ShopsDTO> shopsDTO { get; set; }
        //מערך הקיבולות של המשאיות
        public long[] VehicleCapacities { get; set; }
        private ProviderDTO provider;
        private IShopDAL shopDAL;
        private DistanceMatrixInterface timeAndDistanceMatrix;
        private IProviderDL providerDAL;
     
        private ITrucksDAL trucksDAL;
        private ISizeContainDAL sizeContainsDAL;

        public DataModel(IShopDAL shopDAL, DistanceMatrixInterface timeAndDistanceMatrix, IProviderDL providerDAL, ITrucksDAL trucksDAL, ISizeContainDAL sizeContainDAL)
        {
            this.shopDAL = shopDAL;
            this.timeAndDistanceMatrix = timeAndDistanceMatrix;
            this.providerDAL = providerDAL;
            this.trucksDAL = trucksDAL;
            this.sizeContainsDAL = sizeContainDAL;
        }

        public void Init(string providerId)
        {
            provider = providerDAL.GetById(providerId);
            if (provider == null)
                return;
            Depot = 0;
            InitShops();
            InitMatrix();
            InitTimeWindows();
            InitTrucks();
        }
        private void InitShops()
        {
            //מילוי מערך הדרישות של החנויות
           shopsDTO = shopDAL.GetAll().FindAll(s => s.ProviderId == provider.ProviderId);
            Demands = new long[shopsDTO.Count+1];
            Demands[0] = 0;
            int i = 1;
            foreach (ShopsDTO shop in shopsDTO)
            {
                Demands[i++] = shop.GoodsRequired;
            }
        }
        private void InitMatrix()
        {
            //מילוי מטריצת מרחקים ומטריצת זמנים
            //List<ShopsDTO> shopsDTO = shopBLL.GetAll().FindAll(s => s.ProviderId == provider.ProviderId);
            List<string> x = (from shop in shopsDTO select shop.placeGoogleMapsId).ToList();//רשימת הכתובות של החניות
            timeAndDistanceMatrix.CreateTimeAndDistanceMatrix(provider.placeGoogleMapsId, x);//google mpasיצירת המטריצות דרך   
            //list<list<int>> מחזיר  timeAndDistanceMatrix.GetTimeAndDistanceMatrix().durationMatrix האוביקט  
            //or-tools כדי שיתאים לספרייה  long[,] המטרה היא להמיר אותו ל- 
            List<List<int>> timeMatrixInt = timeAndDistanceMatrix.GetTimeAndDistanceMatrix().durationMatrix;
            TimeMatrix = new long[timeMatrixInt.Count, timeMatrixInt[0].Count];
            DistanceMatrix = new long[timeMatrixInt.Count, timeMatrixInt[0].Count];
            for (int j = 0; j < TimeMatrix.GetLength(0); j++)
            {
                for (int k = 0; k < TimeMatrix.GetLength(1); k++)
                {
                    TimeMatrix[j, k] = Convert.ToInt64(timeMatrixInt[j][k]);
                    DistanceMatrix[j, k] = Convert.ToInt64(timeMatrixInt[j][k]);
                }
            }
        }
        private void InitTimeWindows()
        {
            //מילוי מטריצת חלונות הזמן
            //חלונות הזמן דורשים מימד מסוים
            //אנחנו נשתמש במימד של דקות שעת היציאה תהיה מאותחלת ל-0
            //ונבדןק עבור כל שעה את מספר השניות משעת היציאה ועד אליה
            TimeWindows = new long[shopsDTO.Count + 1, 2];
            TimeSpan timeStart = provider.LeavingTime;
            ////אתחול המחסןב-0 כזמן התחלה וזמן סיום לא מוגבל
           

            TimeWindows[0, 0] = Convert.ToInt64(timeStart.TotalSeconds);
            TimeWindows[0, 1] = long.MaxValue;
            int i = 1;
            foreach (ShopsDTO shop in shopsDTO)
            {
               
                TimeWindows[i, 0] = Convert.ToInt64(((TimeSpan)shop.HourDayStart).TotalSeconds);
                TimeWindows[i++, 1] = Convert.ToInt64(((TimeSpan)shop.HourDayFinish).TotalSeconds);
            }
        }
        private void InitTrucks()
        {
            truckSource = trucksDAL.GetAll().FindAll(t => t.ProviderId == provider.ProviderId);
            VehicleNumber = truckSource.Count;
            //כמה סחורה נכנסת בכל ארגד
            int MaxGoodsInCrate = provider.MaxGoodsInCrate;
            //כמה ארגזים נכנסים במשאית בהתאם למגבלות המשקל והנפח
            int countOfCrateInTruck;
            //כמה סחורה נכנסת בכל משאית
            int capacity;
            int maxCapacityWeigth;
            int maxCapacityVolume;
            //הגבלות הגודל של המשאית
            SizeContainDTO sizeContain;
            VehicleCapacities = new long[truckSource.Count];
            int i = 0;
            foreach (TrucksDTO truck in truckSource)
            {
                sizeContain = sizeContainsDAL.GetById(truck.SizeId);
                maxCapacityWeigth = (int)(Math.Floor(sizeContain.WeightSize / provider.CrateWeight) * MaxGoodsInCrate);
                maxCapacityVolume = sizeContain.VolumeSqmr / provider.CrateVolume * MaxGoodsInCrate;
                VehicleCapacities[i++] = Math.Min(maxCapacityWeigth, maxCapacityVolume);
            }
        }
    }
}
