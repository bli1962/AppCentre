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
	[RoutePrefix("api/FXSettlementInf")]
	public class FXSettlementInfController : ApiController
    {
        //http://localhost:62001//api/FXSettlementInf/
        [EnableCors("*", "*", "*")]
        //[BasicAuthentication]
        //[Authorize]
        [Route("Pending")]
        [HttpGet]
        public HttpResponseMessage GetPendingTrans()
        {
            var entry = FXSettlementInfBusiness.GetPendingTrans();

            if (entry != null && entry.Count() != 0)
            {
                // HTTP/1.1 200 OK  
                return Request.CreateResponse(HttpStatusCode.OK, entry);
            }
            else
            {
                // HTTP/1.1 404 Not found  
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Pending FX Settlemennts not found");
            }
        }


        ////http://localhost:62001//api/FXSettlementInf/?dateFrom=2020-04-01&dateTo=2021-03-31
        //[EnableCors("*", "*", "*")]
        ////[BasicAuthentication]
        ////[Authorize]
        //[Route("")]
        //[HttpGet]
        //public HttpResponseMessage GetFxTransactioByDates(string dateFrom, string dateTo)
        //{
        //    var entry = FXTransactionBusiness.getFxTransactioByDates(dateFrom, dateTo);

        //    if (entry != null && entry.Count() != 0)
        //    {
        //        // HTTP/1.1 200 OK  
        //        return Request.CreateResponse(HttpStatusCode.OK, entry);
        //    }
        //    else
        //    {
        //        // HTTP/1.1 404 Not found  
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
        //            "FX Transactions between contract date " + dateFrom.ToString() + " and " + dateTo.ToString() + " not found");
        //    }
        //}


        // http://localhost:62001//api/FXSettlementInf/?DeliveryDate=2020-05-29
        [EnableCors("*", "*", "*")]
		//[BasicAuthentication]
		//[Authorize]
		[Route("")]
		[HttpGet]
		public HttpResponseMessage GetfxDeliveryCorpByDate(string deliveryDate)
		{
			var entry = FXSettlementInfBusiness.getfxDeliveryCorpByDate(deliveryDate);

			if (entry != null && entry.Count() != 0)
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else
			{
                if (deliveryDate != null)
                {  
                    // HTTP/1.1 404 Not found  
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "FX Settlemennt details on " + deliveryDate.ToString() + " not found");
                }
                else
                {
                    // HTTP/1.1 404 Not found  
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "FX Settlemennt not found");
                }
                  
			}		
		}

		// http://localhost:62001//api/FXSettlementInf/?DeliverySummryDate=2020-05-29
		[EnableCors("*", "*", "*")]
		//[BasicAuthentication]
		//[Authorize]
		[Route("")]
		[HttpGet]
		public HttpResponseMessage GetfxDeliveryCorpSummaryByDate(string deliverySummryDate)
		{
			var entry = FXSettlementInfBusiness.getfxDeliveryCorpSummaryByDate(deliverySummryDate);

			if (entry != null && entry.Count() != 0)
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else
			{
                // HTTP/1.1 404 Not found  
                if (deliverySummryDate != null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                         "FX Settlemennt details Summary on " + deliverySummryDate.ToString() + " not found");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                             "FX Settlemennt details Summary  not found");
                }
                  
			}
			
		}


		//GET:  http://localhost:62001//api/FXSettlementInf/?refNo=FSS751379854
		[EnableCors("*", "*", "*")]
		//[BasicAuthentication]
		//[Authorize]
		[Route("")]
		[HttpGet]
		public HttpResponseMessage LoadFXSettlementByRefNo(string refNo)
		{
			var entry = FXSettlementInfBusiness.LoadFXSettlementByRefNo(refNo);

			if (entry != null )
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else
			{
				// HTTP/1.1 404 Not found  
				return Request.CreateErrorResponse(HttpStatusCode.NotFound,
							"FX Settlemennt of Ref No : " + refNo.ToString() + " not found");
			}
			
		}

		//http://localhost:62001//api/FXSettlementInf/?REF_NO=FSS751379854=F&GIP_DESCRIPTION=Approved&GIP_STATUS=15
		[EnableCors("*", "*", "*")]
		//[BasicAuthentication]
		//[Authorize]
		[Route("")]
		[HttpPut]
		public HttpResponseMessage Put([FromBody] FXTranStatus status)
		{
			var entry = FXSettlementInfBusiness.updateStatus(status);

			if (entry == "OK")
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else if (entry == "NotFound")
			{
				// HTTP/1.1 404 Not found  
				return Request.CreateErrorResponse(HttpStatusCode.NotFound,
						"FX Settlemennt of Ref No : " + status.REF_NO.ToString() + " not found");
			}
			else
			{
				// HTTP/1.1 500	Internal Server Error
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, entry);
			}
			
		}
	}
}
