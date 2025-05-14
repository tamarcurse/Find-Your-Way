using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Algoritm
{
    public class Vertex
    {
        //הקוד של הקודקוד זה בעצם האינדקס שלו במטריצת המרחקים ובמטריצת הזמנים
        //כאשר האינדקס 0 שייך למחסן
        public int id { get; set; }
        //החנות אותה הקודקוד מייצג
        public int shpoId { get; set; }
        //כמות הסחורה שהחנות דורשת לקבל
        public int demand { get; set; }
        //קוד הקבוצה שהקודקוד משתייך אליה אם הוא עוד לא שייך לאף קבוצה הערך יהיה 
        //groupId=-1
        public int groupId { get; set; }
        public bool selected { get; set; }
        public Vertex(int id, int shopId, int demand)
        {
            this.id = id;
            this.shpoId = shpoId;
            this.demand = demand;
            groupId = -1;
            selected = false;

        }
    }
}
