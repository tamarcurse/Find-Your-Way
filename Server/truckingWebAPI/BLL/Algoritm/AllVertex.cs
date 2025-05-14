using DTO.Algoritm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Algoritm
{
    public class AllVertex:AllVertexInterface
    {
        //רשימת הקודקודם בגרף
        public List<Vertex> vertexList { get; set; }

        //רשימת קודקודי קצות המסלול
        public List<int> endOfTruckList { get; set; }

        //הנתונים מהמסד נתונים הקשורים לחנויות כגון הדרישות שלהן, הגבלות זמן וכן מטריצת מרחקים
        public DataModelVertexInterface dataVertex { get; set; }

        public AllVertex(DataModelVertexInterface dataVertex)
        {
            this.dataVertex = dataVertex;

        }

        //אתחול נתוני הקודקודים
        public void InitAllVertex(string providerId)
        {
            //
            //שליפת הנתונים מהמסד
            dataVertex.InsertFormDBByProvider(providerId);

            //מיון הקודקודים לפי הזמנות החנויות 
            dataVertex.demondsShop.Sort();
            vertexList = new List<Vertex>();

            //הוספת המחסן לרשימת הקודקודים
            vertexList.Add(new Vertex(0, 0, 0));

            //הוספת שאר הקודקודים לגרף
            for (int i = 1; i < dataVertex.demondsShop.Count; i++)
            {
                vertexList.Add(new Vertex(i, i, dataVertex.demondsShop[i]));
            }
        }
        //חלוקת הקודקודים שנבחרו לקבוצות של 2,2
        //לזכור לטפל במקרה קצה של קיפאון עם יותר מ-2 צמתים
        public void DivideVertexOfGroup(int newVertex)
        {
            //
            int count = 0;
            //עבור כל קבוצה
            //מחפש 2 קודקודים קרובים ביותר
            //שעדיין לא שייכים לשום קבוצה ומעדכן אותם כשייכים לקבוצה
            while (Find2VertexThatMinDistance(count++)) ;
            foreach (int i in endOfTruckList)
            {
                //- האם הקדקוד הקרוב ביותר אליו מבין קצות המסלול 
                //לא נמצא בקבוצה שלו 
                if (CheckIfPerIsBestClosed(i, i))
                {
                    //בדיקה האם לא מוסר קודקוד שעכשיו נוסף(כדי למנוע מצב של קיפאון)
                    if (i != newVertex)
                    {
                        vertexList[i].groupId = -1;
                        endOfTruckList.Remove(i);
                        newVertex = GetVertexMin();
                        endOfTruckList.Add(newVertex);
                        vertexList[i].selected = false;
                    }
                    else
                    {
                        //מצא את הקודקוד הקרוב ביותר ברשימת קצות המסלול
                        int distuncevertexMin = endOfTruckList.FindAll((v1) => v1 != i).Min((v1) => dataVertex.distanceMatrix[i][v1]);
                        int vertexMin = endOfTruckList.FirstOrDefault((v) => dataVertex.distanceMatrix[i][v] == distuncevertexMin);
                        
                        endOfTruckList.Remove(vertexMin);
                        newVertex = GetVertexMin();
                        endOfTruckList.Add(newVertex);
                        vertexList[vertexMin].selected = false;
                        vertexList[vertexMin].groupId = -1;
                    }
                    //מחיקת החלוקה לקבוצות
                    foreach (int v in endOfTruckList)
                    {
                        vertexList[v].groupId = -1;
                    }

                    //חלק לקבוצות מחדש
                    DivideVertexOfGroup(newVertex);
                    return;

                }
            }
        }
        //מביאה קודקוד שמרחקו מהמחסן הוא הכי קטן שלא נבחר עדיין
        public int GetVertexMin()
        {
            //
            long min = Int64.MaxValue;
            int vertexMin = 0;
            for (int i = 1; i < vertexList.Count; i++)
            {
                if (!vertexList[i].selected && dataVertex.distanceMatrix[0][i] < min)
                {
                    min = dataVertex.distanceMatrix[0][i];
                    vertexMin = i;
                }
            }
            vertexList[vertexMin].selected = true;
            return vertexMin;
        }
        //מוצאת זוג קדקודים קרובים ביותר מתוך רשימת קצות המסלול
        //שעדיין לא שייכים לקבוצה מסוימת 
        private bool Find2VertexThatMinDistance(int groupId)
        {
            //
            int minDistance = Int16.MaxValue;
            int vertexSource = 0;
            int vertexDest = 0;
            foreach (int i in endOfTruckList)
            {


                if (vertexList[i].groupId == -1)
                {
                    foreach (int j in endOfTruckList)
                    {

                        if (vertexList[j].groupId == -1 && dataVertex.distanceMatrix[i][j] < minDistance && i != j)
                        {
                            minDistance = dataVertex.distanceMatrix[i][j];
                            vertexSource = i;
                            vertexDest = j;
                        }
                    }

                }

            }
            if (minDistance == Int16.MaxValue)
                return false;
            vertexList[vertexSource].groupId = groupId;
            vertexList[vertexDest].groupId = groupId;



            return true;

        }
        //-הפונקציה בודקת עבור קדקוד מסוים האם הקדקוד הקרוב ביותר אליו מבין קצות המסלול 
        //לא נמצא בקבוצה שלו 
        public bool CheckIfPerIsBestClosed(int vertexId, int newVertex)
        {
            //
            int per = endOfTruckList.FirstOrDefault(v => vertexList[v].groupId == vertexList[vertexId].groupId && vertexId != v);
            foreach (int someVertex in endOfTruckList)
            {
                if (dataVertex.distanceMatrix[someVertex][newVertex] < dataVertex.distanceMatrix[per][newVertex] && someVertex != vertexId && someVertex != vertexId)
                    return true;
            }
            return false;
        }
        //מחזריה קודקוד שלא שייך לשום קבוצה
        //אן אין קודקוד כזה, תחזיר -1
        public int GetVertexNotGroup()
        {
            //
            Vertex ver = vertexList.FirstOrDefault(v => v.groupId == -1 && v.id != 0);
            if (ver == null)
                return -1;
            return ver.id;
        }
    }
}

