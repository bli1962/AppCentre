using mhcb.syd.DataAccess;
using mhcb.syd.DataAccess.View;
using System.Collections.Generic;

namespace mhcb.syd.Interface
{
	public interface ICovenantMasterRecord
	{
		IEnumerable<CovenantMasterRecordView> GetCovenantMasterRecord();
		IEnumerable<CovenantRatioView> GetCovenantRatio(string covenantType);
	}
}