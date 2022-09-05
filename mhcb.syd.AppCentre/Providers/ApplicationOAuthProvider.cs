using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using mhcb.syd.DataService.Models;

namespace mhcb.syd.DataService.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

            //Front end input
            //UserName    "BL007014@mizuho.cb.com"  
            //Password    "xxxxxx" 

            // USER gUser = UsersSecurity.GetGUser(context.UserName, context.Password);
            //ApplicationUser user1 = new ApplicationUser()
            //{
            //AccessFailedCount = 0,
            ////Claims = 0,
            ////Logins = 0,
            ////Roles  =  Count
            //EmailConfirmed = false,
            //LockoutEnabled = false,
            //PhoneNumberConfirmed = false,
            //TwoFactorEnabled = false,
            //PhoneNumber = null,
            //LockoutEndDateUtc = null,
            //Email = "BL007014@mizuho.cb.com",
            //UserName = "BL007014@mizuho.cb.com",
            //Id = "3f92fa49-d921-45da-9143-f8d6ec7290bf",
            //PasswordHash = "ABQvnZaZSdaQFeJF4ouEKHy0eJKPPFSRDjm3dChLGV3KGuiW3EWcZKj/I78jxeADJA==",
            //SecurityStamp = "d49b4330-4a93-45a6-9311-99de6cd42f51",
            //};

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            //1. Prepare AuthIdendity
            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
               OAuthDefaults.AuthenticationType);

            //2. Prepare cookiesIdendity
            ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
                CookieAuthenticationDefaults.AuthenticationType);

            //3. Prepare AuthenticationPropeties of user
            AuthenticationProperties properties = CreateProperties(user.UserName);
            
            //4. Prepare Authentication ticket
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);

            //5. Return cookesIdentity
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
}