using BLL.algoritm;
using BLL.interfaces;
using BLL.OR_Tools;
using DAL.models;
using DTO.models;
using DTO.viewMap;
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
    public class StationsInShopController : ControllerBase
    {
        IStationsInShopBLL stationsInShopBLL;
        IFindWay findWay;
        public StationsInShopController(IStationsInShopBLL p, IFindWay findWay)
        {
            stationsInShopBLL = p;
            this.findWay = findWay;
        }
        [HttpGet("getAll")]
        public ActionResult<List<StationsInShopDTO>> get()
        {
            return stationsInShopBLL.GetAll();
       
        }
        [HttpGet("getByDriverId")]
        public ActionResult<List<StationsInShopDTO>> getByDriverId(string licensePlateTruck)
        {
            //Maneger m=new Maneger(providerId)
            // findWay.CalculateRoute(providerId);
            return stationsInShopBLL.GetAllbyLicensePlateTruck(licensePlateTruck);
        }
        [HttpDelete("delete")]
        public ActionResult<List<StationsInShopDTO>> delete(int id)
        {
            return stationsInShopBLL.Delete(id);
        }
        [HttpPost("add")]
        public ActionResult<List<StationsInShopDTO>> Add(StationsInShopDTO p)
        {
            return stationsInShopBLL.Add(p);
        }
        [HttpPut("update")]
        public ActionResult<List<StationsInShopDTO>> Update(StationsInShopDTO p)
        {
            return stationsInShopBLL.Update(p);
        }
        [HttpGet("getById")]
        public ActionResult<StationsInShopDTO> getBiId(int id)
        {

            return stationsInShopBLL.GetById(id);
        }
        [HttpGet("getAllByProviderId")]
        public ActionResult<List<StationsInShopDTO>> GetAllByProviderId(string providerId)
        {

            return stationsInShopBLL.GetAllByProviderId(providerId);
        }

        [HttpGet("GetStationInMap")]
        public List<StationInMap> GetStationInMap(string providerId)
        {
            return stationsInShopBLL.GetStationInMap(providerId);
        }


        [HttpGet("GetStationOfDriver")]
        public List<StationOfDriver> GetStationOfDriver(string licensePlateTruck)
        {
            return stationsInShopBLL.GetStationOfDriver(licensePlateTruck);
        }

    }
}
