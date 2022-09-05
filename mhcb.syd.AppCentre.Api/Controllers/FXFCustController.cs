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
	[RoutePrefix("api/FXFCust")]
    public class FXFCustController : ApiController
    {
		//GET:  http://localhost:62001//api/FXFCust/?abbreviation=FUJISGSG
		[EnableCors("*", "*", "*")]
		//[BasicAuthentication]
		//[Authorize]
		[Route("")]
		[HttpGet]
		public HttpResponseMessage LoadFXCustByCustAbbr(string abbreviation, string branchNo)
		{
			var entry = FXFCustBusiness.LoadFXCustByCustAbbr(abbreviation);

			if (entry != null && entry.Count() != 0)
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else
			{
				// HTTP/1.1 404 Not found  
				return Request.CreateErrorResponse(HttpStatusCode.NotFound,
					"Customer of " + abbreviation.ToString() + " not found");
			}
		}

		//TEST  http://localhost:62001//api/FXFCust/?abbreviation=FUJISGSG
		[EnableCors("*", "*", "*")]
		//[BasicAuthentication]
		//[Authorize]
		[Route("")]
		[HttpPut]
		public HttpResponseMessage Put([FromBody] FXFCustAttribute custAttr)
		{
			var entry = FXFCustBusiness.updateStatus(custAttr);
			if (entry == "OK")
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else if (entry == "NotFound")
			{
				// HTTP/1.1 404 Not found  
				return Request.CreateErrorResponse(HttpStatusCode.NotFound,
						"Customer of abbrivation :" + custAttr.Abbreviation.ToString() + " not found to update");
			}
			else
			{
				// HTTP/1.1 500	Internal Server Error
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, entry);
			}
			
		}

	}
}
