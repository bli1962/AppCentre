
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using mhcb.syd.DataAccess.View;
using mhcb.syd.Interface;
using System.Linq;

namespace mhcb.syd.AppCentre.Controllers.api
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/MoneyMarketTicket")]
    public class MoneyMarketTicketController : ApiController
    {
        private readonly IMoneyMarketTicket _repository;
        public MoneyMarketTicketController(IMoneyMarketTicket repository)
        {
            _repository = repository;
        }

        //[BasicAuthentication]
        [Authorize]
        [Route("getMMGID")]
        [HttpGet]
        public HttpResponseMessage GetPendingTrans()
        {
            var entry = _repository.GetPendingTrans();

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


        //[BasicAuthentication]
        [Authorize]
        [Route("getMMTransByDates")]
        [HttpGet]
        public HttpResponseMessage GetMoneyMarketTransactionByDates(string dateFrom, string dateTo)
        {
            var entry = _repository.GetMoneyMarketTransactionByDates(dateFrom, dateTo);

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
        //[BasicAuthentication]
        [Authorize]
        [Route("")]
        [HttpPut]
        public HttpResponseMessage Put([FromBody] MMGIDStatus status)
        {
            var entry = _repository.updateStatus(status);

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
