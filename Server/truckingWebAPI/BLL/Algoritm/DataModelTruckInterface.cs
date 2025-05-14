using DTO.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Algoritm
{
    public interface DataModelTruckInterface
    {
        public List<int> capacityTruks { get; set; }

        public List<TrucksDTO> truckSource { get; set; }
        public int numOfTruck { get; set; }
        public void InsertDatFromDBByProviderId(string providerId);
    }
}
