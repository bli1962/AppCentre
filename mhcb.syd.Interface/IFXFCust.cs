using mhcb.syd.DataAccess.View;
using System.Collections.Generic;

namespace mhcb.syd.Interface
{
	public interface IFXFCust
	{
		IEnumerable<FXFCustomerView> LoadFXCustByCustAbbr(string abbreviation);
		string updateStatus(FXFCustAttribute custAttr);
	}
}