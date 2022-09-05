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
	[RoutePrefix("api/FXTransaction")]
	public class FXTransactionController : ApiController
    {

        //GET:  http://localhost:62001//api/FXTransaction/Pending
        [EnableCors("*", "*", "*")]
        //[BasicAuthentication]
        //[Authorize]
        [Route("Pending")]
        [HttpGet]
        public HttpResponseMessage GetPendingTrans()
        {
            var entry = FXTransactionBusiness.GetPendingTrans();

            if (entry != null && entry.Count() != 0)
            {
                // HTTP/1.1 200 OK  
                return Request.CreateResponse(HttpStatusCode.OK, entry);
            }
            else
            {
                // HTTP/1.1 404 Not found  
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Pending FX Transactions not found");
            }
        }


        //GET:  http://localhost:62001//api/FXTransaction/?dateFrom=2020-04-01&dateTo=2021-03-31&dateType=DelivaryDate&custAbbr=Mizuho
        [EnableCors("*", "*", "*")]
        //[BasicAuthentication]
        //[Authorize]
        [Route("")]
        [HttpGet]
        public HttpResponseMessage GetFXTransactionByDatesCustAbbr(string dateFrom, string dateTo, string dateType, string custName)
        {
            var entry = FXTransactionBusiness.GetFXTransactionByDatesCustAbbr(dateFrom, dateTo, dateType, custName);

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
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "FX Transactions between start dates : " + dateFrom.ToString() + " and " + dateTo.ToString() + " not found");
                   
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "FX Transactions not found");
                }
                    
            }
        }


        //http://localhost:62001//api/FXTransaction/?dateFrom=2020-04-01&dateTo=2021-03-31
        [EnableCors("*", "*", "*")]
        //[BasicAuthentication]
        //[Authorize]
        [Route("")]
        [HttpGet]
        public HttpResponseMessage GetFxTransactioByDates(string dateFrom, string dateTo)
        {
            var entry = FXTransactionBusiness.getFxTransactioByDates(dateFrom, dateTo);

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
                     "FX Transactions between contract date " + dateFrom.ToString() + " and " + dateTo.ToString() + " not found");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "FX Transactions not found");
                }
               
            }
        }



        //GET:  http://localhost:62001//api/FXTransaction/?refNo=FSS751379854
        [EnableCors("*", "*", "*")]
		//[BasicAuthentication]
		//[Authorize]
		[Route("")]
		[HttpGet]
		public HttpResponseMessage LoadFXTransactionByRefNo(string refNo)
		{
			var entry = FXTransactionBusiness.LoadFXTransactionByRefNo(refNo);

			if (entry != null )
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else
			{
				// HTTP/1.1 404 Not found  
				return Request.CreateErrorResponse(HttpStatusCode.NotFound,
						"FX Transaction of Ref No :" + refNo.ToString() + " not found");
			}
		}



        //http://localhost:62001//api/FXTransaction/?REF_NO=FSS751379854=F&GIP_DESCRIPTION=Approved&GIP_STATUS=15
        [EnableCors("*", "*", "*")]
		//[BasicAuthentication]
		//[Authorize]
		[Route("")]
		[HttpPut]
		public HttpResponseMessage Put([FromBody] FXTranStatus status)
		{
			var entry = FXTransactionBusiness.updateStatus(status);

			if (entry == "OK")
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else if (entry == "NotFound")
			{
				// HTTP/1.1 404 Not found  
				return Request.CreateErrorResponse(HttpStatusCode.NotFound,
						"FX Transaction with Ref Noe " + status.REF_NO.ToString() + " not found");
			}
			else
			{
				// HTTP/1.1 500	Internal Server Error
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, entry);
			}
		}
	}
}
