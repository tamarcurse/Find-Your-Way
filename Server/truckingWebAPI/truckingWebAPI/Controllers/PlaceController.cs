using BLL.interfaces;
using DAL.models;
using DTO.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace truckingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        IPlaceBLL PlaceBll;
        public PlaceController(IPlaceBLL p)
        {
            PlaceBll = p;
        }
        [HttpGet("getAll")]
        public ActionResult<List<PlacesDTO>> get()
        {
            return PlaceBll.GetAll();
        }
        [HttpDelete("delete")]
        public ActionResult<List<PlacesDTO>> delete(int id)
        {
            return PlaceBll.Delete(id);
        }
        [HttpPost("add")]
        public ActionResult<List<PlacesDTO>> Add(PlacesDTO p)
        {
            return PlaceBll.Add(p);
        }
        [HttpPut("update")]
        public ActionResult<List<PlacesDTO>> Update(PlacesDTO p)
        {
            return PlaceBll.Update(p);
        }
        [HttpGet("getById")]
        public ActionResult<PlacesDTO> getBiId(int id)
        {
            return PlaceBll.GetById(id);
        }
    }
}
