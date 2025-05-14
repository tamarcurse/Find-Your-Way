using System;
using System.Collections.Generic;
using System.Text;
using DTO.models;
namespace DTO.viewMap
{
    public class StationInMap
    {
        public TrucksDTO truck { get; set; }
        public List< StationShop> stations{ get; set; }
        public StationInMap()
        {
            stations = new List<StationShop>();
        }

    }
}
