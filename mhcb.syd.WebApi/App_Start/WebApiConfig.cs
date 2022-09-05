using System;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http.Cors;
using mhcb.syd.BusinessLayer;
using mhcb.syd.WebApi.App_Start;
using mhcb.syd.Interface;
using Unity.Lifetime;
using Unity;

namespace mhcb.syd.WebApi
{
    public static class WebApiConfig
    {
        public class CustomJsonFormatter : JsonMediaTypeFormatter
        {
            // how to create custom Json Formatter
            public CustomJsonFormatter()
            {
                this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            }

            public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
            {
                base.SetDefaultContentHeaders(type, headers, mediaType);
                headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
        }

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            //IUnityContainer container = new UnityContainer();
            var container = new UnityContainer();
            container.RegisterType<IBankInf, BankInfBusiness>(new HierarchicalLifetimeManager());
            container.RegisterType<ICovenantMasterRecord, CovenantMasterRecord>(new HierarchicalLifetimeManager());
            container.RegisterType<ICustomerMaster, CustomerMasterBusiness>(new HierarchicalLifetimeManager());
            container.RegisterType<IEDocument, EDocumentBusiness>(new HierarchicalLifetimeManager());
            container.RegisterType<IEUCBalanceEvent, EUCBalanceEventBusiness>(new HierarchicalLifetimeManager());
            container.RegisterType<IEUCOnDemandBatch, EUCOnDemandBatchBusiness>(new HierarchicalLifetimeManager());
            container.RegisterType<IFacilityMasterLoanTrans, FacilityMasterLoanTrans>(new HierarchicalLifetimeManager());
            container.RegisterType<IFXFCust, FXFCustBusiness>(new HierarchicalLifetimeManager());
            container.RegisterType<IFXSettlementInf, FXSettlementInfBusiness>(new HierarchicalLifetimeManager());
            container.RegisterType<IFXTransaction, FXTransactionBusiness>(new HierarchicalLifetimeManager());
            container.RegisterType<IFXTransactionExt, FXTransactionExtBusiness>(new HierarchicalLifetimeManager());
            container.RegisterType<ILoanMasterTranche, LoanMasterTrancheBusiness>(new HierarchicalLifetimeManager());
            container.RegisterType<IMoneyMarketTicket, MoneyMarketTicketBusiness>(new HierarchicalLifetimeManager());
            container.RegisterType<IPaymentControl, PaymentControlBusiness>(new HierarchicalLifetimeManager());
            container.RegisterType<IRFRInterest, RFRInterest>(new HierarchicalLifetimeManager());
            container.RegisterType<ISettlementInf, SettlementInfBusiness>(new HierarchicalLifetimeManager());
            container.RegisterType<ISpecialRate, SpecialRateBusiness>(new HierarchicalLifetimeManager());
            container.RegisterType<IUser, UserBusiness>(new HierarchicalLifetimeManager());
            container.RegisterType<IUsersSecurity, UsersSecurity>(new HierarchicalLifetimeManager());
            container.RegisterType<IGASBusiness, GASBusiness>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            // *********************************************************************************************
            //How to return only JSON Formatter only from ASP.NET Web API Service irrespective of the Accept header value
            // 1. to remove XML formatter and return JSON format data only
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //How to return only XML Formatter only from ASP.NET Web API Service irrespective of the Accept header value
            //    to remove Json formatter and return XML format data only
            //config.Formatters.Remove(config.Formatters.JsonFormatter);

            // 2. How to return JSON Formatter instead of XML from ASP.NET Web API Service when a request is made from the browser
            // add this to force return JSON format from Browser, because browser return XML by default
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            //
            // or create customer class CustomJsonFormatter, then register this line
            //config.Formatters.Add(new CustomJsonFormatter());
            // *********************************************************************************************


            // ************ TOW WAY TO CALL CROSS DOMIN ********************************************//

            // *********************************************************************************************
            // Step 1 : To support JSONP format, 
            //          installs WebApiContrib.Formatting.Jsonp package.

            // Step 2 : Add the following two lines of code in Register() method of WebApiConfig here 
            //var jsonpFormatter = new JsonpMediaTypeFormatter(config.Formatters.JsonFormatter);
            //config.Formatters.Insert(0, jsonpFormatter);

            // Step 3 : In the ClientApplication, set the dataType option of the jQuery ajax function to jsonp
            // dataType: 'jsonp'
            //   $.ajax({
            //   type: 'GET',
            //        url: 'http://localhost:62001/api/settlementinf?status=closed/',
            //        dataType: 'jsonp',
            //        ....... etc
            // *********************************************************************************************




            // *********************************************************************************************
            // Step 1 : Install Microsoft.AspNet.WebApi.Cors package. 
            //          Install - Package Microsoft.AspNet.WebApi.Cors

            // Step 2 : Include the following 2 lines of code in Register() method of WebApiConfig 
            EnableCorsAttribute cors = new EnableCorsAttribute("http://localhost:62001", "*", "*");
            //EnableCorsAttribute cors = new EnableCorsAttribute("http://localhost:62001", "http,https", "GET,POST");
            config.EnableCors(cors);

            //or no instance or parameter, leave control in class or method level
            //config.EnableCors();

            // Step 3 : In the ClientApplication, set the dataType option of the jQuery ajax function to json
            // dataType: 'json'
            //   $.ajax({
            //   type: 'GET',
            //        url: url: 'http://localhost:62001/api/settlementinf?status=closed/',
            //        dataType: 'json',
            //        ....... etc
            // *********************************************************************************************


            // *********************************************************************************************
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
            // *********************************************************************************************

            //config.Filters.Add(new BasicAuthenticationAttribute());
        }

        //public static void Register(HttpConfiguration config)
        //{
        //    // Web API configuration and services
        //    // Configure Web API to use only bearer token authentication.
        //    config.SuppressDefaultHostAuthentication();
        //    config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

        //    // Web API routes
        //    config.MapHttpAttributeRoutes();

        //    config.Routes.MapHttpRoute(
        //        name: "DefaultApi",
        //        routeTemplate: "api/{controller}/{id}",
        //        defaults: new { id = RouteParameter.Optional }
        //    );
        //}
    }
}
