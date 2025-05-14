using System;
using System.Collections.Generic;

#nullable disable

namespace DTO.models
{
    public partial class ProviderDTO
    {
        public ProviderDTO()
        {
            
        }

       
        public string placeAddress { get; set; }
        public string placeGoogleMapsId { get; set; }
        public double Lat { get; set; }
        public double Lang { get; set; }
        public string ProviderName { get; set; }
      
        public string PasswordProvider { get; set; }
        public string FactoryName { get; set; }
        public TimeSpan LeavingTime { get; set; }
        public string ProviderId { get; set; }
        public double CrateWeight { get; set; }
        public int CrateVolume { get; set; }
        public int MaxGoodsInCrate { get; set; }


    }
}
