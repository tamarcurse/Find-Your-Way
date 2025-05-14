using DTO.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.OR_Tools
{
    public interface IDataModel
    {
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
        //מערך הקיבולות של המשאיות
        public long[] VehicleCapacities { get; set; }
        //מערך החנויות המשתתפות בהובלה
        public List<ShopsDTO> shopsDTO { get; set; }
        //מערך משאיות המשתתפות בהובלה
        public List<TrucksDTO> truckSource { get; set; }
        public void Init(string providerId);

    }
}
