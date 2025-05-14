using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.models
{
    public partial class ProviderTable
    {
        public ProviderTable()
        {
            ShopsTables = new HashSet<ShopsTable>();
            StationsInShopTables = new HashSet<StationsInShopTable>();
            TrucksTables = new HashSet<TrucksTable>();
        }

        public string ProviderId { get; set; }
        public string ProviderName { get; set; }
        public string PasswordProvider { get; set; }
        public string FactoryName { get; set; }
        public TimeSpan LeavingTime { get; set; }
        public int PlaceId { get; set; }
        public double CrateWeight { get; set; }
        public int CrateVolume { get; set; }
        public int MaxGoodsInCrate { get; set; }

        public virtual PlacesTable Place { get; set; }
        public virtual ICollection<ShopsTable> ShopsTables { get; set; }
        public virtual ICollection<StationsInShopTable> StationsInShopTables { get; set; }
        public virtual ICollection<TrucksTable> TrucksTables { get; set; }
    }
}
