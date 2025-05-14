using BLL.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Algoritm
{
    public interface DataModelVertexInterface
    {
        
        
        public List<List<int>> distanceMatrix { get; set; }
        public List<int> demondsShop { get; set; }
        public void InsertFormDBByProvider(string providerId);
    }
}
