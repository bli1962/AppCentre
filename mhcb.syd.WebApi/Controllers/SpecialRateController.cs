using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using mhcb.syd.DataAccess;
using mhcb.syd.DataAccess.View;
using mhcb.syd.Interface;

namespace mhcb.syd.AppCentre.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/SpecialRate")]
    public class SpecialRateController : ApiController
    {
        private ISpecialRate _repository;
        public SpecialRateController(ISpecialRate repository)
        {
            _repository = repository;
        }

        //[BasicAuthentication]
        [Authorize]
        [Route("getMizuhoRates")]
        [HttpGet]
        public HttpResponseMessage LoadSpcialRate()
        {
            var entry = _repository.LoadSpcialRate();

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

        
        //[BasicAuthentication]
        [Authorize]
        [Route("")]
        [HttpPut]
        public HttpResponseMessage Put([FromBody] RateAttribute rate)
        {
            var entry = _repository.updateStatus(rate);

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
