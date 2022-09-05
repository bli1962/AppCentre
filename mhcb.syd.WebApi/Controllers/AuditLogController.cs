using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using mhcb.syd.BusinessLayer;

namespace mhcb.syd.AppCentre.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/AuditLog")]
	public class AuditLogController : ApiController
	{

		//[BasicAuthentication]
		[Authorize]
		[Route("LoadAuditLogByDate")]
		[HttpGet]
		public HttpResponseMessage LoadAuditLogByDate(string optDate)
		{
			var entry = AuditLog.LoadAuditLogByDate(optDate);

			if (entry != null && entry.Count() != 0)
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else
			{
				// HTTP/1.1 404 Not found  
				return Request.CreateErrorResponse(HttpStatusCode.NotFound,
						"Audit Log of  : " + optDate.ToString() + " not found");
			}
		}
   
        //[BasicAuthentication]
        [Authorize]
        [Route("LoadAuditLogByDates")]
        [HttpGet]
        public HttpResponseMessage LoadAuditLogByDateRange(string optDateFrom, string optDateTo)
        {
            var entry = AuditLog.LoadAuditLogByDateRange(optDateFrom, optDateTo);

            if (entry != null && entry.Count() != 0)
            {
                // HTTP/1.1 200 OK  
                return Request.CreateResponse(HttpStatusCode.OK, entry);
            }
            else
            {
                // HTTP/1.1 404 Not found  
                if (optDateFrom !=null && optDateTo != null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                           "Audit Log of between " + optDateFrom.ToString() + " and " + optDateTo.ToString() + "not found");
                } else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                           "Audit Log not found");
                }
                
            }
        }


    }
}
