using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using mhcb.syd.DataService.Models;

namespace mhcb.syd.DataService.Controllers.mvc
{
    public class EmployeeController : Controller
    {
		// GET: Employee
		public ActionResult Index()
		{
			// this is the case return data from MVC controller 
			//List<Employee> myList = new List<Employee>();
			//myList.Add(	new Employee() {EmployeeId = 101, Name = "John", Gender = "Male", City = "London"});
			//myList.Add(	new Employee() {EmployeeId = 102, Name = "Jason", Gender = "Male", City = "Sydney" }
			//);			
			//return View(myList);


			// this is the case return data from Web Api controller caller
			IEnumerable<Employee> employees = null;

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://localhost:62001/api/");

				//HTTP GET
				var responseTask = client.GetAsync("Employee");

				responseTask.Wait();
				var result = responseTask.Result;

				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<IList<Employee>>();
					readTask.Wait();

					employees = readTask.Result;
				}
				else //web api sent error response 
				{
					//log response status here..
					employees = Enumerable.Empty<Employee>();

					ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
				}
			}
			return View(employees);
		}

	}
}