using Google.OrTools.ConstraintSolver;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Text;

using DTO.models;
using DAL.interfaces;

namespace BLL.OR_Tools
{
    public class FindWay:IFindWay
    {
        private RoutingIndexManager manager;
        private RoutingModel routing;
        private IDataModel dataModel;
        private IStationsInShopDAL stationsInShopDAL;
        int transitCallbackIndex;
        string providerId;
        public FindWay(IDataModel dataModel,IStationsInShopDAL stationsInShopDAL)
        {
            this.dataModel = dataModel;
            this.stationsInShopDAL = stationsInShopDAL;
        }

        public void CalculateRoute(string providerId)
        {
            this.providerId = providerId;
            dataModel.Init(providerId);
            //פונקציה לחישוב מסלול
            manager = new RoutingIndexManager(dataModel.
           TimeMatrix.GetLength(0),//מספר החנויות הכולל
           dataModel.VehicleNumber,//מספר המשאיות
           dataModel.Depot);
            routing = new RoutingModel(manager);
            //יצירת קשתות עם עלות של המרחק מנקודה לנקודה
            transitCallbackIndex = routing.RegisterTransitCallback(
            (long fromIndex, long toIndex) =>
            {
                var fromNode = manager.IndexToNode(fromIndex);
                var toNode = manager.IndexToNode(toIndex);
                return dataModel.TimeMatrix[fromNode, toNode];
            });
            //הוספת עלות הזמן
            routing.SetArcCostEvaluatorOfAllVehicles(transitCallbackIndex);

            CVRP(); //הוספת מימד קיבולת על עלות הקשתות
            TW();//הוספת אילוצי חלונות זמן

            RoutingSearchParameters searchParameters =
            operations_research_constraint_solver.DefaultRoutingSearchParameters();
            searchParameters.FirstSolutionStrategy =
            FirstSolutionStrategy.Types.Value.PathCheapestArc;
            //מגביל את זמן החישוב של הפותר כדי שלא נגיע לסיבוכיות גבוהה במקרה בו אין פתרון
            searchParameters.TimeLimit = new Duration { Seconds = 10 };
            Assignment solution = routing.SolveWithParameters(searchParameters);
             SaveSolution(routing, manager, solution);
        }

        private void SaveSolution(RoutingModel routing, RoutingIndexManager manager, Assignment solution)
        {
            
            stationsInShopDAL.DeleteAllByProviderId(providerId);
            if (solution == null)
                return;
            
            int StationSerialNumber;
            
            RoutingDimension timeDimension = routing.GetMutableDimension("Time");
            long totalTime = 1;
            for (int i = 0; i < dataModel.VehicleNumber; ++i)
            {
                
                //האינדקס של המסלול של המשאית הנוכחית
                var index = routing.Start(i);
                StationSerialNumber = 0;
                while (routing.IsEnd(index) == false)
                {
                   
                        var timeVar = timeDimension.CumulVar(index);
                    if (manager.IndexToNode(index) != 0)
                    {
                        StationsInShopDTO newStation = new StationsInShopDTO();
                        newStation.ProviderId = providerId;
                        newStation.LicensePlateTruck = dataModel.truckSource[i].LicensePlate;

                        newStation.ShopId = dataModel.shopsDTO[manager.IndexToNode(index) - 1].ShopId;
                        newStation.TimeStart = TimeSpan.FromSeconds(solution.Min(timeVar));
                        newStation.TimeFinish = TimeSpan.FromSeconds(solution.Max(timeVar));
                        newStation.StationSerialNumber = StationSerialNumber++;
                        stationsInShopDAL.Add(newStation);
                    }
                   
                    index = solution.Value(routing.NextVar(index));
                }
                var endTimeVar = timeDimension.CumulVar(index);
              
                totalTime += solution.Min(endTimeVar);
            }
           
        }

        private void TW()
        {

            //הוספת אילוצי חלונות זמן// 
            routing.AddDimension(
            transitCallbackIndex, // transit callback
            500, // allow waiting time
            100000, // vehicle maximum capacitiess
            false, // האם להתחיל בכל מקום מ-0
            "Time");
            RoutingDimension timeDimension = routing.GetMutableDimension("Time");
            // הוסף אילוצי חלון זמן עבור כל מיקום מלבד המחסן
            for (int i = 1; i < dataModel.TimeWindows.GetLength(0); ++i)
            {
                long index = manager.NodeToIndex(i);
                timeDimension.CumulVar(index).SetRange(
                dataModel.TimeWindows[i, 0],//זמן התחלה
                dataModel.TimeWindows[i, 1]);//זמן סיום
            }
            //(הוסף את אילוצי חלון הזמן של המחסן (זמן התחלה וסיום של כל משאית
            for (int i = 0; i < dataModel.VehicleNumber; ++i)
            {
                long index = routing.Start(i);
                timeDimension.CumulVar(index).SetRange(
                dataModel.TimeWindows[0, 0],
                dataModel.TimeWindows[0, 1]);
            }
            // להכניס לבעיה את נתוני חלונות הזמן שהכנסנו כדי למצוא פתאון העונה עליהם
            for (int i = 0; i < dataModel.VehicleNumber; ++i)
            {
                routing.AddVariableMinimizedByFinalizer(
                timeDimension.CumulVar(routing.Start(i)));
                routing.AddVariableMinimizedByFinalizer(
                timeDimension.CumulVar(routing.End(i)));
            }
        }
        private void CVRP()
        {
            //הוספת אילוצי נפח ומשקל
            long x = routing.Size();
            int demandWeightCallbackIndex = routing.RegisterUnaryTransitCallback(
            (long fromIndex) =>
            {
                var fromNode = manager.IndexToNode(fromIndex);
                return dataModel.Demands[fromNode];
            }
            );
            routing.AddDimensionWithVehicleCapacity(
            demandWeightCallbackIndex, 0, // null capacity slack
            dataModel.VehicleCapacities, // vehicle maximum capacities
            false, // start cumul to zero
            "Capacity"); ;
            //שם עלות על הקשתות לאלוצי נפח// 
            int demandVolumeCallbackIndex = routing.RegisterUnaryTransitCallback(
            (long fromIndex) =>
            {
                // Convert from routing variable Index to demand NodeIndex.
                var fromNode = manager.IndexToNode(fromIndex);
                return dataModel.Demands[fromNode];
            }
            );
            routing.AddDimensionWithVehicleCapacity(
            demandVolumeCallbackIndex, 0, // null capacity slack
            dataModel.VehicleCapacities, // vehicle maximum capacities
            true, // start cumul to zero
            "Capacity");
        }
    }
}
