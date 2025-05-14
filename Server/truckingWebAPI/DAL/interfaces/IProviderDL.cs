using DAL;
using DAL.models;
using DTO.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.interfaces
{
    public interface IProviderDL
    {
        List<ProviderDTO> GetAll();
        ProviderDTO GetById(string id);
        List<ProviderDTO> Delete(string id);
        ProviderDTO Add(ProviderDTO p);
        ProviderDTO Update(ProviderDTO p);
    }
}
