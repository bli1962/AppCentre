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
    [RoutePrefix("api/SettlementInf")]
    public class SettlementInfController : ApiController
    {

        //GET:http://localhost:62001/api/SettlementInf
        //[HttpGet]
        //public HttpResponseMessage LoadSettlementInfs()
        ////public HttpResponseMessage Get()
        //{
        //	using (GUIDEDBEntities entities = new GUIDEDBEntities())
        //	{
        //		//return entities.Employees.ToList();
        //		var entry = entities.SETTLEMENT_INF.ToList();
        //		if (entry != null && entry.Count != 0)
        //		{
        //			// HTTP/1.1 200 OK  
        //			return Request.CreateResponse(HttpStatusCode.OK, entry);
        //		}
        //		else
        //		{
        //			// HTTP/1.1 404 Not found 
        //			return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Settlement Inf found");
        //		}
        //	}
        //}

        //GET:  http://localhost:62001//api/settlementinf/Pending
        [EnableCors("*", "*", "*")]
        //[BasicAuthentication]
        //[Authorize]
        [Route("Pending")]
        [HttpGet]
        public HttpResponseMessage GetPendingTrans()
        {
            var entry = SettlementInfBusiness.GetPendingTrans();

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


        //GET: http://localhost:62001/api/settlementinf?id=3240
        [EnableCors("*", "*", "*")]
        //[BasicAuthentication]
        //[Authorize]
        [Route("")]
        [HttpGet]
		public HttpResponseMessage LoadSettlementInfBySifId(int id)
		{
			var entry = SettlementInfBusiness.LoadSettlementInfBySifId(id);
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


		//GET: http://localhost:62001/api/SettlementInf/?custAbbr=AAPD
		[EnableCors("*", "*", "*")]
		//[BasicAuthentication]
		//[Authorize]
		//[Route("{custAbbr}")]
		[Route("")]
		[HttpGet]
		public HttpResponseMessage LoadSettlementInfByCustAbbr(string custAbbr)
		{
			var entry = SettlementInfBusiness.LoadSettlementInfByCustAbbr(custAbbr);
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


		//GET: http://localhost:62001/api/SettlementInf/?custAbbr=AAPD&&ccyCD=16&&recordKind=0
		[EnableCors("*", "*", "*")]
		//[BasicAuthentication]
		//[Authorize]
		//[Route("{custAbbr}/{ccyCD}/{recordKind}")]		
		[Route("")]
		[HttpGet]
		public HttpResponseMessage LoadSettlementInfByDetails(string custAbbr, string ccyCD, string recordKind)
		{
			var entry = SettlementInfBusiness.LoadSettlementInfByDetails(custAbbr, ccyCD, recordKind);
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


		//GET: http://localhost:62001/api/settlementinf?status=F
		[EnableCors("*", "*", "*")]
		//[BasicAuthentication]
		//[Authorize]
        [Route("")]
        [HttpGet]
		public HttpResponseMessage LoadSettlementInfsByStatus(string status = "All")
		{
			var entry = SettlementInfBusiness.LoadSettlementInfsByStatus(status);

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


		[EnableCors("*", "*", "*")]
		//[BasicAuthentication]
		//[Authorize]
		[Route("")]
		[HttpPost]
		public HttpResponseMessage InsertSettlementInf([FromBody] SETTLEMENT_INF settlement_inf)
		{
			var entry = SettlementInfBusiness.InsertSettlementInf(settlement_inf);
			if (entry != "")
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, 
							"New Settlement Information of Swift ID : " + entry);
			}
			else
			{
				// HTTP/1.1 500	Internal Server Error
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, entry);
			}
		}


		[EnableCors("*", "*", "*")]
		//[BasicAuthentication]
		//[Authorize]
		[Route("")]
		[HttpDelete]
		public HttpResponseMessage DeleteSettlementInf(int id)
		{
			var entry = SettlementInfBusiness.DeleteSettlementInf(id);
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
		[EnableCors("*", "*", "*")]
		//[BasicAuthentication]
		//[Authorize]
		[Route("")]
		[HttpPut]
		public HttpResponseMessage Put([FromBody] SIFStatus status)
		{
			var entry = SettlementInfBusiness.updateStatus(status);
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
