using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using mhcb.syd.Interface;

namespace mhcb.syd.AppCentre.Controllers.api
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/FacilityLoanTrans")]
    public class FacilityMasterLoanTransController : ApiController
    {
        private IFacilityMasterLoanTrans _repository;
        public FacilityMasterLoanTransController(IFacilityMasterLoanTrans repository)
        {
            _repository = repository;
        }

        //[BasicAuthentication]
        [Authorize]
        [Route("getLoanTransactionByDates")]
        [HttpGet]
        public HttpResponseMessage getLoanTransactionByDates(string dateFrom, string dateTo)
        {
            var entry = _repository.getLoanTransactionByDates(dateFrom, dateTo);

            if (entry != null && entry.Count() != 0)
            {
                // HTTP/1.1 200 OK  
                return Request.CreateResponse(HttpStatusCode.OK, entry);
            }
            else
            {
                // HTTP/1.1 404 Not found  
                if (dateFrom != null && dateTo != null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Loan Transaction between start dates : " + dateFrom.ToString() + " and " + dateTo.ToString() + " not found");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                             "Loan Transaction not found");
                }               
            }
        }

   
        //[BasicAuthentication]
        [Authorize]
        [Route("getLoanTransactionByDatesCcy")]
        [HttpGet]
        public HttpResponseMessage getLoanTransactionByDatesCcy(string dateFrom, string dateTo, string ccy)
        {
            var entry = _repository.getLoanTransactionByCcyDates(dateFrom, dateTo, ccy);

            if (entry != null && entry.Count() != 0)
            {
                // HTTP/1.1 200 OK  
                return Request.CreateResponse(HttpStatusCode.OK, entry);
            }
            else
            {
                // HTTP/1.1 404 Not found  
                if (dateFrom != null && dateTo != null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "Loan Transaction between start dates : " + dateFrom.ToString() + " and " + dateTo.ToString() + " not found");
                } else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "Loan Transaction not found");
                }
            }
        }
    }
}
