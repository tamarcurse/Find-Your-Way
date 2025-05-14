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
    public class TruckController : ControllerBase
    {
        ITrucksBLL truckBLL;
        public TruckController(ITrucksBLL p)
        {
            truckBLL = p;
        }
        [HttpGet("getAll")]
        public ActionResult<List<TrucksDTO>> get()
        {
            return truckBLL.GetAll();
        }
        [HttpGet("getAllByProviderId")]
        public ActionResult<List<TrucksDTO>> getAllByProviderId(string providerId)
        {
            return truckBLL.GetAllByProviderId(providerId);
        }
        [HttpGet("getByDriverId")]
        public ActionResult<TrucksDTO> GetByDriverId(string id)
        {
            TrucksDTO trucksDTO = truckBLL.GetByDriverId(id);
            return truckBLL.GetByDriverId(id);
        }
        [HttpDelete("delete")]
        public ActionResult<List<TrucksDTO>> delete(string id)
        {
            return truckBLL.Delete(id);
        }
        [HttpPost("add")]
        public ActionResult<List<TrucksDTO>> Add(TrucksDTO p)
        {
            return truckBLL.Add(p);
        }
        [HttpPut("update")]
        public ActionResult<List<TrucksDTO>> Update(TrucksDTO p)
        {
            return truckBLL.Update(p);
        }
        [HttpGet("getById")]
        public ActionResult<TrucksDTO> getBiId(string id)
        {
            return truckBLL.GetById(id);
        }

    }
}
