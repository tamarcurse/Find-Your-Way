using System;
using System.Collections.Generic;

#nullable disable

namespace DTO.models
{
    public partial class StationsInShopDTO
    {
        public int Id { get; set; }
        public string LicensePlateTruck { get; set; }
        public int ShopId { get; set; }
        public string ProviderId { get; set; }
        public int StationSerialNumber { get; set; }
        public TimeSpan? TimeStart { get; set; }
        public TimeSpan? TimeFinish { get; set; }
    }
}
