using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.viewMap
{
   public class StationShop
    {
        public string shopAddress { get; set; }
        public string shopName { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string TimeStart { get; set; }
        public string TimeFinish { get; set; }
        public int SerialNumber { get; set; }
    }
}
