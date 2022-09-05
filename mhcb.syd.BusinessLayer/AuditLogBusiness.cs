using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data;
using System.Linq;
using mhcb.syd.DataAccess;
using mhcb.syd.Interface;

namespace mhcb.syd.BusinessLayer
{
	public class AuditLog
	{
		public static Boolean InsertAuditLog(AUDIT_LOG audit_Log)
		{
			try
			{
				using (GUIDEDBEntities entities = new GUIDEDBEntities())
				{
					entities.AUDIT_LOG.Add(audit_Log);
					entities.SaveChanges();
					return true;
				}
			}
			catch 
			{
				return false;
			}
		}

		public static IEnumerable<AUDIT_LOG> LoadAuditLogByDate(string optDate)
		{
			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				DateTime date = DateTime.Parse(optDate);

				var entry = entities.AUDIT_LOG
									.Where(e => DbFunctions.TruncateTime(e.AUDIT_DATE) == DbFunctions.TruncateTime(date))
									.OrderByDescending(e => e.AUDIT_ID)
									//.Take(50)
									.ToList();
				return entry;
			}
		}

        public static IEnumerable<AUDIT_LOG> LoadAuditLogByDateRange(string strDateFrom, string strDateTo)
        {
            if (!(strDateFrom != null && strDateTo != null)) return new List<AUDIT_LOG>();

            using (GUIDEDBEntities entities = new GUIDEDBEntities())
            {
                DateTime dateFrom = DateTime.Parse(strDateFrom);
                DateTime dateTo = DateTime.Parse(strDateTo);

                var entry = entities.AUDIT_LOG
                                    .Where(e => DbFunctions.TruncateTime(e.AUDIT_DATE) >= DbFunctions.TruncateTime(dateFrom)
                                             && DbFunctions.TruncateTime(e.AUDIT_DATE) <= DbFunctions.TruncateTime(dateTo))
                                    .OrderByDescending(e => e.AUDIT_ID)
                                    //.Take(50)
                                    .ToList();
                return entry;
            }
        }

    }

}



