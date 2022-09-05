using mhcb.syd.DataAccess.View;
using System.Collections.Generic;

namespace mhcb.syd.Interface
{
	public interface IRFRInterest
	{
		IEnumerable<RFRInterestLoanView> getRFRInterestForLoanByReportDate(string strReportDate, string custAbbr);
		IEnumerable<RFRInterestSwapView> getRFRInterestForSwapByReportDate(string strReportDate, string custAbbr);
	}
}