using System;
using System.Collections.Generic;

#nullable disable

namespace DTO.models
{
    public partial class SizeContainDTO
    {
        public SizeContainDTO()
        {
            
        }

        public int SizeContainId { get; set; }
        public double WeightSize { get; set; }
        public int VolumeSqmr { get; set; } 

       
    }
}
