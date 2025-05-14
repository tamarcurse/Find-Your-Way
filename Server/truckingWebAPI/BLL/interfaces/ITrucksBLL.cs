using DAL.models;
using DTO.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface ITrucksBLL
    {
        List<TrucksDTO> GetAll();
        TrucksDTO GetById(string id);
        TrucksDTO GetByDriverId(string id);
        List<TrucksDTO> Delete(string id);
        List<TrucksDTO> Add(TrucksDTO p);
        List<TrucksDTO> Update(TrucksDTO p);
        List<TrucksDTO> GetAllByProviderId(string id);

    }
}
