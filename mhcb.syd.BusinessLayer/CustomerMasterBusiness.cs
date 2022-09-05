using System.Collections.Generic;
using System.Data;
using System.Linq;
using mhcb.syd.DataAccess;
using mhcb.syd.Interface;

namespace mhcb.syd.BusinessLayer
{
	public class CustomerMasterBusiness : ICustomerMaster
	{
		public IEnumerable<CUSTOMER_MASTER> LoadCustomerMasterByCustAbbr(string custAbbr, string branchNo)
		{
			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				var entry = entities.CUSTOMER_MASTER
							.Where(e => e.CUST_ABBR == custAbbr
									 && e.BRANCH_NO == branchNo
									 && e.STATUS_CD != "C")
							.Select(x => new CUSTOMER_MASTER()
							{
								CUST_ABBR = x.CUST_ABBR,
								BRANCH_NO = x.BRANCH_NO,
								STEPS_CUST_CD = x.STEPS_CUST_CD,
								CUST_NAME = x.CUST_NAME,
								ADDRESS = x.ADDRESS,
								ADDRESS2 = x.ADDRESS2,
								DEPARTMENT_CD = x.DEPARTMENT_CD,
								OFFICER_CD = x.OFFICER_CD,
								LOCATION_COUNTRY = x.LOCATION_COUNTRY,
								TAX_CD = x.TAX_CD,
								NATIONALITY = x.NATIONALITY,
								STATUS_CD = x.STATUS_CD,
								DELETION_STATUS = x.DELETION_STATUS,
								STATUS = x.STATUS,
							})
							.ToList();
				return entry;
			}
		}
	}
}
