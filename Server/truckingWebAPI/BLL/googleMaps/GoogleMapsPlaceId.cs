using DTO.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace BLL.googleMaps
{
    public class GoogleMapsPlaceId:IGoogleMapsPlaceId
    {
        public PlacesDTO GetPlaceIdMapsByAddress(string address)
        {
            address += " ישראל";
            string url = "https://maps.googleapis.com/maps/api/geocode/json?address=" + address + "&key="+ keyGoogleMaps.key;
            string html = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;



            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }
            dynamic d2 = JsonConvert.DeserializeObject(html);
            PlacesDTO p = new PlacesDTO();
            p.IdMaps = d2.results[0].place_id;
            p.Lang = d2.results[0].geometry.location.lng;
            p.Lat = d2.results[0].geometry.location.lat;
            return p;
        }
        //public double[] GetCoordinateByAddress(string address)
        //{
        //    address += " ישראל";
        //    string url = "https://maps.googleapis.com/maps/api/geocode/json?address=" + address + "&key=" + keyGoogleMaps.key;
        //    string html = string.Empty;
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //    request.AutomaticDecompression = DecompressionMethods.GZip;



        //    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //    using (Stream stream = response.GetResponseStream())
        //    using (StreamReader reader = new StreamReader(stream))
        //    {
        //        html = reader.ReadToEnd();
        //    }
        //    dynamic d2 = JsonConvert.DeserializeObject(html);
        //    double[] arr = new double[2];
        //    arr[0] =Convert.ToDouble(d2.results[0].geometry.location.lat);
        //    arr[1] = Convert.ToDouble(d2.results[0].geometry.location.lng);
        //    return arr;
        //}
    }
}
