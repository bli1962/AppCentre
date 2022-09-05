using mhcb.syd.DataAccess;
using mhcb.syd.DataAccess.View;
using System.Collections.Generic;

namespace mhcb.syd.Interface
{
	public interface IFXSettlementInf
	{
		IEnumerable<FxDeliveryView> getfxDeliveryCorpByDate(string strDeliveryDate);
		IEnumerable<FxDeliveryCorpSummaryView> getfxDeliveryCorpSummaryByDate(string strDeliveryDate);
		IEnumerable<FXDeliveryView> GetPendingTrans();
		FX_SETTLEMENT_INF LoadFXSettlementByRefNo(string refNo);
		string updateStatus(FXTranStatus status);
	}
}