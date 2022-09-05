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

namespace mhcb.syd.DataService.Controllers.api
{
    [RoutePrefix("api/EUOnDemandBatch")]
    public class EUCOnDemandBatchController : ApiController
    {
        //GET:  http://localhost:62001//api/EUOnDemandBatch/
        [EnableCors("*", "*", "*")]
        //[BasicAuthentication]
        ////[Authorize]
        [Route("")]
        [HttpGet]
        public HttpResponseMessage getTodayOnDemandBatchNo()
        {
            //var dtfi = CultureInfo.CurrentCulture.DateTimeFormat;
            string createdDate = DateTime.Today.ToShortDateString();
            //createdDate = "09/06/2021";

            var entry = EUCOnDemandBatchBusiness.GetOnDemandBatchNoByDate(createdDate);

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


        //GET:  http://localhost:62001//api/EUOnDemandBatch/?strBatchId=3687
        [EnableCors("*", "*", "*")]
        //[BasicAuthentication]
        ////[Authorize]
        [Route("")]
        [HttpGet]
        public HttpResponseMessage getOnDemandBatchLogByBatchNo(string strBatchId)
        {
            var entry = EUCOnDemandBatchBusiness.GetOnDemandBatchLogByBatchNo(strBatchId);
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


        //TEST  http://localhost:62001//api/EUOnDemandBatch/?BatchId=3687&&BatchNo=3808&&AUTHORIZE_BY=liaaba
        [EnableCors("*", "*", "*")]
        //[BasicAuthentication]
        ////[Authorize]
        [Route("")]
        [HttpPut]
        public HttpResponseMessage Put([FromBody] OnDemandBatchAttribute onDemandBatchAttribute)
        {
            var entry = EUCOnDemandBatchBusiness.UpdateStatus(onDemandBatchAttribute);

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
