using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using mhcb.syd.BusinessLayer;

namespace mhcb.syd.DataService.Controllers.api
{
    //[EnableCors("*", "*", "*")]
    [RoutePrefix("api/RfrInterest")]
    public class FRAInterestController : ApiController
    {
        // http://localhost:62001//api/RfrInterest/Loan/?reportDate=2021-03-01
        [EnableCors("*", "*", "*")]
        //[BasicAuthentication]
        //[Authorize]
        [Route("Loan")]
        [HttpGet]
        public HttpResponseMessage GetRfrInterestForLoanByReportDate(string reportDate, string custAbbr)
        {
            var entry = RFRInterest.getRFRInterestForLoanByReportDate(reportDate, custAbbr);

            if (entry != null && entry.Count() != 0)
            {
                // HTTP/1.1 200 OK  
                return Request.CreateResponse(HttpStatusCode.OK, entry);
            }
            else
            {
                if (reportDate != null)
                {
                    // HTTP/1.1 404 Not found  
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Loan Transactions on Report Date:" + reportDate.ToString() + " not found");
                }
                else
                {
                    // HTTP/1.1 404 Not found  
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Loan Transactions on not found");
                }

            }
        }


        // http://localhost:62001//api/RfrInterest/Swap/?reportDate=2021-03-01
        [EnableCors("*", "*", "*")]
        //[BasicAuthentication]
        //[Authorize]
        [Route("Swap")]
        [HttpGet]
        public HttpResponseMessage GetRfrInterestForSwapByReportDate(string reportDate, string custAbbr)
        {
            var entry = RFRInterest.getRFRInterestForSwapByReportDate(reportDate, custAbbr);

            if (entry != null && entry.Count() != 0)
            {
                // HTTP/1.1 200 OK  
                return Request.CreateResponse(HttpStatusCode.OK, entry);
            }
            else
            {
                if (reportDate != null)
                {
                    // HTTP/1.1 404 Not found  
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Swap Loan Transactions on Report Date: " + reportDate.ToString() + " not found");
                }
                else
                {
                    // HTTP/1.1 404 Not found  
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Swap Loan Transactions on not found");
                }

            }
        }
    }
}
