using System;
using System.Collections.Generic;
using System.Linq;
//using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using mhcb.syd.DataAccess;
using mhcb.syd.BusinessLayer;

namespace mhcb.syd.DataService.Controllers
{
    //[EnableCors("*", "*", "*")]
    [RoutePrefix("api/CovenantMaster")]
    public class CovenantMasterController : ApiController
    {
        //GET: http://localhost:62001//api/CovenantMaster/?covenantType=Covenant
        [EnableCors("*", "*", "*")]
        //[BasicAuthentication]
        //[Authorize]
        [Route("")]
        [HttpGet]
        public HttpResponseMessage GetCovenantRatio(string covenantType)
        {
            var entry = CovenantMasterRecord.GetCovenantRatio(covenantType);

            if (entry != null && entry.Count() != 0)
            {
                // HTTP/1.1 200 OK  
                return Request.CreateResponse(HttpStatusCode.OK, entry);
            }
            else
            {
                // HTTP/1.1 404 Not found  
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Covenant Ratio Records of covenant type : " + covenantType.ToString() + " not found");
            }
        }
    }
}
