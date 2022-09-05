using mhcb.syd.DataAccess;
using mhcb.syd.DataAccess.View;
using System.Collections.Generic;

namespace mhcb.syd.Interface
{
	public interface IEDocument
	{
		IEnumerable<eDocumentView> GetPendingTrans(string eDocType, string optDate);
		string updateStatus(EDocumentAttribute eDocAttribute);
	}
}