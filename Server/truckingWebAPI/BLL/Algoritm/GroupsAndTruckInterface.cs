using DTO.Algoritm;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Algoritm
{
    public interface GroupsAndTruckInterface
    {
        public DataModelTruckInterface dataTruck { get; set; }
        //שומר משאית כלשהי שהקיבולת שלה היא הגדולה ביותר
        public Truck truckByMaxCapacity { get; set; }
        //רשימת הקבוצות -כמספר המשאיות
        public List<Group> groupList { get; set; }
        //רשימת המשאיות
        public List<Truck> truckList { get; set; }
        public void Init(string providerId);
        public bool CheckCapcity(int demand, int groupId);
        public bool DivideTrucksToGroup();
        public void ChangeTruck();
        public void ConnectGroup();
        public void AddTruck();
    }
}
