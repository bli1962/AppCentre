using mhcb.syd.DataAccess.View;
using System.Collections.Generic;

namespace mhcb.syd.Interface
{
	public interface IFacilityMasterLoanTrans
	{
		IEnumerable<FacilityMasterLoanTransView> getLoanTransactionByCcyDates(string strDateFrom, string strDateTo, string ccy);
		IEnumerable<FacilityMasterLoanTransView> getLoanTransactionByDates(string strDateFrom, string strDateTo);
	}
}