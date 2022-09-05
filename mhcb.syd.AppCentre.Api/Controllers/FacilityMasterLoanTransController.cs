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
    //[EnableCors("*", "*", "*")]
    [RoutePrefix("api/FacilityMasterLoanTrans")]
    public class FacilityMasterLoanTransController : ApiController
    {
        //http://localhost:62001//api/FacilityMasterLoanTrans/?dateFrom=2020-04-01&dateTo=2021-03-31
        [EnableCors("*", "*", "*")]
        //[BasicAuthentication]
        //[Authorize]
        [Route("")]
        [HttpGet]
        public HttpResponseMessage getLoanTransactionByDates(string dateFrom, string dateTo)
        {
            var entry = FacilityMasterLoanTrans.getLoanTransactionByDates(dateFrom, dateTo);

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
                            "Loan Transaction between start dates : " + dateFrom.ToString() + " and " + dateTo.ToString() + " not found");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                             "Loan Transaction not found");
                }               
            }
        }


        //http://localhost:62001//api/FacilityMasterLoanTrans/?dateFrom=2020-04-01&dateTo=2021-03-31&Ccy=USD
        [EnableCors("*", "*", "*")]
        //[BasicAuthentication]
        //[Authorize]
        [Route("")]
        [HttpGet]
        public HttpResponseMessage getLoanTransactionByDatesCcy(string dateFrom, string dateTo, string ccy)
        {
            var entry = FacilityMasterLoanTrans.getLoanTransactionByCcyDates(dateFrom, dateTo, ccy);

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
                    "Loan Transaction between start dates : " + dateFrom.ToString() + " and " + dateTo.ToString() + " not found");
                } else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "Loan Transaction not found");
                }
            }
        }
    }
}
