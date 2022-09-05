using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using mhcb.syd.DataAccess;
using mhcb.syd.DataAccess.View;
using mhcb.syd.Interface;

namespace mhcb.syd.BusinessLayer
{
	public class FXTransactionExtBusiness : IFXTransactionExt
	{
		public IEnumerable<FXTransactionExtView> GetPendingTrans()
		{

			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				string[] validStatus = { "F", "A", "V" };
				DateTime date = DateTime.Now;
				DateTime defaultDatetime = DateTime.Parse("1900-01-01 00:00:00");

				var entry = (from b in entities.FX_TRANSACTION_EXT
							 .Where(e => e.Printed == true
									&& e.GIDUpload == true
									&& DbFunctions.TruncateTime(e.PrintDateTime) == DbFunctions.TruncateTime(date)
							 //&& e.GIDUpload == true       )                            
							 ).OrderByDescending(e => e.PrintDateTime)
							 select new FXTransactionExtView()
							 {
								 REF_NO = b.REF_NO,
								 ContractId = b.ContractId,
								 TradeId = b.TradeId,
								 Event = b.Event,
								 PortfolioGroup = b.PortfolioGroup,
								 PortfolioDesk = b.PortfolioDesk,
								 PortfolioBook = b.PortfolioBook,
								 AuthorisedDealer = b.AuthorisedDealer,
								 CONTRACT_CCY_ABBR_ORIG = b.CONTRACT_CCY_ABBR_ORIG,

								 EQUIV_CCY_ABBR_ORIG = b.EQUIV_CCY_ABBR_ORIG,
								 Printed = (b.Printed ?? false) ? "Y" : "N",
								 PrintDateTime = (DbFunctions.TruncateTime(b.PrintDateTime).ToString()).Substring(0, 10),
								 GIDUpload = b.GIDUpload ? "Ture" : "False",
							 })

							.ToList();
				return entry;
			}
		}

		public string updateStatus(FXGIDStatus status)
		{
			try
			{
				using (GUIDEDBEntities entities = new GUIDEDBEntities())
				{
					DateTime date = DateTime.Parse(status.optDate);

					var entry = entities.FX_TRANSACTION_EXT
						.Where(e => e.Printed == true
								&& e.REF_NO == status.REF_NO
								&& DbFunctions.TruncateTime(e.PrintDateTime) == DbFunctions.TruncateTime(date)
								//&& e.GIDUpload == true 
								)
						.FirstOrDefault();

					if (entry != null)
					{

						AUDIT_LOG auditLog = new AUDIT_LOG
						{
							USER_ID = status.AUTHORIZE_BY,
							AUDIT_DATE = DateTime.Now,
							REF_NO = status.REF_NO.ToString(),
							TRANS_NO = "",
							AUDIT_DESC = "Reset FX GID Upload",
							APP_NAME = "GUIDE",
							TABLE_NAME = "FX Transaction Ext",
							ORIGINAL_VALUE = entry.GIDUpload.ToString(),
							NEW_VALUE = "false",
						};

						entry.GIDUpload = false;
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
