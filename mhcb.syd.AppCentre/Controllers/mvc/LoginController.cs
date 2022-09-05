using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace mhcb.syd.AppCentre.Controllers.mvc
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(string username, string password)
        {       
            // this is the case return data from Web Api controller caller
            //IEnumerable<Employee> employees = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:62001/api/");

                //HTTP GET
                var responseTask = client.GetAsync("Login/?username =" + username + "password=" + password);

                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return View("OK");
                    //return RedirectToAction("Index");
                    //window.location.href = origin + "/Register.html"
                }
                else //web api sent error response 
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            //return RedirectToAction("Index");
            return View("OK");
        }
    }
}