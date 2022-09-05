using mhcb.syd.DataAccess;
using System.Collections.Generic;

namespace mhcb.syd.Interface
{
	public interface ICustomerMaster
	{
		IEnumerable<CUSTOMER_MASTER> LoadCustomerMasterByCustAbbr(string custAbbr, string branchNo);
	}
}