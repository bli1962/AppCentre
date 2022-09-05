using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using mhcb.syd.DataService.Models;


namespace mhcb.syd.DataService.Controllers.api
{
	//[EnableCors("*", "*", "*")]
	[RoutePrefix("api/Employee")]
	public class EmployeeController : ApiController
	{
		//GET:  http://localhost:62001//api/Employee
		[EnableCors("*", "*", "*")]
		//[BasicAuthentication]
		//[Authorize]
		[Route("")]
		[HttpGet]
		public IHttpActionResult Get()
		{
			List<Employee> myList = new List<Employee>();
			myList.Add(new Employee() { EmployeeId = 101, Name = "John", Gender = "Male", City = "London" });
			myList.Add(new Employee() { EmployeeId = 102, Name = "Jason", Gender = "Male", City = "Sydney" }
			);

			if (myList == null)
			{
				return NotFound();
			}
			return Ok(myList);
		}

	}
}
