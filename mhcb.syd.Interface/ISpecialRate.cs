using mhcb.syd.DataAccess;
using mhcb.syd.DataAccess.View;
using System.Collections.Generic;

namespace mhcb.syd.Interface
{
	public interface ISpecialRate
	{
		IEnumerable<ExchRateView> LoadSpcialRate();
		string updateStatus(RateAttribute rate);
	}
}