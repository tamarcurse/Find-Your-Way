using DAL.models;
using DTO.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.interfaces
{
    public interface ISizeContainsBLL
    {
        List<SizeContainDTO> GetAll();
        SizeContainDTO GetById(int id);
        List<SizeContainDTO> Delete(int id);
        List<SizeContainDTO> Add(SizeContainDTO p);
        List<SizeContainDTO> Update(SizeContainDTO p);
    }
}
