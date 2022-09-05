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
    [RoutePrefix("api/BankInf")]
	public class BankInfController : ApiController
	{
		private IBankInf _repository;

		public BankInfController(IBankInf repository)
		{
			_repository = repository;
		}

		//[BasicAuthentication]
		[Authorize]
        [Route("getPendingBIF")]
        [HttpGet]
        public HttpResponseMessage GetPendingBIF()
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
                        "Pending Bank Information not found");
            }
        }

    
		//[BasicAuthentication]
		[Authorize]
		[Route("getBIFByCustName")]
		[HttpGet]
		public HttpResponseMessage LoadBankInfByDetails(string custName, string branchNo)
		{
			var entry = _repository.LoadBankInfByDetails(custName, branchNo);
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


		//[BasicAuthentication]
		[Authorize]
		[Route("getBIFBySwiftID")]
		[HttpGet]
		public HttpResponseMessage LoadBankInfBySwiftID(string swiftId)
		{
			var entry = _repository.LoadBankInfBySwiftID(swiftId);

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


		//[BasicAuthentication]
		[Authorize]
		[Route("getBIFByStatus")]
		[HttpGet]
		public HttpResponseMessage LoadBankInfsByStatus(string status = "All")
		{
			var entry = _repository.LoadBankInfsByStatus(status);

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
		//[BasicAuthentication]
		[Authorize]
		[Route("")]
		[HttpPut]
		public HttpResponseMessage Put([FromBody] BankInfStatus status)
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
