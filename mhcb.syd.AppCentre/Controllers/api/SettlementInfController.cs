using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using mhcb.syd.DataAccess.View;
using mhcb.syd.DataAccess;
using mhcb.syd.Interface;

namespace mhcb.syd.AppCentre.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/SettlementInf")]
    public class SettlementInfController : ApiController
    {
		private readonly ISettlementInf _repository;
		public SettlementInfController(ISettlementInf repository)
		{
			_repository = repository;
		}

		//[BasicAuthentication]
		[Authorize]
        [Route("getPendingSIF")]
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
                        "Pending FX Transactions not found");
            }
        }
    
        //[BasicAuthentication]
        [Authorize]
        [Route("getSIFById")]
        [HttpGet]
		public HttpResponseMessage LoadSettlementInfBySifId(int id)
		{
			var entry = _repository.LoadSettlementInfBySifId(id);
			if (entry != null && entry.Count() != 0)
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else
			{
				// HTTP/1.1 404 Not found  
				return Request.CreateErrorResponse(HttpStatusCode.NotFound,
					"Settlement Information of ID :" + id.ToString() + " not found");
			}
		}

	
		//[BasicAuthentication]
		[Authorize]
		//[Route("{custAbbr}")]
		[Route("getSIFByCustAbbr")]
		[HttpGet]
		public HttpResponseMessage LoadSettlementInfByCustAbbr(string custAbbr)
		{
			var entry = _repository.LoadSettlementInfByCustAbbr(custAbbr);
			if (entry != null && entry.Count() != 0)
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else
			{
				// HTTP/1.1 404 Not found  
				return Request.CreateErrorResponse(HttpStatusCode.NotFound,
					"Settlement Information of Customer abbrivation " + custAbbr.ToString() + " not found");
			}
		}


		//[BasicAuthentication]
		[Authorize]
		//[Route("{custAbbr}/{ccyCD}/{recordKind}")]		
		[Route("getSIFByCustAbbrCcyCDRecKind")]
		[HttpGet]
		public HttpResponseMessage LoadSettlementInfByDetails(string custAbbr, string ccyCD, string recordKind)
		{
			var entry = _repository.LoadSettlementInfByDetails(custAbbr, ccyCD, recordKind);
			if (entry != null && entry.Count() != 0)
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else
			{
				// HTTP/1.1 404 Not found  
				return Request.CreateErrorResponse(HttpStatusCode.NotFound,
					"Settlement Information of Customer : " + custAbbr.ToString() + " not found");
			}
		}

	
		//[BasicAuthentication]
		[Authorize]
        [Route("getSIFByStatus")]
        [HttpGet]
		public HttpResponseMessage LoadSettlementInfsByStatus(string status = "All")
		{
			var entry = _repository.LoadSettlementInfsByStatus(status);

			if (entry != null && entry.Count() != 0)
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else
			{ 
				// HTTP/1.1 404 Not found  
				return Request.CreateErrorResponse(HttpStatusCode.NotFound,
					"Settlement Information with status : " + status.ToString() + " not found");
			}
		}

		
		//[BasicAuthentication]
		//[Authorize]
		//[Route("")]
		//[HttpPost]
		//public HttpResponseMessage InsertSettlementInf([FromBody] SETTLEMENT_INF settlement_inf)
		//{
		//	var entry = _repository.InsertSettlementInf(settlement_inf);
		//	if (entry != "")
		//	{
		//		// HTTP/1.1 200 OK  
		//		return Request.CreateResponse(HttpStatusCode.OK, 
		//					"New Settlement Information of Swift ID : " + entry);
		//	}
		//	else
		//	{
		//		// HTTP/1.1 500	Internal Server Error
		//		return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, entry);
		//	}
		//}

		
		//[BasicAuthentication]
		[Authorize]
		[Route("")]
		[HttpDelete]
		public HttpResponseMessage DeleteSettlementInf(int id)
		{
			var entry = _repository.DeleteSettlementInf(id);

			if (entry == "OK")
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, 
						"Settlement SIF with Id = " + id.ToString() + " is deleted");
			}
			else if (entry == "NotFound")
			{
				// HTTP/1.1 404 Not found  
				return Request.CreateErrorResponse(HttpStatusCode.NotFound,
							"Settlement SIF with Id = " + id.ToString() + " not found");
			}
			else
			{
				// HTTP/1.1 500	Internal Server Error
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, entry);
			}
		}


		//public HttpResponseMessage Put(int id, [FromBody]Employee employee)  // Parameter binding, default 
		//public HttpResponseMessage Put([FromBody]int id, [FromUri]Employee employee)  
		//public HttpResponseMessage Put(int id, [FromUri] SETTLEMENT_INF settlement_inf)
		//public HttpResponseMessage Put(int id, [FromBody]SETTLEMENT_INF settlement_inf)
		//public HttpResponseMessage Put([FromUri] int id, [FromBody] FXTransStatus fxTransStatus)
		//GET:	http://localhost:62001/api/SettlementInf?id=3240&STATUS=F&GIP_DESCRIPTION=Approve&GIP_STATUS=8		
		//[BasicAuthentication]
		[Authorize]
		[Route("")]
		[HttpPut]
		public HttpResponseMessage Put([FromBody] SIFStatus status)
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
					"Settlement INF with Id " + status.SIF_ID.ToString() + " not found");
			}
			else
			{
				// HTTP/1.1 500	Internal Server Error
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, entry);
			}
		}
	}
}
