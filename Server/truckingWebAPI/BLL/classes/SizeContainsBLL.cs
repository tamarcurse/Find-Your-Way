using BLL.interfaces;
using DAL.models;
using System;
using System.Collections.Generic;
using System.Text;

using DAL.interfaces;
using DTO.models;

namespace BLL.classes
{
    public class SizeContainsBLL : ISizeContainsBLL
    {
        ISizeContainDAL SizeContainsDAL;
        public SizeContainsBLL(ISizeContainDAL p)
        {
            SizeContainsDAL = p;
        }
        public List<SizeContainDTO> Add(SizeContainDTO p)
        {
            return SizeContainsDAL.Add(p);
        }

        public List<SizeContainDTO> Delete(int id)
        {
            return SizeContainsDAL.Delete(id);
        }

        public List<SizeContainDTO> GetAll()
        {
            return SizeContainsDAL.GetAll();
        }

        public SizeContainDTO GetById(int id)
        {
            return SizeContainsDAL.GetById(id);
        }

        public List<SizeContainDTO> Update(SizeContainDTO p)
        {
            return SizeContainsDAL.Update(p);
        }
    }
}
