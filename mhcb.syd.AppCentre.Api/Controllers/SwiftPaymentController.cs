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
    [RoutePrefix("api/SwiftPayment")]
    public class SwiftPaymentController : ApiController
    {
        //GET:  http://localhost:62001//api/SwiftPayment/?optDate=2020-05-01&dateType=CreatedDate
        [EnableCors("*", "*", "*")]
        //[BasicAuthentication]
        //[Authorize]
        [Route("")]
        [HttpGet]
        public HttpResponseMessage getSwiftPaymentByDate(string optDate, string dateType)
        {
            var entry = PaymentControlBusiness.getSwiftPaymentByDate(optDate, dateType);

            if (entry != null && entry.Count() != 0)
            {
                // HTTP/1.1 200 OK  
                return Request.CreateResponse(HttpStatusCode.OK, entry);
            }
            else
            {
                // HTTP/1.1 404 Not found  
                if (optDate != null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                       "SWIFT Payments of " + dateType + " on  the date: " + optDate + " not found");
                } else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                       "SWIFT Payments of " + dateType + " not found");
                }
               
            }
        }


        //GET:	http://localhost:62001/api/SwiftPayment/Swift?id=3240&STATUS=F&GIP_DESCRIPTION=Approve&GIP_STATUS=8
        [EnableCors("*", "*", "*")]
        //[BasicAuthentication]
        //[Authorize]
        [Route("")]
        [HttpDelete]
        public HttpResponseMessage DeleteSwiftPayment(SwiftPaymentStatus status)
        {
            var entry = PaymentControlBusiness.DeleteSwiftPayment(status);

            if (entry == "OK")
            {
                // HTTP/1.1 200 OK  
                return Request.CreateResponse(HttpStatusCode.OK, entry);
            }
            else if (entry == "NotFound")
            {
                // HTTP/1.1 404 Not found  
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "SwiftPayment Id " + status.SwiftPaymentId.ToString() + " not found");
            }
            else
            {
                // HTTP/1.1 500	Internal Server Error
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, entry);
            }
        }

    }
}
