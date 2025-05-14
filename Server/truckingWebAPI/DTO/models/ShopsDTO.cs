using System;
using System.Collections.Generic;

#nullable disable

namespace DTO.models
{
    public partial class ShopsDTO
    {
        public int ShopId { get; set; }
        
        public string placeAddress { get; set; }
        public string placeGoogleMapsId{ get; set; }
        public double Lat { get; set; }
        public double Lang { get; set; }
        public int GoodsRequired { get; set; }
        public string ProviderId { get; set; }
        public TimeSpan HourDayStart { get; set; }
        public TimeSpan HourDayFinish { get; set; }
        public string ShopName { get; set; }
        //public string timeStart { get; set; }
        //public string timeFinish { get; set; }


    }
}
