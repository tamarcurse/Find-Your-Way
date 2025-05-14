using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.models
{
    public partial class PlacesTable
    {
        public PlacesTable()
        {
            ProviderTables = new HashSet<ProviderTable>();
            ShopsTables = new HashSet<ShopsTable>();
        }

        public int PlaceId { get; set; }
        public string PlaceAddress { get; set; }
        public string IdMaps { get; set; }
        public string Lat { get; set; }
        public string Lang { get; set; }

        public virtual ICollection<ProviderTable> ProviderTables { get; set; }
        public virtual ICollection<ShopsTable> ShopsTables { get; set; }
    }
}
