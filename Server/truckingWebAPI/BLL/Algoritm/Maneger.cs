using BLL.interfaces;
using BLL.Algoritm;
using DTO.models;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
using DTO.Algoritm;
using DAL.interfaces;

namespace BLL.algoritm
{
    //המחלקה שמנהלת את האלגוריתם ושומרת בתוכה את כל הנתונים
    public class Maneger:ManegerInterface
    {

        //עבור איזה ספק מתבצע החישוב הנ"ל
        public string providerId { get; set; }
        //רשימת הקבוצות והמשאיות שלהן
        public GroupsAndTruckInterface groupsAndTruck { get; set; }

        //רשימת הקדקודים
        public AllVertexInterface allVertex { get; set; }

        //רשימת התחנות כדי להכניס את הנתונים בסיום החישוב
        IStationsInShopDAL stationsInShopDAL;



        public Maneger(IStationsInShopDAL stationsInShopDAL, AllVertexInterface allVertex, GroupsAndTruckInterface groupsAndTruck)
        {

            this.stationsInShopDAL = stationsInShopDAL;
            this.allVertex = allVertex;
            this.groupsAndTruck = groupsAndTruck;

        }




        public bool CumputeByProvider(string providerId)
        {

            try
            {
                this.providerId = providerId;
                allVertex.InitAllVertex(providerId);
                groupsAndTruck.Init(providerId);
                Start();
                while (allVertex.GetVertexNotGroup() != -1)
                {  //לטפל המקרה קצה שבו כל הקודקודים פסולים            
                    AddVertexToGroup(new List<bool>(new bool[allVertex.vertexList.Count]), 1, false);
                }
                groupsAndTruck.ConnectGroup();
                InsertSolutionToDB();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }



        }
        //מוסיפה קדקוד למסלול
        // שומר כמה פעמים זימנתי את הפונקציה count
        private int AddVertexToGroup(List<bool> vertxeIsFiled, int count, bool isAllvertxFailed)
        {

            int vertexClosed;
            int distanceClose;
            int minVertexClosed = 0;
            int minDistanceClose = Int16.MaxValue;
            int sorceVertexMin = 0;
            if (count == allVertex.vertexList.Count)
                return 0;
            // =;
            //list<int> vertexclosedlist = new list<int>(data.numofshops+1);
            //vertexclosedlist.add(int16.maxvalue);
            // ללולאה שעוברת כל כל הצמתים ומוצאת את הצומת הקרובה ביותר לכל צומת שמהווה קצה מסלול
            //אם לא נמצא קדקוד להוסיף תחזיר 0
            foreach (var i in allVertex.endOfTruckList)
            {
                //אם הקדקוד הנוכחי הוא קצה מסלול והוא לא קודקוד פסול
                if (vertxeIsFiled[i] == false)
                {
                    var results = FindVertexClosed(i);
                    vertexClosed = results.Item1;
                    distanceClose = results.Item2;
                    if (distanceClose < minDistanceClose)
                    {
                        minDistanceClose = distanceClose;
                        minVertexClosed = vertexClosed;
                        sorceVertexMin = i;
                    }
                }


            }
            if (minVertexClosed == Int16.MaxValue)
                //לטפל המקרה קצה שבו כל הקודקודים פסולים
                return AddVertexToGroup(new List<bool>(new bool[allVertex.vertexList.Count]), count + 1, true);
            allVertex.vertexList[minVertexClosed].groupId = allVertex.vertexList[sorceVertexMin].groupId;
            if (!isAllvertxFailed && allVertex.CheckIfPerIsBestClosed(sorceVertexMin, minVertexClosed))
            {
                vertxeIsFiled[sorceVertexMin] = true;
                allVertex.vertexList[minVertexClosed].groupId = -1;
                return AddVertexToGroup(vertxeIsFiled, count + 1, false);

            }
            else
            {
                if (groupsAndTruck.CheckCapcity(allVertex.vertexList[minVertexClosed].demand, allVertex.vertexList[sorceVertexMin].groupId))
                {
                    if (groupsAndTruck.groupList[allVertex.vertexList[sorceVertexMin].groupId].vertexIdList1.FirstOrDefault(v => v == sorceVertexMin) != 0)
                        groupsAndTruck.groupList[allVertex.vertexList[sorceVertexMin].groupId].vertexIdList1.Add(minVertexClosed);
                    else
                        groupsAndTruck.groupList[allVertex.vertexList[sorceVertexMin].groupId].vertexIdList2.Add(minVertexClosed);

                    allVertex.endOfTruckList.Remove(sorceVertexMin);
                    allVertex.endOfTruckList.Add(minVertexClosed);
                    return 1;
                }
                else
                {
                    groupsAndTruck.groupList[allVertex.vertexList[sorceVertexMin].groupId].blackList.Add(sorceVertexMin);
                    allVertex.vertexList[minVertexClosed].groupId = -1;
                    return AddVertexToGroup(vertxeIsFiled, count + 1, false);
                }
            }
        }
        //בודק האם יש משאית מתאימה שתספיק לכל התכולה שבקבוצה יחד עם הדרישה הנוספת 


