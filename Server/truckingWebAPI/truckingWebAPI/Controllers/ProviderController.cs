using DAL;
using DAL.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.interfaces;
using DAL.classes;
using BLL.classes;
using BLL.interfaces;
using DTO.models;

namespace truckingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    
    public class ProviderController : ControllerBase
    {
        IproviderBLL iproviderBll;
       
        public ProviderController(IproviderBLL p)
        {
            iproviderBll = p;
        }
        [HttpGet("getAll")]

        public ActionResult<List<ProviderDTO>> get()
        {
            return iproviderBll.GetAll();
        }
        [HttpDelete("delete")]
        public ActionResult<List<ProviderDTO>> delete(string id)
        {
            return iproviderBll.Delete(id);
        }
        [HttpPost("add")]
        public ActionResult<ProviderDTO> Add(string LeavingTime, ProviderDTO p)
        {
            p.LeavingTime = TimeSpan.Parse(LeavingTime);
            return iproviderBll.Add(p);
        }
        [HttpPut("update")]
        public ActionResult <ProviderDTO> Update(string LeavingTime, ProviderDTO p)
        {
            // p.LeavingTime = new TimeSpan(p.LeavingTime.Hours,p.LeavingTime.Minutes,p.LeavingTime.Seconds);
            p.LeavingTime = TimeSpan.Parse(LeavingTime);
            return iproviderBll.Update(p);
        }
        [HttpGet("getById")]
        public ActionResult<ProviderDTO> getBiId(string id)
        {
            return iproviderBll.GetById(id);
        }


    }
}
