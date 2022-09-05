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
	[RoutePrefix("api/CustomerMaster")]
	public class CustomerMasterController : ApiController
	{
		//GET:  http://localhost:62001//api/CustomerMaster/?custAbbr=ABNAGB2P&&branchNo=751
		[EnableCors("*", "*", "*")]
		//[BasicAuthentication]
		//[Authorize]
		[Route("")]
		[HttpGet]
		public HttpResponseMessage LoadCustomerMasterByCustAbbr(string custAbbr, string branchNo)
		{
			var entry = CustomerMasterBusiness.LoadCustomerMasterByCustAbbr(custAbbr, branchNo);

			if (entry != null && entry.Count() != 0)
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else
			{
				// HTTP/1.1 404 Not found  
				return Request.CreateErrorResponse(HttpStatusCode.NotFound,
					"Customer name of " + custAbbr.ToString() + " not found");
			}
		}

	}
}
