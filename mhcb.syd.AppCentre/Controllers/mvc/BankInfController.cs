using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using mhcb.syd.DataAccess;
using mhcb.syd.DataService.Models;

namespace mhcb.syd.DataService.Controllers.mvc
{
    public class BankInfController : Controller
    {
        public ActionResult Index()
        {        
            //return View();
            IEnumerable<BankInfView> bankInfView = null;

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://localhost:62001/api/");

				//HTTP GET
				var responseTask = client.GetAsync("BankInf?swiftId=AACKDE33080");

				responseTask.Wait();

				var result = responseTask.Result;

				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<IList<BankInfView>>();
					readTask.Wait();

					bankInfView = readTask.Result;
				}
				else //web api sent error response 
				{
					//log response status here..
					bankInfView = Enumerable.Empty<BankInfView>();

					ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
				}
			}
			return View(bankInfView);
        }
    }
}