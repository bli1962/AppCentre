using System.Web;
using System.Web.Mvc;

namespace mhcb.syd.AppCentre.Api
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
