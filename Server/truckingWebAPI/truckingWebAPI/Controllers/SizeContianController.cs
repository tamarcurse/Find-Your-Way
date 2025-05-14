using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.classes;
using DAL.interfaces;
using BLL.interfaces;
using BLL.classes;
using DAL.models;
using DTO.models;

namespace truckingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeContianController : ControllerBase
    {
        ISizeContainsBLL SizeContainsBll;
        public SizeContianController(ISizeContainsBLL p)
        {
            SizeContainsBll = p;
        }
        [HttpGet("getAll")]
        public ActionResult<List<SizeContainDTO>> get()
        {
            return SizeContainsBll.GetAll();
        }
        [HttpDelete("delete")]
        public ActionResult<List<SizeContainDTO>> delete(int id)
        {
            return SizeContainsBll.Delete(id);
        }
        [HttpPost("add")]
        public ActionResult<List<SizeContainDTO>> Add(SizeContainDTO p)
        {
            return SizeContainsBll.Add(p);
        }
        [HttpPut("update")]
        public ActionResult<List<SizeContainDTO>> Update(SizeContainDTO p)
        {
            return SizeContainsBll.Update(p);
        }
        [HttpGet("getById")]
        public ActionResult<SizeContainDTO> getBiId(int id)
        {
            return SizeContainsBll.GetById(id);
        }


    }

}

