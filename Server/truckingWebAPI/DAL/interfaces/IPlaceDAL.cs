using DAL.models;
using DTO.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.interfaces
{
    public interface IPlaceDAL
    {
        List<PlacesDTO> GetAll();
        PlacesDTO GetById(int id);
        List<PlacesDTO> Delete(int id);
        List<PlacesDTO> Add(PlacesDTO p);
        List<PlacesDTO> Update(PlacesDTO p);
    }
}
