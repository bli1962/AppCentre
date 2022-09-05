using mhcb.syd.DataAccess;
using mhcb.syd.DataAccess.View;
using System.Collections.Generic;

namespace mhcb.syd.Interface
{
	public interface IEUCBalanceEvent
	{
		IEnumerable<MxEucBalanceEvent> getEUCBalanceEventByCustAbbr(string custAbbr, string optDate);
		IEnumerable<MxEucBalanceEvent> getEUCBalanceEventByRefno(string refNo, string optDate);
		IEnumerable<MxInboundEventLogView> getMxInboundEventLogBySender(string sender, string optDate);
		IEnumerable<MxEucBalanceEventView> GetPendingTrans();
		string UpdateStatus(EucBalanceAttribute balanceAttribute);
	}
}