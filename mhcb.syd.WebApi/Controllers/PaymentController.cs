using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using mhcb.syd.DataAccess;
using mhcb.syd.DataAccess.View;
using mhcb.syd.Interface;

namespace mhcb.syd.AppCentre.Controllers.api
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/Payment")]
    public class PaymentController : ApiController
    {
        private IPaymentControl _repository;
        public PaymentController(IPaymentControl repository)
        {
            _repository = repository;
        }

        //[BasicAuthentication]
        [Authorize]
        [Route("getPPTransByDate")]
        [HttpGet]
        public HttpResponseMessage getPaymentControlPaymentByDate(string dateValue, string dateType)
        {
            var entry = _repository.getPaymentControlPaymentByDate(dateValue, dateType);

            if (entry != null && entry.Count() != 0)
            {
                // HTTP/1.1 200 OK  
                return Request.CreateResponse(HttpStatusCode.OK, entry);
            }
            else
            {
                // HTTP/1.1 404 Not found  
                if (dateValue != null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                       "Payment control of " + dateType + " on  the date: " + dateValue + " not found");
                } else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                       "Payment control of " + dateType + " not found");
                }
               
            }
        }


        //GET:	http://localhost:62001/api/Payment/?id=3240&STATUS=F&GIP_DESCRIPTION=Approve&GIP_STATUS=8        
        //[BasicAuthentication]
        [Authorize]
        [Route("")]
        [HttpPut]
        public HttpResponseMessage Put([FromBody] SwiftPaymentStatus status)
        {
            var entry = _repository.UpdateStatus(status);

            if (entry == "OK")
            {
                // HTTP/1.1 200 OK  
                return Request.CreateResponse(HttpStatusCode.OK, entry);
            }
            else if (entry == "NotFound")
            {
                // HTTP/1.1 404 Not found  
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "PaymentControl Id " + status.PaymentId.ToString() + " not found");
            }
            else
            {
                // HTTP/1.1 500	Internal Server Error
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, entry);
            }
        }

    }

}