using DTO.Algoritm;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Algoritm
{
    public interface AllVertexInterface
    {
        public List<Vertex> vertexList { get; set; }
        public List<int> endOfTruckList { get; set; }
        public DataModelVertexInterface dataVertex { get; set; }
        public void InitAllVertex(string providerId);
        public void DivideVertexOfGroup(int newVertex);
        public int GetVertexMin();
        public bool CheckIfPerIsBestClosed(int vertexId, int newVertex);
        public int GetVertexNotGroup();
    }
}
