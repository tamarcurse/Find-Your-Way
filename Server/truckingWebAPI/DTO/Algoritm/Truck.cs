using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Algoritm
{
    public class Truck
    {
        //הקוד המקורי של המשאיות ,מספר הרישוי שלה
        public string truckIdSource { get; set; }
        //קוד שניתן כעת לצורך האלגוריתם
        public int id { get; set; }

        public int groupId { get; set; }
        //הקיבולת של המשאית
        public int capacity { get; set; }

        public Truck(int id)
        {
            this.id = id;

        }

        public Truck(int id, int capacity)
        {
            this.id = id;

            this.capacity = capacity;
        }
    }
}

