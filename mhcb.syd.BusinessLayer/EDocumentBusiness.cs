using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using mhcb.syd.DataAccess.View;
using mhcb.syd.DataAccess;
using mhcb.syd.Interface;

namespace mhcb.syd.BusinessLayer
{
	public class EDocumentBusiness : IEDocument
	{
		public IEnumerable<eDocumentView> GetPendingTrans(string eDocType, string optDate)
		{

			if (optDate == null) return new List<eDocumentView>();

			using (eDocumentDBEntities entities = new eDocumentDBEntities())
			{
				DateTime date = DateTime.Parse(optDate);

				List<eDocumentView> entry;
				//entities.Configuration.LazyLoadingEnabled = false;
				// Or: entities.Configuration.ProxyCreationEnabled = false;
				switch (eDocType)
				{
					case "CurrentAndSavingsStatement":
						entry = (from g in entities.gBaseDocuments
								 join s in entities.Statements on g.Id equals s.gBaseDocumentId
								 where g.ReportType.Contains(eDocType)
										&& DbFunctions.TruncateTime(g.DateProcessed) == DbFunctions.TruncateTime(date)
								 select new eDocumentView()
								 {
									 Id = g.Id,
									 ReportType = g.ReportType,
									 ReportFileName = g.ReportFileName,
									 Processed = g.Processed ? "Y" : "N",

									 DateProcessed = (DbFunctions.TruncateTime(g.DateProcessed).ToString()).Substring(0, 10),
									 Errored = g.Errored ? "Y" : "N",
									 ErrorMessage = g.ErrorMessage ?? "",

									 CompanyName = s.CompanyName,
									 AccountNo = s.AccountNo,
									 //ReferenceNumber = 'n/a',
									 AccountType = s.AccountType
								 })
								 .ToList();
						break;
					//case "MoneyMarketDepositStatement":
					//      not available
					//case "MoneyMarketLoanStatement":
					//      not available
					case "MoneyMarketLoanConfirmation":
					case "MoneyMarketDepositConfirmation":
						entry = (from g in entities.gBaseDocuments
								 join s in entities.MoneyMarketConfirmations on g.Id equals s.gBaseDocumentId
								 where g.ReportType.Contains(eDocType)
								   && DbFunctions.TruncateTime(g.DateProcessed) == DbFunctions.TruncateTime(date)
								 select new eDocumentView()
								 {
									 Id = g.Id,
									 ReportType = g.ReportType,
									 ReportFileName = g.ReportFileName,
									 Processed = g.Processed ? "Y" : "N",
									 DateProcessed = (DbFunctions.TruncateTime(g.DateProcessed).ToString()).Substring(0, 10),
									 Errored = g.Errored ? "Y" : "N",
									 ErrorMessage = g.ErrorMessage ?? "",

									 CompanyName = s.CompanyName,
									 AccountNo = s.ReferenceNumber,
									 //ReferenceNumber = 'n/a',
									 AccountType = "n/a"
								 })
							  .ToList();

						break;
					case "ForeignExchangeConfirmation":
						entry = (from g in entities.gBaseDocuments
								 join s in entities.ForeignExchangeConfirmations on g.Id equals s.gBaseDocumentId
								 where g.ReportType.Contains(eDocType)
									   && DbFunctions.TruncateTime(g.DateProcessed) == DbFunctions.TruncateTime(date)
								 select new eDocumentView()
								 {
									 Id = g.Id,
									 ReportType = g.ReportType,
									 ReportFileName = g.ReportFileName,
									 Processed = g.Processed ? "Y" : "N",
									 DateProcessed = (DbFunctions.TruncateTime(g.DateProcessed).ToString()).Substring(0, 10),
									 Errored = g.Errored ? "Y" : "N",
									 ErrorMessage = g.ErrorMessage ?? "",

									 CompanyName = s.CompanyName,
									 AccountNo = s.ReferenceNumber,
									 //ReferenceNumber = 'n/a',
									 AccountType = "n/a"
								 })
								 .ToList();
						break;

					default:
						entry = null;
						break;
				}
				return entry;
			}
		}

