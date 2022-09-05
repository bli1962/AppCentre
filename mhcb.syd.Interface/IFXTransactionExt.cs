using mhcb.syd.DataAccess;
using mhcb.syd.DataAccess.View;
using System.Collections.Generic;

namespace mhcb.syd.Interface
{
	public interface IFXTransactionExt
	{
		IEnumerable<FXTransactionExtView> GetPendingTrans();
		string updateStatus(FXGIDStatus status);
	}
}