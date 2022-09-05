using System.Net;
using System.Net.Http;
using System.Web.Http;
using mhcb.syd.BusinessLayer;

namespace mhcb.syd.DataService.Controllers
{
	public class GuideController : ApiController
    {
		[HttpGet]
		public HttpResponseMessage Get(string username, string password)
		{		
			if (UsersSecurity.Login(username, password.ToUpper()))
			{
				//Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(userid), null);
				//return Request.CreateResponse(HttpStatusCode.OK,  username);
				var authorisers = UsersSecurity.GetAuthorisers(username);
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
