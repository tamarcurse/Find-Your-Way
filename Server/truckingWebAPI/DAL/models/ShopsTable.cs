using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.models
{
    public partial class ShopsTable
    {
        public ShopsTable()
        {
            StationsInShopTables = new HashSet<StationsInShopTable>();
        }

        public int ShopId { get; set; }
        public int PlaceId { get; set; }
        public int GoodsRequired { get; set; }
        public string ProviderId { get; set; }
        public TimeSpan HourDayStart { get; set; }
        public TimeSpan HourDayFinish { get; set; }
        public string ShopName { get; set; }

        public virtual PlacesTable Place { get; set; }
        public virtual ProviderTable Provider { get; set; }
        public virtual ICollection<StationsInShopTable> StationsInShopTables { get; set; }
    }
}
