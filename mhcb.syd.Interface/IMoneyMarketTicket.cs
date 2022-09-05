using mhcb.syd.DataAccess;
using mhcb.syd.DataAccess.View;
using System.Collections.Generic;

namespace mhcb.syd.Interface
{
	public interface IMoneyMarketTicket
	{
		IEnumerable<MoneyMarketReportView> GetMoneyMarketTransactionByDates(string strDateFrom, string strDateTo);
		IEnumerable<MoneyMarketTicketView> GetPendingTrans();
		string updateStatus(MMGIDStatus status);
	}
}