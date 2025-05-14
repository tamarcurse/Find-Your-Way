using BLL.algoritm;
using BLL.Algoritm;
using BLL.interfaces;
using DTO.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.OR_Tools;
using BLL.googleMaps;

namespace truckingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputeSolutionController : ControllerBase
    {
        ManegerInterface maneger;
        IFindWay findWay;
        DistanceMatrixInterface distanceMatrix;
        

        public ComputeSolutionController(ManegerInterface maneger, IFindWay findWay, DistanceMatrixInterface distanceMatrix)
        {
            this.maneger = maneger;
            this.findWay = findWay;
            this.distanceMatrix = distanceMatrix;
        }

        [HttpGet("getSolutionByProviderId")]
        public ActionResult<string> getSolutionByProviderId(string providerId)
        {

            return maneger.CumputeByProvider(providerId).ToString();
           
        }
        [HttpGet("getTest")]
        public ActionResult<string> getTest()
        {
            string[] address = new string[9];
            address[0] = "ChIJB94988BeHBURIGvcyuGjtSU";
            address[1] = "EiJNZXNoZWtoIEtob2tobWEgU3QgMSwgTW9kaSdpbiBJbGl0IjASLgoUChIJ59MG2qbSAhURBn5e0EE9n88QASoUChIJC_sT8qDSAhURZLHPIAvqyeo";
            address[2] = "ChIJAa6V6yE_AhURjyBESg85Ogk";
            address[3] = "ChIJZ2n4kWNmAhURsop_TjVOzyk";
            address[4] = "EidMb2toYW1laSBoYS1HZXRhJ290IFN0IDEsIFNhZmVkLCBJc3JhZWwiMBIuChQKEgmXs6-FmCMcFRH-EO4yvLFPRxABKhQKEgmH5_DRmCMcFRFrvPndrsqZ7Q";
            address[5] = "ChIJveE0rz5KHRURQgiesBxr_GM";
            address[6] = "ChIJrQPPU7xMHRURgnTWJFAfHOg";
            address[7] = "ChIJL5zy_YZMHRUR8swlJ6RN3n8";
            address[8] = "ChIJTZ5cNgliAhURNUS8HbV_Tm4";
            address[9] = "ChIJuVMHo3M2HRURPETBIx9l4M0";
            address[10] = "ChIJRegNdUy6HRURmlKBKpgjXcM";
            address[11] = "ChIJVwGsfwc0HRURQ3RMtLrVinU";
            address[12] = "EidIYVJhdiBLYWhhbm1hbiBTdCAxMCwgQm5laSBCcmFrLCBJc3JhZWwiMBIuChQKEgmjta98IkodFRFKIcHJAwOvnRAKKhQKEgkL4BAyFkodFRGvzBHk6z9q4Q";
            address[13] = "ChIJT_x1AIOB0IkRhcd1YOHXJXk";
            address[14] = "ChIJ3Y9BISVKHRURFPqXNn5Z0ro";
            //address[15] =
            //address[16]=
            //address[17]=
            //address[18]=
            //address[19]=
            //address[20]=

            distanceMatrix.CreateTimeAndDistanceMatrix("ChIJ3Y9BISVKHRURFPqXNn5Z0ro",new List<string>( address));
            return null;
        }

        [HttpGet("getSolutionByProviderIdOR-Tools")]
        public void getSolutionByProviderIdOrTools(string providerId)
        {

             findWay.CalculateRoute(providerId);

        }
    }
}
