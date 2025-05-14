using DAL.models;
using DTO.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IShopDAL
    {
        List<ShopsDTO> GetAll();
        List<ShopsDTO> GetByProviderId(string id);
       List<ShopsDTO> Delete(int id);
        List<ShopsDTO> Add(ShopsDTO p);
        List<ShopsDTO> Update(ShopsDTO p);
        ShopsDTO GetById(int id);

    }
}
