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
    [RoutePrefix("api/FXSettlementInf")]
	public class FXSettlementInfController : ApiController
    {
		private IFXSettlementInf _repository;
		public FXSettlementInfController(IFXSettlementInf repository)
		{
			_repository = repository;
		}


		//[BasicAuthentication]
		[Authorize]
        [Route("getPendingFxDelivery")]
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
                        "Pending FX Settlemennts not found");
            }
        }
      
 
		//[BasicAuthentication]
		[Authorize]
		[Route("getFxDeliveryCorpByDate")]
		[HttpGet]
		public HttpResponseMessage GetfxDeliveryCorpByDate(string deliveryDate)
		{
			var entry = _repository.getfxDeliveryCorpByDate(deliveryDate);

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

		
		//[BasicAuthentication]
		[Authorize]
		[Route("getFxDeliveryCorpSummaryByDate")]
		[HttpGet]
		public HttpResponseMessage GetfxDeliveryCorpSummaryByDate(string deliverySummryDate)
		{
			var entry = _repository.getfxDeliveryCorpSummaryByDate(deliverySummryDate);

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


		//[BasicAuthentication]
		[Authorize]
		[Route("getFXSettlementByRefNo")]
		[HttpGet]
		public HttpResponseMessage LoadFXSettlementByRefNo(string refNo)
		{
			var entry = _repository.LoadFXSettlementByRefNo(refNo);

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
		//[BasicAuthentication]
		[Authorize]
		[Route("")]
		[HttpPut]
		public HttpResponseMessage Put([FromBody] FXTranStatus status)
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
