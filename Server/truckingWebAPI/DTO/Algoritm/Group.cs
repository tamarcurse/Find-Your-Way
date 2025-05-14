using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Algoritm
{
   public class Group
    {
        public Group(int id)
        {
            this.id = id;
            truckId = -1;
            vertexIdList1 = new List<int>();
            vertexIdList2 = new List<int>();
            blackList = new List<int>();
        }

        //קוד קבוצה ממוספר מ-0 ומעלה
        public int id { get; set; }
        // המשאית שתיקח את המסלול הזה (עשוי להשתנות במהלך הריצה
        public int truckId { get; set; }
        //מייצג את כמות הסחורה שהמשאית סוחבת כרגע 
        //זה בעצם סכום הדרישות של כל החנויות שבשתי הרשימות  של הקודקודים
        public int truckContents { get; set; }
        //רשימת הקודקודים בתת מסלול אחד כאשר הקודקוד האחרון ברשימה מהווה קצה מסלול
        //הקודקודים מאוחסנים ברשימה באופן שקודקוד 0 נמצא במקום ה-0 וכו
        public List<int> vertexIdList1 { get; set; }
        //רשימת הקודקודים בתת מסלול שני כאשר הקודקוד האחרון ברשימה מהווה קצה מסלול
        //הקודקודים מאוחסנים ברשימה באופן שקודקוד 0 נמצא במקום ה-0 וכו
        public List<int> vertexIdList2 { get; set; }
        //הרשימה השחורה של הקבוצה. מייצגת את הקודקודים שלא יכולים להצטרף לקבוצה בגלל מגבלות קבולת
        public List<int> blackList { get; set; }

        public bool IsEndOfTrack(int vertexId)
        {
            return vertexIdList1[vertexIdList1.Count - 1] == vertexId || vertexIdList2[vertexIdList2.Count - 1] == vertexId;
        }
    }
}
