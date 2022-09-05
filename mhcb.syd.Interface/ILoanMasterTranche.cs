using mhcb.syd.DataAccess.View;
using System.Collections.Generic;

namespace mhcb.syd.Interface
{
	public interface ILoanMasterTranche
	{
		IEnumerable<LoanMasterTrancheView> GetCovenantCheckList();
		IEnumerable<LoanMasterTrancheView> GetLiborDiscontinuation();
	}
}