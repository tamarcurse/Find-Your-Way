using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.GoogleMpas
{
    public class TimeAndDistanceMatrix
    {
        public List<string> addresses { get; set; }
        public List<List<int>> distanceMatrix { get; set; }
        public List<List<int>> durationMatrix { get; set; }
        public TimeAndDistanceMatrix()
        {
            addresses = new List<string>();
            distanceMatrix = new List<List<int>>();
            durationMatrix = new List<List<int>>();
        }

    }
}
