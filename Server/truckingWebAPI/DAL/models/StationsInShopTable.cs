using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.models
{
    public partial class StationsInShopTable
    {
        public int Id { get; set; }
        public string LicensePlateTruck { get; set; }
        public int ShopId { get; set; }
        public string ProviderId { get; set; }
        public int StationSerialNumber { get; set; }
        public TimeSpan? TimeStart { get; set; }
        public TimeSpan? TimeFinish { get; set; }

        public virtual TrucksTable LicensePlateTruckNavigation { get; set; }
        public virtual ProviderTable Provider { get; set; }
        public virtual ShopsTable Shop { get; set; }
    }
}
