using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.models
{
    public partial class SizeContainTable
    {
        public SizeContainTable()
        {
            TrucksTables = new HashSet<TrucksTable>();
        }

        public int SizeContainId { get; set; }
        public double WeightSize { get; set; }
        public int VolumeSqmr { get; set; }

        public virtual ICollection<TrucksTable> TrucksTables { get; set; }
    }
}
