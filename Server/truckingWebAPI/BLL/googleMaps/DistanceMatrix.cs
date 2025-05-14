using DTO.GoogleMpas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using DTO.models;

namespace BLL.googleMaps
{
    public class DistanceMatrix : DistanceMatrixInterface
    {
        
        private TimeAndDistanceMatrix timeAndDistanceMatrix;


        public DistanceMatrix()
        {
            timeAndDistanceMatrix = new TimeAndDistanceMatrix();
        }

        private static string CreateUrl(string[] origin_addresses, string[] dest_addresses)
        {
            string url = "https://maps.googleapis.com/maps/api/distancematrix/json?";
            string orgins = "";
            string dest = "";
            foreach (string item in origin_addresses)
            {
                orgins += "place_id:" + item + "|";
            }
            orgins = orgins.Substring(0, orgins.Length - 1);
            foreach (string item in dest_addresses)
            {
                dest += "place_id:" + item + "|";
            }
            dest = dest.Substring(0, dest.Length - 1);
            url += " &destinations=" + dest + "&origins=" + orgins + "&key="+keyGoogleMaps.key;
            return url;
        }
        private void InsertPartDistaceMatrixAndTimeMatrix(string[] origin_addresses, string[] dest_addresses)
        {

            string html;
            if (origin_addresses.Length * dest_addresses.Length > 100)
                return;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(CreateUrl(origin_addresses, dest_addresses));
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }
            dynamic d2 = JsonConvert.DeserializeObject(html);


            for (int i = 0; i < dest_addresses.Length; i++)
            {
                List<int> rowInMatrix = new List<int>();
                for (int j = 0; j < origin_addresses.Length; j++)
                {
                    rowInMatrix.Add(Convert.ToInt32(d2.rows[j].elements[i].duration.value));
                    // MessageBox.Show(Convert.ToInt32(d2.rows[i].elements[j].distance.value));
                }
                timeAndDistanceMatrix.durationMatrix.Add(rowInMatrix);
            }
            for (int i = 0; i < dest_addresses.Length; i++)
            {
                List<int> rowInMatrix = new List<int>();
                for (int j = 0; j < origin_addresses.Length; j++)
                {
                    rowInMatrix.Add(Convert.ToInt32(d2.rows[j].elements[i].distance.value));
                    // MessageBox.Show(Convert.ToInt32(d2.rows[i].elements[j].distance.value));
                }
                timeAndDistanceMatrix.distanceMatrix.Add(rowInMatrix);
            }


        }
        private void InsertDistaceMatrixAndTimeMatrix(string[] addresses)
        {
            int max_elements = 100;
            int numAddress = addresses.Length;
            int numColumn = numAddress;
            if (numColumn > max_elements)
                return;
            int numRows = max_elements / numColumn;
            //double numRows_ = (double)max_elements / numColumn;
            //if (numRows_ != (int)numRows_)
            //    numRows_++;
            //int numRows = (int)numRows_;
            int numSend = numAddress / numRows;
            int numRowInEndSend = numAddress - numSend * numRows;
            string[] destion_adress;
            for (int i = 0; i < numSend; i++)
            {
                destion_adress = new string[numRows];

                Array.Copy(addresses, i * numRows, destion_adress, 0, numRows);
                InsertPartDistaceMatrixAndTimeMatrix(addresses, destion_adress);
            }
            if (numRowInEndSend == 0)
                return;
            destion_adress = new string[numRowInEndSend];
            
            Array.Copy(addresses,numAddress-numRowInEndSend, destion_adress,0, numRowInEndSend);
            InsertPartDistaceMatrixAndTimeMatrix(addresses, destion_adress);
        }

        // מקבל את כתובת המחסן וכתובות החנויות
        public void CreateTimeAndDistanceMatrix(string depotAddress, List<string> addressShop)
        {
            //באינדקס 0 יהיה שמור כתובת המחסן
            timeAndDistanceMatrix.addresses.Add(depotAddress);
            timeAndDistanceMatrix.addresses.AddRange(addressShop);
            InsertDistaceMatrixAndTimeMatrix(timeAndDistanceMatrix.addresses.ToArray());
           
        }
        public TimeAndDistanceMatrix GetTimeAndDistanceMatrix()
        {
            return timeAndDistanceMatrix;
        }
    }
}
