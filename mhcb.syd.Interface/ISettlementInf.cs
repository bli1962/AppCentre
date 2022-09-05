using mhcb.syd.DataAccess.View;
using mhcb.syd.DataAccess;
using System.Collections.Generic;

namespace mhcb.syd.Interface
{
	public interface ISettlementInf
	{
		IEnumerable<SettlementInfView> GetPendingTrans();
		//string InsertSettlementInf(SETTLEMENT_INF settlement_inf);
		IEnumerable<SettlementInfView> LoadSettlementInfByCustAbbr(string custAbbr);
		IEnumerable<SettlementInfView> LoadSettlementInfByDetails(string custAbbr, string ccyCD, string recordKind);
		IEnumerable<SettlementInfView> LoadSettlementInfBySifId(int id);
		IEnumerable<SETTLEMENT_INF> LoadSettlementInfsByStatus(string status = "All");

		string DeleteSettlementInf(int id);

		string updateStatus(SIFStatus status);
	}
}