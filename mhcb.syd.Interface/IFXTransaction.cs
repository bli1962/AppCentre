using mhcb.syd.DataAccess;
using mhcb.syd.DataAccess.View;
using System.Collections.Generic;

namespace mhcb.syd.Interface
{
	public interface IFXTransaction
	{
		IEnumerable<FxDeliveryView> getFxTransactioByDates(string strDateFrom, string strDateTo);
		IEnumerable<FXTransactionView> GetFXTransactionByDatesCustAbbr(string strDateFrom, string strDateTo, string dateType, string custName);
		IEnumerable<FXTransactionView> GetPendingTrans();
		FX_TRANSACTION LoadFXTransactionByRefNo(string refNo);
		string updateStatus(FXTranStatus status);
	}
}