		public string updateStatus(EDocumentAttribute eDocAttribute)
		{
			try
			{
				using (eDocumentDBEntities entities = new eDocumentDBEntities())
				{
					var entry = entities.gBaseDocuments
							.Where(x => x.Id == eDocAttribute.Id)
							.FirstOrDefault();

					if (entry != null)
					{
						AUDIT_LOG auditLog = new AUDIT_LOG
						{
							USER_ID = eDocAttribute.AUTHORIZE_BY,
							AUDIT_DATE = DateTime.Now,
							REF_NO = eDocAttribute.Id.ToString(),
							TRANS_NO = "",
							AUDIT_DESC = "Sent " + eDocAttribute.ReportType + " to reprint",
							APP_NAME = "eDocument",
							TABLE_NAME = "gBaseDocuments",
							ORIGINAL_VALUE = entry.Processed.ToString(),
							NEW_VALUE = "false",
						};

						entry.ErrorMessage = "RePrinted";
						entry.Errored = false;
						entry.Processed = false;

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

		//public static string updateStatus_notuse(EDocumentType eDocType)
		//{
		//    try
		//    {
		//        using (eDocumentDBEntities entities = new eDocumentDBEntities())
		//        {
		//            gBaseDocument entry;
		//            int gBaseDocumentId;

		//            //entities.Configuration.LazyLoadingEnabled = false;
		//            // Or: entities.Configuration.ProxyCreationEnabled = false;
		//            switch (eDocType.ReportType)
		//            {
		//                case "CurrentAndSavingsStatement":
		//                    //entry = entities.gBaseDocuments.Where(b => b.ReportType == eDocType.ReportType)
		//                    //			.Select(b => new
		//                    //			{
		//                    //				b,
		//                    //				Statements = b.Statements
		//                    //								.Where(p => p.AccountNo == eDocType.AccRefNo)
		//                    //			})
		//                    //			.AsEnumerable()
		//                    //			.Select(x => x.b)
		//                    //			.OrderByDescending(b => b.Id)
		//                    //			.FirstOrDefault();
		//                    gBaseDocumentId = entities.Statements
		//                                  .Where(e => e.AccountNo.Contains(eDocType.AccRefNo))
		//                                  .OrderByDescending(e => e.gBaseDocumentId)
		//                                  .Select(e => e.gBaseDocumentId)
		//                                  .FirstOrDefault();

		//                    entry = entities.gBaseDocuments
		//                                    .Where(e => e.ReportType == eDocType.ReportType
		//                                                && e.Id == gBaseDocumentId)
		//                                    .FirstOrDefault();

		//                    break;
		//                //case "MoneyMarketDepositStatement":
		//                //      not available
		//                //case "MoneyMarketLoanStatement":
		//                //      not available
		//                case "MoneyMarketLoanConfirmation":
		//                case "MoneyMarketDepositConfirmation":
		//                    gBaseDocumentId = entities.MoneyMarketConfirmations
		//                                          .Where(e => e.ReferenceNumber.Contains(eDocType.AccRefNo))
		//                                          .OrderByDescending(e => e.gBaseDocumentId)
		//                                          .Select(e => e.gBaseDocumentId)
		//                                          .FirstOrDefault();
		//                    entry = entities.gBaseDocuments
		//                                    .Where(e => e.ReportType == eDocType.ReportType
		//                                                && e.Id == gBaseDocumentId)
		//                                    .FirstOrDefault();

		//                    break;
		//                case "ForeignExchangeConfirmation":
		//                    gBaseDocumentId = entities.ForeignExchangeConfirmations
		//                                      .Where(e => e.ReferenceNumber.Contains(eDocType.AccRefNo))
		//                                      .OrderByDescending(e => e.gBaseDocumentId)
		//                                      .Select(e => e.gBaseDocumentId)
		//                                      .FirstOrDefault();
		//                    entry = entities.gBaseDocuments
		//                                    .Where(e => e.ReportType == eDocType.ReportType
		//                                                && e.Id == gBaseDocumentId)
		//                                    .FirstOrDefault();
		//                    break;

		//                default:
		//                    //break;
		//                    return null;
		//            }


		//            if (entry != null)
		//            {
		//                var processed = entry.Processed.ToString();

		//                AUDIT_LOG auditLog = new AUDIT_LOG
		//                {
		//                    USER_ID = eDocType.AUTHORIZE_BY,
		//                    AUDIT_DATE = DateTime.Now,
		//                    REF_NO = eDocType.AccRefNo,
		//                    TRANS_NO = "",
		//                    AUDIT_DESC = "Sent " + eDocType.ReportType + " to reprint",
		//                    APP_NAME = "eDocument",
		//                    TABLE_NAME = "gBaseDocuments",
		//                    ORIGINAL_VALUE = processed,
		//                    NEW_VALUE = "false",
		//                };


		//                entry.ErrorMessage = "RePrinted";
		//                entry.Errored = false;
		//                entry.Processed = false;

		//                entities.SaveChanges();
		//                AuditLog.InsertAuditLog(auditLog);
		//                return "OK";
		//            }
		//            else
		//            {
		//                return "NotFound";
		//            }
		//        }
		//    }
		//    catch (Exception ex)
		//    {
		//        return ex.ToString();
		//    }
		//}

	}
}
