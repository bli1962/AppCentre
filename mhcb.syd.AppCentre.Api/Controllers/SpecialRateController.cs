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
    [RoutePrefix("api/SpecialRate")]
    public class SpecialRateController : ApiController
    {

        //GET:  http://localhost:62001//api/SpecialRate/
        [EnableCors("*", "*", "*")]
        //[BasicAuthentication]
        //[Authorize]
        [Route("")]
        [HttpGet]
        public HttpResponseMessage LoadSpcialRate()
        {
            var entry = SpecialRateBusiness.LoadSpcialRate();

            if (entry != null && entry.Count() != 0)
            {
                // HTTP/1.1 200 OK  
                return Request.CreateResponse(HttpStatusCode.OK, entry);
            }
            else
            {
                // HTTP/1.1 404 Not found  
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Special Rates not found");
            }
        }


        [EnableCors("*", "*", "*")]
        //[BasicAuthentication]
        //[Authorize]
        [Route("")]
        [HttpPut]
        public HttpResponseMessage Put([FromBody] RateAttribute rate)
        {
            var entry = SpecialRateBusiness.updateStatus(rate);

            if (entry == "OK")
            {
                // HTTP/1.1 200 OK  
                return Request.CreateResponse(HttpStatusCode.OK, entry);
            }
            else if (entry == "NotFound")
            {
				// HTTP/1.1 404 Not found  
				return Request.CreateErrorResponse(HttpStatusCode.NotFound,
					    "MIZUHO Special rate of CCY " + rate.CcyCode + " not found ");
			}
            else
            {
                // HTTP/1.1 500	Internal Server Error
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, entry);
            }
        }
    }
}
