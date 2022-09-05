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
	[RoutePrefix("api/BankInf")]
	public class BankInfController : ApiController
	{
        //GET:  http://localhost:62001//api/BankInf
        //[Route("")]
        //[HttpGet]
        //public HttpResponseMessage Get()
        //{
        //	using (GUIDEDBEntities entities = new GUIDEDBEntities())
        //	{
        //		var entry = entities.BANK_INF
        //				.Include(x => x.BANK_INF_CORR).ToList();

        //		if (entry != null && entry.Count != 0)
        //		{
        //			// HTTP/1.1 200 OK  
        //			return Request.CreateResponse(HttpStatusCode.OK, entry);
        //		}
        //		else
        //		{
        //			// HTTP/1.1 404 Not found  
        //			return Request.CreateErrorResponse(HttpStatusCode.NotFound,
        //				"Bank INF " + " not found");
        //		}
        //	}
        //}

        //GET:  http://localhost:62001//api/BankInf/Pending
        [EnableCors("*", "*", "*")]
        //[BasicAuthentication]
        //[Authorize]
        [Route("Pending")]
        [HttpGet]
        public HttpResponseMessage GetPendingTrans()
        {
            var entry = BankInfBusiness.GetPendingTrans();

            if (entry != null && entry.Count() != 0)
            {
                // HTTP/1.1 200 OK  
                return Request.CreateResponse(HttpStatusCode.OK, entry);
            }
            else
            {
                // HTTP/1.1 404 Not found  
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Pending Bank Information not found");
            }
        }



        //GET:  http://localhost:62001//api/BankInf/?custName=KREISSPARKASSE AACHEN&&branchNo=751
        [EnableCors("*", "*", "*")]
		//[BasicAuthentication]
		//[Authorize]
		[Route("")]
		[HttpGet]
		public HttpResponseMessage LoadBankInfByDetails(string custName, string branchNo)
		{
			var entry = BankInfBusiness.LoadBankInfByDetails(custName, branchNo);
			if (entry != null && entry.Count() != 0)
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else
			{
				// HTTP/1.1 404 Not found  
				return Request.CreateErrorResponse(HttpStatusCode.NotFound,
					"Bank Information of Customer : " + custName.ToString() + " not found");
			}
		}


		//GET:  http://localhost:62001//api/BankInf/?swiftId=AACKDE33080
		[EnableCors("*", "*", "*")]
		//[BasicAuthentication]
		//[Authorize]
		[Route("")]
		[HttpGet]
		public HttpResponseMessage LoadBankInfBySwiftID(string swiftId)
		{
			var entry = BankInfBusiness.LoadBankInfBySwiftID(swiftId);

			if (entry != null && entry.Count() != 0)
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else
			{
				// HTTP/1.1 404 Not found  
				return Request.CreateErrorResponse(HttpStatusCode.NotFound,
						"Bank Information of Swift ID : " + swiftId.ToString() + " not found");
			}
		}


		//GET:  http://localhost:62001//api/BankInf/?status=ALL
		[EnableCors("*", "*", "*")]
		//[BasicAuthentication]
		//[Authorize]
		[Route("")]
		[HttpGet]
		public HttpResponseMessage LoadBankInfsByStatus(string status = "All")
		{
			var entry = BankInfBusiness.LoadBankInfsByStatus(status);

			if (entry != null && entry.Count() != 0)
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else
			{
				// HTTP/1.1 404 Not found  
				return Request.CreateErrorResponse(HttpStatusCode.NotFound,
						"Bank Information of Status : " + status.ToString() + " not found");
			}			
		}

		//http://localhost:62001//api/BankInf/?SWIFT_ID=AABAFI22=F&GIP_DESCRIPTION=Approved&GIP_STATUS=4
		[EnableCors("*", "*", "*")]
		//[BasicAuthentication]
		//[Authorize]
		[Route("")]
		[HttpPut]
		public HttpResponseMessage Put([FromBody] BankInfStatus status)
		{
			var entry = BankInfBusiness.updateStatus(status);

			if (entry == "OK")
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else if (entry == "NotFound")
			{
				// HTTP/1.1 404 Not found  
				return Request.CreateErrorResponse(HttpStatusCode.NotFound,
						"Bank Information of Swift ID " + status.ToString() + " not found");
			}
			else
			{
				// HTTP/1.1 500	Internal Server Error
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, entry);
			}
			
		}
	}
}
