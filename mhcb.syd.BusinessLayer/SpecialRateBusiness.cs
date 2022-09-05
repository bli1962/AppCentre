using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using mhcb.syd.DataAccess;
using mhcb.syd.DataAccess.View;
using mhcb.syd.Interface;

namespace mhcb.syd.BusinessLayer
{
	public class SpecialRateBusiness : ISpecialRate
	{
		public IEnumerable<ExchRateView> LoadSpcialRate()
		{
			using (LoanDBEntities entities = new LoanDBEntities())
			{
				string[] CCYs = { "AUD", "NZD", "SGD", "JPY", "EUR", "HKD", "GBP" };

				var entry = (from e in entities.EXCH_RATE
							 where CCYs.Contains(e.CCY_CODE)
							 orderby e.CCY_CODE
							 select new ExchRateView()
							 {
								 CCY_CODE = e.CCY_CODE,
								 DKB_SPECIAL_RATE_USD = e.DKB_SPECIAL_RATE_USD
							 })
							.ToList();
				return entry;
			}
		}

		public string updateStatus(RateAttribute rate)
		{
			try
			{
				using (LoanDBEntities entities = new LoanDBEntities())
				{
					var entry = entities.EXCH_RATE
						.FirstOrDefault(e => e.CCY_CODE == rate.CcyCode);

					if (entry != null)
					{
						AUDIT_LOG auditLog = new AUDIT_LOG
						{
							USER_ID = rate.AUTHORIZE_BY,
							AUDIT_DATE = DateTime.Now,
							REF_NO = entry.CCY_CODE,
							TRANS_NO = "",
							AUDIT_DESC = "Update Mizuho Specail Rate",
							APP_NAME = "LOAN",
							TABLE_NAME = "EXCH_RATE",
							ORIGINAL_VALUE = entry.DKB_SPECIAL_RATE_USD.ToString(),
							NEW_VALUE = rate.Rate.ToString(),
						};

						entry.DKB_SPECIAL_RATE_USD = rate.Rate;
						entities.SaveChanges();
						AuditLog.InsertAuditLog(auditLog);
						return "OK";
					}
					else
					{
						return "NotFound";
					}
				}
			}
			catch (Exception ex)
			{
				return ex.ToString();
			}
		}
	}
}
