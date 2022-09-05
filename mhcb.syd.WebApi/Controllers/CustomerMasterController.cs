using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using mhcb.syd.Interface;

namespace mhcb.syd.AppCentre.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/CustomerMaster")]
	public class CustomerMasterController : ApiController
	{
		private ICustomerMaster _repository;
		public CustomerMasterController(ICustomerMaster repository)
		{
			_repository = repository;
		}

		//[BasicAuthentication]
		[Authorize]
		[Route("")]
		[HttpGet]
		public HttpResponseMessage LoadCustomerMasterByCustAbbr(string custAbbr, string branchNo)
		{
			var entry = _repository.LoadCustomerMasterByCustAbbr(custAbbr, branchNo);

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
