using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using mhcb.syd.Interface;

namespace mhcb.syd.AppCentre.Controllers
{
	public class GuideController : ApiController
    {
		private IUsersSecurity _repository;
		public GuideController(IUsersSecurity repository)
		{
			_repository = repository;
		}

		[EnableCors("*", "*", "*")]
        [HttpGet]
		public HttpResponseMessage Get(string username, string password)
		{		
			if (_repository.Login(username, password.ToUpper()))
			{
				//Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(userid), null);
				//return Request.CreateResponse(HttpStatusCode.OK,  username);
				var authorisers = _repository.GetAuthorisers(username);
				return Request.CreateResponse(HttpStatusCode.OK, authorisers); 
			}
			else
			{
                // HTTP/1.1 401 Unauthorized 
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, username + " user is not found.");
            }

		}
	}
}
