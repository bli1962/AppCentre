using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using mhcb.syd.DataAccess;
using mhcb.syd.DataAccess.View;
using mhcb.syd.Interface;

namespace mhcb.syd.AppCentre.Controllers.api
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/EUOnDemandBatch")]
    public class EUCOnDemandBatchController : ApiController
    {
        private IEUCOnDemandBatch _repository;
        public EUCOnDemandBatchController(IEUCOnDemandBatch repository)
        {
            _repository = repository;
        }

   
        //[BasicAuthentication]
        //[Authorize]
        [Route("getTodayOnDemandBatchNo")]
        [HttpGet]
        public HttpResponseMessage getTodayOnDemandBatchNo()
        {
            //var dtfi = CultureInfo.CurrentCulture.DateTimeFormat;
            string createdDate = DateTime.Today.ToShortDateString();
            //createdDate = "09/06/2021";

            var entry = _repository.GetOnDemandBatchNoByDate(createdDate);

            if (entry != null && entry.Count() != 0)
            {
                // HTTP/1.1 200 OK  
                return Request.CreateResponse(HttpStatusCode.OK, entry);
            }
            else
            {
                // HTTP/1.1 404 Not found  
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Today EUC OnDemand Batch not found");
            }
        }

   
        //[BasicAuthentication]
        //[Authorize]
        [Route("getOnDemandBatchLogByBatchNo")]
        [HttpGet]
        public HttpResponseMessage getOnDemandBatchLogByBatchNo(string strBatchId)
        {
            var entry = _repository.GetOnDemandBatchLogByBatchNo(strBatchId);
            if (entry != null && entry.Count() != 0)
            {
                // HTTP/1.1 200 OK  
                return Request.CreateResponse(HttpStatusCode.OK, entry);
            }
            else
            {
                // HTTP/1.1 404 Not found  
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "EUC OnDemand Batch Log of " + strBatchId + " not found");
            }
        }

     
        //[BasicAuthentication]
        //[Authorize]
        [Route("")]
        [HttpPut]
        public HttpResponseMessage Put([FromBody] OnDemandBatchAttribute onDemandBatchAttribute)
        {
            var entry = _repository.UpdateStatus(onDemandBatchAttribute);

            if (entry == "OK")
            {
                // HTTP/1.1 200 OK  
                return Request.CreateResponse(HttpStatusCode.OK, entry);
            }
            else if (entry == "NotFound")
            {
                // HTTP/1.1 404 Not found  
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "EUC OnDemand Batc of " + onDemandBatchAttribute.BatchNo.ToString() + " not found or to be updated.");
            }
            else
            {
                // HTTP/1.1 500	Internal Server Error
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, entry);
            }
        }

    }
}
