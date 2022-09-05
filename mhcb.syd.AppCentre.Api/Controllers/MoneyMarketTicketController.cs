
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using mhcb.syd.DataAccess;
using mhcb.syd.BusinessLayer;
using System.Linq;

namespace mhcb.syd.DataService.Controllers.api
{
    //[EnableCors("*", "*", "*")]
    [RoutePrefix("api/MoneyMarketTicket")]
    public class MoneyMarketTicketController : ApiController
    {

        //GET:  http://localhost:62001//api/MoneyMarketTicket/Pending
        [EnableCors("*", "*", "*")]
        //[BasicAuthentication]
        //[Authorize]
        [Route("Pending")]
        [HttpGet]
        public HttpResponseMessage GetPendingTrans()
        {
            var entry = MoneyMarketTicketBusiness.GetPendingTrans();

            if (entry != null && entry.Count() != 0)
            {
                // HTTP/1.1 200 OK  
                return Request.CreateResponse(HttpStatusCode.OK, entry);
            }
            else
            {
                // HTTP/1.1 404 Not found  
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Pending MM Transactions not found");
            }
        }

        //GET:  http://localhost:62001//api/MoneyMarketTicket/?strDateFrom=2020-04-01&strDateTo=2021-03-31
        [EnableCors("*", "*", "*")]
        //[BasicAuthentication]
        //[Authorize]
        [Route("")]
        [HttpGet]
        public HttpResponseMessage GetMoneyMarketTransactionByDates(string dateFrom, string dateTo)
        {
            var entry = MoneyMarketTicketBusiness.GetMoneyMarketTransactionByDates(dateFrom, dateTo);

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
                            "MM Transactions between start dates : " + dateFrom.ToString() + " and " + dateFrom.ToString() + " not found");

                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Pending MM Transactions not found");
                }
            }
        }


        //http://localhost:62001//api/MoneyMarketTicket/?REF_NO=SSS751378986
        [EnableCors("*", "*", "*")]
        //[BasicAuthentication]
        //[Authorize]
        [Route("")]
        [HttpPut]
        public HttpResponseMessage Put([FromBody] MMGIDStatus status)
        {
            var entry = MoneyMarketTicketBusiness.updateStatus(status);

            if (entry == "OK")
            {
                // HTTP/1.1 200 OK  
                return Request.CreateResponse(HttpStatusCode.OK, entry);
            }
            else if (entry == "NotFound")
            {
                // HTTP/1.1 404 Not found  
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "GID of MM Transaction with Ticket " + status.TicketId.ToString() + " not found");
            }
            else
            {
                // HTTP/1.1 500	Internal Server Error
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, entry);
            }
        }

    }



}
