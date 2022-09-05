using mhcb.syd.DataAccess.View;
using mhcb.syd.Interface;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace mhcb.syd.DataService.Controllers.api
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/GAS")]
    public class GASController : ApiController
    {
        private readonly IGASBusiness _repository;
        public GASController(IGASBusiness repository)
        {
            _repository = repository;
        }


		//[BasicAuthentication]
		[Authorize]
		[Route("getGASTransByDate")]
		[HttpGet]
		public HttpResponseMessage GetGASTransByDate(string optDate)
		{
			var entry = _repository.getGASTransByDate(optDate);

			if (entry != null && entry.Count() != 0)
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else
			{
				// HTTP/1.1 404 Not found  
				return Request.CreateErrorResponse(HttpStatusCode.NotFound,
							"GAS_Debit transactions on date : " + optDate + " not found");
			}
		}


		//[BasicAuthentication]
		[Authorize]
		[Route("")]
		[HttpPut]
		public HttpResponseMessage Put([FromBody] GASAttribute gasAttribute)
		{
			string entry;

			if (gasAttribute.HasDCSPayment == "Yes")
            {
				entry = _repository.UpdateDCSStatus(gasAttribute);
			} else
            {
				entry = _repository.UpdateGASStatus(gasAttribute);
			}

			if (entry == "OK")
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else if (entry == "NotFound")
			{
				// HTTP/1.1 404 Not found  
				return Request.CreateErrorResponse(HttpStatusCode.NotFound,
					"GAS_Debit of " + gasAttribute.Min_No.ToString() + " not found");
			}
			else
			{
				// HTTP/1.1 500	Internal Server Error
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, entry);
			}
		}
	}
}
