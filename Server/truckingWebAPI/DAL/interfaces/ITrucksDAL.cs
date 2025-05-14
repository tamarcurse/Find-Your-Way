using DAL.models;
using DTO.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface ITrucksDAL
    {
        List<TrucksDTO> GetAll();
        TrucksDTO GetById(string id);
        List<TrucksDTO> Delete(string id);
        List<TrucksDTO> Add(TrucksDTO p);
        List<TrucksDTO> Update(TrucksDTO p);
        TrucksDTO GetByDriverId(string id);
        public List<TrucksDTO> GetAllByProviderId(string providerId);
    }
}
