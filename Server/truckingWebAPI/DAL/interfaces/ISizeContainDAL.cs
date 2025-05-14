using System;
using System.Collections.Generic;
using System.Text;
using DAL.models;
using DTO.models;

namespace DAL.interfaces
{
    public interface ISizeContainDAL
    {
        List<SizeContainDTO> GetAll();
        SizeContainDTO GetById(int id);
        List<SizeContainDTO> Delete(int id);
        List<SizeContainDTO> Add(SizeContainDTO p);
        List<SizeContainDTO> Update(SizeContainDTO p);
    }
}
