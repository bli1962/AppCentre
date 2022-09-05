using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using mhcb.syd.BusinessLayer;

namespace mhcb.syd.DataService
{
    //Enable basic authentication
    //1. The BasicAuthenticationAttribute can be applied on a specific controller, specific action, 
    //   or globally on all Web API controllers.

    //2. To enable basic authentication across the entire Web API application, register BasicAuthenticationAttribute 
    //   as a filter using the Register() method in WebApiConfig class
    //   config.Filters.Add(new RequireHttpsAttribute());

    //3. You can also apply the attribute on a specific controller, to enable basic authentication 
    //   for all the methods in that controller

    //4. In our case let's just enable basic authentication for Get() method in EmployeesController. 
    //   Also modify the implementation of the Get() method as shown below.

    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // In Basic authroization, user send a header, 
            // so we need to check 'actionContext.Request.Headers.Authorization'
            if (actionContext.Request.Headers.Authorization == null)
            {
                // HTTP/1.1 401 Unauthorized 
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                // get the value of 'actionContext.Request.Headers.Authorization'
				// it's 64 base code string
                string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;

                string decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));

                string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');
                string userid = usernamePasswordArray[0];
                string password = usernamePasswordArray[1];

                // use our own class to check the user name and password is valid
                if (new UsersSecurity().Login(userid, password))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(userid), null);  
                    // second parameter is for rule
                }
                else
                {
                    // HTTP/1.1 401 Unauthorized 
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}