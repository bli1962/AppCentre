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
    [RoutePrefix("api/FXTransaction")]
	public class FXTransactionController : ApiController
    {
        private IFXTransaction _repository;
        public FXTransactionController(IFXTransaction repository)
        {
            _repository = repository;
        }
   
        //[BasicAuthentication]
        [Authorize]
        [Route("getPendingFxTrans")]
        [HttpGet]
        public HttpResponseMessage GetPendingTrans()
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
                        "Pending FX Transactions not found");
            }
        }


        //[BasicAuthentication]
        [Authorize]
        [Route("getFxTransByDatesCustAbbr")]
        [HttpGet]
        public HttpResponseMessage GetFXTransactionByDatesCustAbbr(string dateFrom, string dateTo, string dateType, string custName)
        {
            var entry = _repository.GetFXTransactionByDatesCustAbbr(dateFrom, dateTo, dateType, custName);

            if (entry != null && entry.Count() != 0)
            {
                // HTTP/1.1 200 OK  
                return Request.CreateResponse(HttpStatusCode.OK, entry);
            }
            else
            {
                // HTTP/1.1 404 Not found  
                if (dateFrom != null && dateTo != null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "FX Transactions between start dates : " + dateFrom.ToString() + " and " + dateTo.ToString() + " not found");
                   
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "FX Transactions not found");
                }                   
            }
        }

  
        //[BasicAuthentication]
        [Authorize]
        [Route("getFxTransByDates")]
        [HttpGet]
        public HttpResponseMessage GetFxTransactioByDates(string dateFrom, string dateTo)
        {
            var entry = _repository.getFxTransactioByDates(dateFrom, dateTo);

            if (entry != null && entry.Count() != 0)
            {
                // HTTP/1.1 200 OK  
                return Request.CreateResponse(HttpStatusCode.OK, entry);
            }
            else
            {

                // HTTP/1.1 404 Not found  
                if (dateFrom != null && dateTo != null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                     "FX Transactions between contract date " + dateFrom.ToString() + " and " + dateTo.ToString() + " not found");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "FX Transactions not found");
                }              
            }
        }

   
		//[BasicAuthentication]
		[Authorize]
		[Route("getFxTransByRefNo")]
		[HttpGet]
		public HttpResponseMessage LoadFXTransactionByRefNo(string refNo)
		{
			var entry = _repository.LoadFXTransactionByRefNo(refNo);

			if (entry != null )
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else
			{
				// HTTP/1.1 404 Not found  
				return Request.CreateErrorResponse(HttpStatusCode.NotFound,
						"FX Transaction of Ref No :" + refNo.ToString() + " not found");
			}
		}


        //http://localhost:62001//api/FXTransaction/?REF_NO=FSS751379854=F&GIP_DESCRIPTION=Approved&GIP_STATUS=15       
		//[BasicAuthentication]
		[Authorize]
		[Route("")]
		[HttpPut]
		public HttpResponseMessage Put([FromBody] FXTranStatus status)
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
						"FX Transaction with Ref Noe " + status.REF_NO.ToString() + " not found");
			}
			else
			{
				// HTTP/1.1 500	Internal Server Error
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, entry);
			}
		}
	}
}
