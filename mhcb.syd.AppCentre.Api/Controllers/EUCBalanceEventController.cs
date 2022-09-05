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

namespace mhcb.syd.DataService.Controllers
{
	//[EnableCors("*", "*", "*")]
	[RoutePrefix("api/EUCBalanceEvent")]
	public class EUCBalanceEventController : ApiController
	{
		//GET:  http://localhost:62001//api/EUCBalanceEvent/?custAbbr=ONSLOW SALT
		[EnableCors("*", "*", "*")]
		//[BasicAuthentication]
		//[Authorize]
		[Route("")]
		[HttpGet]
		public HttpResponseMessage getEUCBalanceEventByCustAbbr(string custAbbr, string optDate)
		{
			var entry = EUCBalanceEventBusiness.getEUCBalanceEventByCustAbbr(custAbbr, optDate);

			if (entry != null && entry.Count() != 0)
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else
			{
				// HTTP/1.1 404 Not found  
				return Request.CreateErrorResponse(HttpStatusCode.NotFound, 
							"MxEUCBalance Event of Customer : " + custAbbr + " not found");
			}
		}


		//GET:  http://localhost:62001//api/EUCBalanceEvent/?refNo=H10751006618
		[EnableCors("*", "*", "*")]
		//[BasicAuthentication]
		//[Authorize]
		[Route("")]
		[HttpGet]
		public HttpResponseMessage getEUCBalanceEventByRefno(string refNo, string optDate)
		{
			var entry = EUCBalanceEventBusiness.getEUCBalanceEventByRefno(refNo, optDate);

			if (entry != null && entry.Count() != 0)
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else
			{
				// HTTP/1.1 404 Not found  
				return Request.CreateErrorResponse(HttpStatusCode.NotFound,
						"MxEUCBalance Event of Ref No : " + refNo + " not found");
			}
		}

		//Category & SendingSystem: 
		//							EucBalances, 
		//							GuideCustomer, 
		//							GBaseRefNo
		//   http://localhost:62001//api/EUCBalanceEvent/?sender=EucBalances
		//   http://localhost:62001//api/EUCBalanceEvent/?sender=GuideCustomer
		//   http://localhost:62001//api/EUCBalanceEvent/?sender=GBaseRefNo
		[EnableCors("*", "*", "*")]
		//[BasicAuthentication]
		//[Authorize]
		[Route("")]
		[HttpGet]
		public HttpResponseMessage getMxInboundEventLogBySender(string sender, string optDate)
		{
			var entry = EUCBalanceEventBusiness.getMxInboundEventLogBySender(sender, optDate);

			if (entry != null && entry.Count() != 0)
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else
			{
				// HTTP/1.1 404 Not found  
				return Request.CreateErrorResponse(HttpStatusCode.NotFound, 
							"MxInbound Event Log of " + sender + " not found");
			}
		}


        /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/

        // please add primary key to the table !!!!!!!!!!!!!!!!!

        //BEGIN TRANSACTION
        //SET QUOTED_IDENTIFIER ON
        //SET ARITHABORT ON
        //SET NUMERIC_ROUNDABORT OFF
        //SET CONCAT_NULL_YIELDS_NULL ON
        //SET ANSI_NULLS ON
        //SET ANSI_PADDING ON
        //SET ANSI_WARNINGS ON
        //COMMIT
        //BEGIN TRANSACTION
        //GO
        //ALTER TABLE dbo.MxEucBalanceEvent ADD CONSTRAINT
        //	PK_MxEucBalanceEvent PRIMARY KEY CLUSTERED
        //	(
        //	Id
        //	) WITH(STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]

        //GO
        //ALTER TABLE dbo.MxEucBalanceEvent SET(LOCK_ESCALATION = TABLE)
        //GO
        //COMMIT


        //GET:  http://localhost:62001//api/EUCBalanceEvent/Pending
        [EnableCors("*", "*", "*")]
        //[BasicAuthentication]
        //[Authorize]
        [Route("Pending")]
        [HttpGet]
        public HttpResponseMessage GetPendingTrans()
        {
            var entry = EUCBalanceEventBusiness.GetPendingTrans();

            if (entry != null && entry.Count() != 0)
            {
                // HTTP/1.1 200 OK  
                return Request.CreateResponse(HttpStatusCode.OK, entry);
            }
            else
            {
                // HTTP/1.1 404 Not found  
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Pending EUC Balance Event not found");
            }
        }


        //TEST  http://localhost:62001//api/EUCBalanceEvent/?GBaseReferenceNo=H10751006618
        [EnableCors("*", "*", "*")]
		//[BasicAuthentication]
		//[Authorize]
		[Route("")]
		[HttpPut]
		public HttpResponseMessage Put([FromBody] EucBalanceAttribute balanceAttribute)
		{
			var entry = EUCBalanceEventBusiness.UpdateStatus(balanceAttribute);

			if (entry == "OK")
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else if (entry == "NotFound")
			{
				// HTTP/1.1 404 Not found  
				return Request.CreateErrorResponse(HttpStatusCode.NotFound,
					"EUC Balance Event of " + balanceAttribute.GBaseReferenceNo.ToString() + " not found");
			}
			else
			{
				// HTTP/1.1 500	Internal Server Error
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, entry);
			}
		
		}


	}
}
