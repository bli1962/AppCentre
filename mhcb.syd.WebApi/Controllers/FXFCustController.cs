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
    [RoutePrefix("api/FXFCust")]
    public class FXFCustController : ApiController
    {
		private IFXFCust _repository;
		public FXFCustController(IFXFCust repository)
		{
			_repository = repository;
		}

		//[BasicAuthentication]
		[Authorize]
		[Route("getMMCustByAbbr")]
		[HttpGet]
		public HttpResponseMessage LoadFXCustByCustAbbr(string abbreviation, string branchNo)
		{
			var entry = _repository.LoadFXCustByCustAbbr(abbreviation);

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


		//[BasicAuthentication]
		[Authorize]
		[Route("")]
		[HttpPut]
		public HttpResponseMessage Put([FromBody] FXFCustAttribute custAttr)
		{
			var entry = _repository.updateStatus(custAttr);
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
