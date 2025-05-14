using DTO.GoogleMpas;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.googleMaps
{
    public interface DistanceMatrixInterface
    {
        public void CreateTimeAndDistanceMatrix(string depotAddress, List<string> addressShop);
        public TimeAndDistanceMatrix GetTimeAndDistanceMatrix();
    }
}
