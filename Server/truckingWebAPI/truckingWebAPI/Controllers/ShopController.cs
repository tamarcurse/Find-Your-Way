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
    public class ShopController : ControllerBase
    {
        IShopBLL shopBll;
        
       
        public ShopController(IShopBLL p)
        {
            shopBll = p;
          
       
        }
        [HttpGet("getAll")]
        public ActionResult<List<ShopsDTO>> get()
        {
            return shopBll.GetAll();
        }
        [HttpGet("getAllByProviderID")]
        public ActionResult<List<ShopsDTO>> getAllByProviderId(string id)
        {
            return shopBll.GetByProviderId(id);
        }
        [HttpDelete("delete")]
        public ActionResult<List<ShopsDTO>> delete(int id)
        {
            
            return shopBll.Delete(id);
            
        }
        [HttpPost("add")]
        public ActionResult<List<ShopsDTO>> Add(string startTime, string finishTime, ShopsDTO p)
        {
            p.HourDayStart = TimeSpan.Parse(startTime);
            p.HourDayFinish = TimeSpan.Parse(finishTime);
            return shopBll.Add(p);
        }
        [HttpPut("update")]
        public ActionResult<List<ShopsDTO>> Update(string startTime, string finishTime, ShopsDTO p)
        {
            p.HourDayStart = TimeSpan.Parse(startTime);
            p.HourDayFinish = TimeSpan.Parse(finishTime);
            return shopBll.Update(p);
        }
        [HttpGet("getById")]
        public ActionResult<ShopsDTO> getBiId(int id)
        {
            return shopBll.GetById(id);
        }
    }
}
