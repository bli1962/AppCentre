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


namespace mhcb.syd.DataService.Controllers.api
{
    //[EnableCors("*", "*", "*")]
    [RoutePrefix("api/Payment")]
    public class PaymentController : ApiController
    {
        //GET:  http://localhost:62001//api/Payment/?dateValue=2020-05-01&dateType=ReleasedDate
        [EnableCors("*", "*", "*")]
        //[BasicAuthentication]
        //[Authorize]
        [Route("")]
        [HttpGet]
        public HttpResponseMessage getPaymentControlPaymentByDate(string dateValue, string dateType)
        {
            var entry = PaymentControlBusiness.getPaymentControlPaymentByDate(dateValue, dateType);

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
        [EnableCors("*", "*", "*")]
        //[BasicAuthentication]
        //[Authorize]
        [Route("")]
        [HttpPut]
        public HttpResponseMessage Put([FromBody] SwiftPaymentStatus status)
        {
            var entry = PaymentControlBusiness.UpdateStatus(status);

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