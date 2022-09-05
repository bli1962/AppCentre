using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using mhcb.syd.DataAccess.View;
using mhcb.syd.Interface;

namespace mhcb.syd.AppCentre.Controllers.api
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/EDocument")]
	public class EDocumentController : ApiController
	{
        private readonly IEDocument _repository;
        public EDocumentController(IEDocument repository)
        {
            _repository = repository;
        }

        //[BasicAuthentication]
        [Authorize]
        [Route("GetPendingEDoc")]
        [HttpGet]
        public HttpResponseMessage GetPendingTrans(string eDocType, string optDate)
        {
            var entry = _repository.GetPendingTrans(eDocType, optDate);

            if (entry != null && entry.Count() != 0)
            {
                // HTTP/1.1 200 OK  
                return Request.CreateResponse(HttpStatusCode.OK, entry);
            }
            else
            {
                // HTTP/1.1 404 Not found  
                if (optDate != null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                       "eDocuments of : " + eDocType.ToString() + " on date of " + optDate + " not found");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                       "eDocuments of : " + eDocType.ToString() + " not found");
                }             
            }
        }

        
		//[BasicAuthentication]
		[Authorize]
		[Route("")]
		[HttpPut]
		public HttpResponseMessage Put([FromBody] EDocumentAttribute eDocAttribute)
		{
			var entry = _repository.updateStatus(eDocAttribute);

			if (entry == "OK")
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else if (entry == "NotFound")
            {
                // HTTP/1.1 404 Not found  
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "eDocument of : " + eDocAttribute.ReportType.ToString() + " ID: " + eDocAttribute.Id + " not found ");
            }
            else
            {
                // HTTP/1.1 500	Internal Server Error
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, entry);
            }
        }
	}
}


