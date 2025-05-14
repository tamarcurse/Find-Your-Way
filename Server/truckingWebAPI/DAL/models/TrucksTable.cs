using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.models
{
    public partial class TrucksTable
    {
        public TrucksTable()
        {
            StationsInShopTables = new HashSet<StationsInShopTable>();
        }

        public string LicensePlate { get; set; }
        public int SizeId { get; set; }
        public string NameOfDriver { get; set; }
        public string IdOfDriver { get; set; }
        public string ProviderId { get; set; }

        public virtual ProviderTable Provider { get; set; }
        public virtual SizeContainTable Size { get; set; }
        public virtual ICollection<StationsInShopTable> StationsInShopTables { get; set; }
    }
}
