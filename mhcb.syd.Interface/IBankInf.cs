using mhcb.syd.DataAccess.View;
using System.Collections.Generic;

namespace mhcb.syd.Interface
{
	public interface IBankInf
	{
		IEnumerable<BankInfView> GetPendingTrans();
		IEnumerable<BankInfView> LoadBankInfByDetails(string custName, string branchNo);
		IEnumerable<BankInfView> LoadBankInfBySwiftID(string swiftId);
		IEnumerable<BankInfView> LoadBankInfsByStatus(string status = "All");
		string updateStatus(BankInfStatus status);
	}
}