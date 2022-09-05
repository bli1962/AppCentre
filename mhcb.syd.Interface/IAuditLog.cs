using mhcb.syd.DataAccess;
using System.Collections.Generic;

namespace mhcb.syd.Interface
{
	public interface IAuditLog
	{
		bool InsertAuditLog(AUDIT_LOG audit_Log);
		IEnumerable<AUDIT_LOG> LoadAuditLogByDate(string optDate);
		IEnumerable<AUDIT_LOG> LoadAuditLogByDateRange(string strDateFrom, string strDateTo);
	}
}