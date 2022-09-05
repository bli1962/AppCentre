using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using mhcb.syd.DataAccess.View;
using mhcb.syd.Interface;

namespace mhcb.syd.AppCentre.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/EUCBalanceEvent")]
	public class EUCBalanceEventController : ApiController
	{
		private readonly IEUCBalanceEvent _repository;
		public EUCBalanceEventController(IEUCBalanceEvent repository)
		{
			_repository = repository;
		}


		//[BasicAuthentication]
		[Authorize]
		[Route("getEUCBalanceEventByCustAbbr")]
		[HttpGet]
		public HttpResponseMessage getEUCBalanceEventByCustAbbr(string custAbbr, string optDate)
		{
			var entry = _repository.getEUCBalanceEventByCustAbbr(custAbbr, optDate);

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

	
		//[BasicAuthentication]
		[Authorize]
		[Route("getEUCBalanceEventByRefno")]
		[HttpGet]
		public HttpResponseMessage getEUCBalanceEventByRefno(string refNo, string optDate)
		{
			var entry = _repository.getEUCBalanceEventByRefno(refNo, optDate);

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

		// http://localhost:62001//api/EUCBalanceEvent/getMxInboundEventLogBySender/?sender=EucBalances
		// http://localhost:62001//api/EUCBalanceEvent/getMxInboundEventLogBySender/?sender=GuideCustomer
		// http://localhost:62001//api/EUCBalanceEvent/getMxInboundEventLogBySender/?sender=GBaseRefNo		
		//[BasicAuthentication]
		[Authorize]
		[Route("getMxInboundEventLogBySender")]
		[HttpGet]
		public HttpResponseMessage getMxInboundEventLogBySender(string sender, string optDate)
		{
			var entry = _repository.getMxInboundEventLogBySender(sender, optDate);

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

    
        //[BasicAuthentication]
        [Authorize]
        [Route("getPendingEUCBalanceEvent")]
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
                        "Pending EUC Balance Event not found");
            }
        }

  
		//[BasicAuthentication]
		[Authorize]
		[Route("")]
		[HttpPut]
		public HttpResponseMessage Put([FromBody] EucBalanceAttribute balanceAttribute)
		{
			var entry = _repository.UpdateStatus(balanceAttribute);

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
