using mhcb.syd.DataAccess;
using mhcb.syd.DataAccess.View;
using System.Collections.Generic;

namespace mhcb.syd.Interface
{
	public interface IPaymentControl
	{
		string DeleteSwiftPayment(SwiftPaymentStatus status);
		IEnumerable<SwiftPaymentView> getPaymentControlPaymentByDate(string strDateValue, string dateType);
		IEnumerable<SwiftPaymentView> getSwiftPaymentByDate(string strDateValue, string dateType);
		string UpdateStatus(SwiftPaymentStatus status);
	}
}