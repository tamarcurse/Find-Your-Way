using DTO.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.googleMaps
{
    public interface IGoogleMapsPlaceId
    {
        public PlacesDTO GetPlaceIdMapsByAddress(string address);
        //public double[] GetCoordinateByAddress(string address);
    }
}
