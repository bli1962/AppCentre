using mhcb.syd.DataAccess;
using mhcb.syd.DataAccess.View;
using System.Collections.Generic;

namespace mhcb.syd.Interface
{
	public interface IEUCOnDemandBatch
	{
		IEnumerable<OnDemandBatchLogView> GetOnDemandBatchLogByBatchNo(string strBatchId);
		IEnumerable<OnDemandBatchView> GetOnDemandBatchNoByDate(string createdDate);
		string UpdateStatus(OnDemandBatchAttribute onDemandBatchAttribute);
	}
}