        //מוצאת קדקוד קרוב ביותר לקצה המסלול שנשלח שעדיין לא שייך לשום קבוצה ולא נמצא ברשימה השחורה
        // של הקבוצה של קצה המסלול הנשלח 
        //אם אין קודקוד כזה מחזירה 0
        private (int, int) FindVertexClosed(int vertexId)
        {
            int minDistance = Int16.MaxValue;
            int vertexClosed = 0;



            for (int i = 1; i < allVertex.vertexList.Count; i++)
            {
                if (allVertex.vertexList[i].groupId == -1 && allVertex.dataVertex.distanceMatrix[i][vertexId] < minDistance)
                    if (groupsAndTruck.groupList[allVertex.vertexList[vertexId].groupId].blackList.FirstOrDefault(v => v == i) == 0)
                    {
                        minDistance = allVertex.dataVertex.distanceMatrix[i][vertexId];
                        vertexClosed = i;
                    }
            }
            return (vertexClosed, minDistance);

        }


        //הפונקציה מוצאת קודקודים קרובים למחסן ומשייכת כל 2 קודקודים לקבוצה מסוימת
        private void Start()
        {
            //ישמור את קבוצת הקודקודים העתידים להוות קצה מסלול
            allVertex.endOfTruckList = new List<int>();

            int i;
            //פתיחת המסלולים
            for (i = 0; i < groupsAndTruck.dataTruck.numOfTruck * 2; i++)
            {
                allVertex.endOfTruckList.Add(allVertex.GetVertexMin());
            }
            allVertex.DivideVertexOfGroup(0);
            
            //נסה לאתחל משאית עבור כל קבוצה
            //אם לא הצלחת
            if (!InitGroup())
            {
                //הוסף משאית דמה למשאיות (חלק מהמשאיות יבצעו מספר מסלולים)
                groupsAndTruck.AddTruck();
                Start();
            }


        }






        //מאתחלת את הקבוצות לפי הקודקודים שנבחרו לכל קבוצה ומתאימה משאית בעלת קיבולת מספקת לכל קבוצה
        //להוסיף מקרי קצה כאשר אין משאית 
        private bool InitGroup()
        {
            groupsAndTruck.groupList = new List<Group>();
            Group group;
            foreach (int item in allVertex.endOfTruckList)
            {
                //מציאת הקבוצה אליה הקודקוד שייך
                group = groupsAndTruck.groupList.FirstOrDefault(g => g.id == allVertex.vertexList[item].groupId);
                
                //אם הקבוצה שהוא שייך אליה לא קיימת
                if (group == null)
                {
                    //צור קבוצה חדשה
                    group = new Group(allVertex.vertexList[item].groupId);

                    //הוסף אותו לרשימת הקודקודים הראשונה של הקבוצה
                    group.vertexIdList1.Add(item);
                    group.truckContents = allVertex.vertexList[item].demand;
                    groupsAndTruck.groupList.Add(group);
                }
                //אם הקבוצה שהוא שייך אליה כבר קיימת
                else
                {
                    group.vertexIdList2.Add(item);

                    //הוסף את הקיבולת לתכולת המשאית
                    group.truckContents += allVertex.vertexList[item].demand;
                }

            }

            //למיין את רשימת הקבוצות לפי הקוד שלהם
            //כך שהקבוצה ה-0 תהיה במקום ה-0 במערך
            groupsAndTruck.groupList.Sort((g1, g2) => g1.id - g2.id);
            //מחלק את המשאיות בין הקבוצות

            //אם הפונקציה מחזירה שקר
            //קרא לפונקציה מקרי קצה
            if (!groupsAndTruck.DivideTrucksToGroup()) { return false; }
            return true;

        }
        //מכניסה את הנתונים הסופיים לטבלת התחנות
        private void InsertSolutionToDB()
        {
            int count = 0;
            foreach (Group group in groupsAndTruck.groupList)
            {
                count = 0;
                foreach (int shop in group.vertexIdList1)
                {
                    StationsInShopDTO station = new StationsInShopDTO();
                    station.ProviderId = providerId;
                    Truck t = groupsAndTruck.truckList.FirstOrDefault(t => t.id == group.truckId);
                    if (t != null)
                        station.LicensePlateTruck = t.truckIdSource;
                    station.ShopId = allVertex.vertexList[shop].shpoId;
                    station.StationSerialNumber = count;
                    stationsInShopDAL.Add(station);
                    count++;
                }
            }

        }


    }
}
