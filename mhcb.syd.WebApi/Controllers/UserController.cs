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
    [RoutePrefix("api/User")]
	public class UserController : ApiController
	{
		private IUser _repository;
		public UserController(IUser repository)
		{
			_repository = repository;
		}

		//[BasicAuthentication]
		[Authorize]
		[Route("getUserByStatus")]
		[HttpGet]
		public HttpResponseMessage LoadUsersByRecStatus(string status)
		{
			var entry = _repository.LoadUsersByRecStatus(status);

			if (entry != null && entry.Count() != 0)
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else
			{
				// HTTP/1.1 404 Not found  
				return Request.CreateErrorResponse(HttpStatusCode.NotFound,
					"GUIDE Users of status : " + status.ToString() + " not found");
			}
		}

	
		//[BasicAuthentication]
		[Authorize]
		[Route("getUserById")]
		[HttpGet]
		public HttpResponseMessage LoadUserByUserId(string userId)
		{
			var entry = _repository.LoadUserByUserId(userId);

			if (entry != null)
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else
			{
				// HTTP/1.1 404 Not found  
				return Request.CreateErrorResponse(HttpStatusCode.NotFound,
					"GUIDE User ID : " + userId.ToString() + " not found");
			}
		}


		//TEST  http://localhost:62001//api/User/?USER_ID=AB003125&PASSWORD=Password1		
		//[BasicAuthentication]
		[Authorize]
		[Route("")]
		[HttpPut]
		public HttpResponseMessage Put([FromBody] UserAttribute userAttr)
		{
			var entry = _repository.updateStatus(userAttr);

			if (entry == "OK")
			{
				// HTTP/1.1 200 OK  
				return Request.CreateResponse(HttpStatusCode.OK, entry);
			}
			else if (entry == "NotFound")
			{
				// HTTP/1.1 404 Not found  
				return Request.CreateErrorResponse(HttpStatusCode.NotFound,
					"GUIDE User ID : " + userAttr.USER_ID.ToString() + " not found");
			}
			else
			{
				// HTTP/1.1 500	Internal Server Error
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, entry);
			}
		}
	}

}
