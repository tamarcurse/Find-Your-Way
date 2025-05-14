using System;
using System.Collections.Generic;

#nullable disable

namespace DTO.models
{
    public partial class PlacesDTO
    {
        public PlacesDTO()
        {

        }

        public PlacesDTO(string idMaps, double lat, double lang)
        {
            IdMaps = idMaps;
            Lat = lat;
            Lang = lang;
        }

        public int PlaceId { get; set; }
        
        public string PlaceAddress { get; set; }
        public string IdMaps { get; set; }
        public double Lat { get; set; }
        public double Lang { get; set; }

    }
}
