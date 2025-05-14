using DAL.models;
using DTO.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface IShopBLL
    {
        List<ShopsDTO> GetAll();
        ShopsDTO GetById(int id);
        List<ShopsDTO> Delete(int id);
        List<ShopsDTO> Add(ShopsDTO p);
        List<ShopsDTO> Update(ShopsDTO p);
        public List<ShopsDTO> GetByProviderId(string id);
    }
}
