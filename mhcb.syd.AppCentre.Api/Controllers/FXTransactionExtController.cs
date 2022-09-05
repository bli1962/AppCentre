
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using mhcb.syd.DataAccess;
using mhcb.syd.BusinessLayer;
using System.Linq;

namespace mhcb.syd.DataService.Controllers.api
{
    //[EnableCors("*", "*", "*")]
    [RoutePrefix("api/FXTransactionExt")]
    public class FXTransactionExtController : ApiController
    {

        //GET:  http://localhost:62001//api/FXTransactionExt/Pending
        [EnableCors("*", "*", "*")]
        //[BasicAuthentication]
        //[Authorize]
        [Route("Pending")]
        [HttpGet]
        public HttpResponseMessage GetPendingTrans()
        {
            var entry = FXTransactionExtBusiness.GetPendingTrans();

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


        //http://localhost:62001//api/FXTransactionExt/?REF_NO=SSS751378986
        [EnableCors("*", "*", "*")]
		//[BasicAuthentication]
		//[Authorize]
		[Route("")]
		[HttpPut]
		public HttpResponseMessage Put([FromBody] FXGIDStatus status)
		{
			var entry = FXTransactionExtBusiness.updateStatus(status);

			if (entry == "OK")
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else if (entry == "NotFound")
			{
				// HTTP/1.1 404 Not found  
				return Request.CreateErrorResponse(HttpStatusCode.NotFound,
					"GID of FX Transaction with Ref No " + status.REF_NO.ToString() + " not found");
			}
			else
			{
				// HTTP/1.1 500	Internal Server Error
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, entry);
			}
		}
	}
}
