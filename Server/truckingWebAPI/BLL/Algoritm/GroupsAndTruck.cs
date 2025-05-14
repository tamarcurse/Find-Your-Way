using DTO.Algoritm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Algoritm
{
    public class GroupsAndTruck:GroupsAndTruckInterface
    {
        //רשימת הגבלות קיבולת של המשאיות
        public DataModelTruckInterface dataTruck { get; set; }
        //שומר משאית כלשהי שהקיבולת שלה היא הגדולה ביותר
        public Truck truckByMaxCapacity { get; set; }
        
        //רשימת הקבוצות -כמספר המשאיות
        public List<Group> groupList { get; set; }

        //רשימת המשאיות
        public List<Truck> truckList { get; set; }
        public GroupsAndTruck(DataModelTruckInterface dataTruck)
        {
            this.dataTruck = dataTruck;
        }

        //
        public void Init(string providerId)
        {
            dataTruck.InsertDatFromDBByProviderId(providerId);
            fillTruckList();
            int maxCapacity = truckList.Max(t => t.capacity);
            truckByMaxCapacity = truckList.FirstOrDefault(t => t.capacity == maxCapacity);
        }


        
      
        //ממלא את רשימת המשאיות בהתאם לנתונים ב-capacityTruks
        private void fillTruckList()
        {

            truckList = new List<Truck>();
            for (int i = 0; i < dataTruck.capacityTruks.Count; i++)
            {
                truckList.Add(new Truck(i, dataTruck.capacityTruks[i]));
            }
        }
        public bool CheckCapcity(int demand, int groupId)
        {
            if (groupList[groupId].truckContents + demand <= truckList[groupList[groupId].truckId].capacity)
                return true;
            return DivideTrucksToGroup();
        }
        //מחלקת את המשאיות לקבוצות כל משאית לקבוצה
        public bool DivideTrucksToGroup()
        {
            List<Group> l = new List<Group>(groupList);

            l.Sort((g1, g2) => g1.truckContents - g2.truckContents);
            for (int i = 0; i < dataTruck.capacityTruks.Count - 1; i++)
            {
                if (l[i].truckContents > truckList[i].capacity)
                    return false;

            }
            for (int i = 0; i < dataTruck.capacityTruks.Count; i++)
            {

                truckList[i].groupId = l[i].id;
                groupList[l[i].id].truckId = i;
            }
            return true;
        }
        //במקרה שנוספה משאית דמה (כי המשאיות לא הספיקו)
        //   הוספתי כל פעם את המשאית שהקיבולת שלה היא הגדולה ביותר
        //כעת נרצה לבדוק עבור כל משאית דמה כמה היא מכילה בפועל ולתת לה את המשאית הקטנה ביותר המאימה עבורה
        //כך שהמסלולים הכפולים יתחלקו באופן שווה בין המשאיות
       public void ChangeTruck()
        {
            
            foreach (var item in truckList.FindAll(v=>v.truckIdSource==truckByMaxCapacity.truckIdSource))
            {
                foreach (var truck in truckList)
                {

                }
            }
        }
        //מאחדת את 2 תת המסלולים למסלול אחד סופי
        //ניתן להוסיף התחשבות בזמנים
        public void ConnectGroup()
        {
            foreach (Group group in groupList)
            {
                if (group.vertexIdList1 == null)
                    continue;
                group.vertexIdList2.Reverse();
                group.vertexIdList1.AddRange(group.vertexIdList2);
                group.vertexIdList2 = null;
                //מצא את המשאית השייכת לקבוצה הנ"ל
                Truck currentTruck = truckList.FirstOrDefault(t => t.groupId == group.id);
                //בדוק האם המשאית הזאת קיימת פעמיים או יותר ברשימת המשאיות
                Truck truck2= truckList.FirstOrDefault(t => t.truckIdSource == currentTruck.truckIdSource&&t.id!=currentTruck.id);
                while(truck2!=null)
                {
                    Group g = groupList[truck2.groupId];
                    g.vertexIdList2.Reverse();
                    g.vertexIdList1.AddRange(group.vertexIdList2);
                    g.vertexIdList2 = null;
                    group.vertexIdList1.AddRange(g.vertexIdList1);
                    g.vertexIdList1 = null;
                    truckList.Remove(truck2);
                    truck2 = truckList.FirstOrDefault(t => t.truckIdSource == currentTruck.truckIdSource && t.id != currentTruck.id);
                }
            }
        }
        //הוספת משאית למקרה שהקיבולת לא מספיקה
        public void AddTruck()
        {
       
            Truck newTruck = new Truck(truckList.Count, truckByMaxCapacity.capacity);
            newTruck.truckIdSource = truckByMaxCapacity.truckIdSource;
            newTruck.groupId = -1;

            truckList.Add(newTruck);
        }

    }
}
