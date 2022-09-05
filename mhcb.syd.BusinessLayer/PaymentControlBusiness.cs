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
	public class PaymentControlBusiness : IPaymentControl
	{
		//part I
		public IEnumerable<SwiftPaymentView> getPaymentControlPaymentByDate(string strDateValue, string dateType)
		{
			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				if (strDateValue == null) return new List<SwiftPaymentView>();

				DateTime date = DateTime.Parse(strDateValue);

				var entry = (from pp in entities.PaymentControl_Payment
								 //join sp in entities.PaymentControl_SwiftPayment on pp.PaymentId equals sp.PaymentId
							 join st in entities.PaymentControl_Status on pp.StatusId equals st.StatusId
							 join fc in entities.FXFCcies on pp.PaymentCurrencyCode equals fc.CCY_CD

							 join ppf in entities.PaymentControl_ProductReference on pp.ProductReferenceId equals ppf.ProductReferenceId
							 join ppt in entities.PaymentControl_Product on ppf.ProductId equals ppt.ProductId

							 where (dateType == "ReleasedDate") && (DbFunctions.TruncateTime(pp.ReleaseDate) == DbFunctions.TruncateTime(date))
							 || (dateType == "ValueDate") && (DbFunctions.TruncateTime(pp.ValueDate) == DbFunctions.TruncateTime(date))
							 orderby pp.PaymentId
							 select new SwiftPaymentView()
							 {
								 PaymentId = pp.PaymentId,
								 CustomerAbbreviation = pp.CustomerAbbreviation,
								 ValueDate = (DbFunctions.TruncateTime(pp.ValueDate).ToString()).Substring(0, 10),
								 ReleaseDate = (DbFunctions.TruncateTime(pp.ReleaseDate).ToString()).Substring(0, 10),
								 PaymentCurrencyCode = fc.CCY,
								 Amount = pp.PaymentAmount,

								 //StatusId = pp.StatusId,
								 StatusDescription = st.Description,
								 GBaseRefNo = pp.GBaseRefNo,

								 Product = ppt.Code + " " + ppf.Code

								 //StatusId = sp.StatusId,
								 //GBaseRefNo = sp.GBaseRefNo,

								 //SwiftPaymentId = sp.SwiftPaymentId,
								 //Processed = sp.Processed,
								 //PaymentDate = sp.PaymentDate,
								 //Currency = sp.Currency,
								 //Amount = sp.Amount,
								 //SenderBank = sp.SenderBank,
								 //ReceiverBank = sp.ReceiverBank,
								 //CreatedWhen = sp.CreatedWhen,
							 })
							.ToList();
				return entry;
			}
		}

		public string UpdateStatus(SwiftPaymentStatus status)
		{
			try
			{
				using (GUIDEDBEntities entities = new GUIDEDBEntities())
				{
					var entry = entities.PaymentControl_Payment
							   .Where(e => e.PaymentId == status.PaymentId)
							   .FirstOrDefault();

					if (entry != null)
					{
						var preStatus = entry.StatusId;

						AUDIT_LOG auditLog = new AUDIT_LOG
						{
							USER_ID = status.AUTHORIZE_BY,
							AUDIT_DATE = DateTime.Now,

							REF_NO = entry.GBaseRefNo.ToString(),
							TRANS_NO = status.PaymentId.ToString(),
							AUDIT_DESC = "Change PaymentControl Status",
							APP_NAME = "PaymentControl",
							TABLE_NAME = "PaymentControl_Payment",
							ORIGINAL_VALUE = preStatus.ToString(),
							NEW_VALUE = status.StatusId.ToString(),
						};

						entry.StatusId = status.StatusId;
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

		//part II
		public IEnumerable<SwiftPaymentView> getSwiftPaymentByDate(string strDateValue, string dateType)
		{
			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				if (strDateValue == null) return new List<SwiftPaymentView>();

				DateTime date = DateTime.Parse(strDateValue);

				var entry = (from pp in entities.PaymentControl_Payment
							 join sp in entities.PaymentControl_SwiftPayment on pp.PaymentId equals sp.PaymentId
							 join st in entities.PaymentControl_Status on sp.StatusId equals st.StatusId

							 where (dateType == "CreatedDate") && (DbFunctions.TruncateTime(sp.CreatedWhen) == DbFunctions.TruncateTime(date))
							|| (dateType == "PaymentDate") && (DbFunctions.TruncateTime(sp.PaymentDate) == DbFunctions.TruncateTime(date))
							|| (dateType == "ValueDate") && (DbFunctions.TruncateTime(pp.ValueDate) == DbFunctions.TruncateTime(date))

							 orderby pp.PaymentId, sp.SwiftPaymentId
							 select new SwiftPaymentView()
							 {
								 PaymentId = pp.PaymentId,
								 CustomerAbbreviation = pp.CustomerAbbreviation,
								 ValueDate = (DbFunctions.TruncateTime(pp.ValueDate).ToString()).Substring(0, 10),
								 ReleaseDate = (DbFunctions.TruncateTime(pp.ReleaseDate).ToString()).Substring(0, 10),
								 PaymentCurrencyCode = sp.Currency,

								 //StatusId = pp.StatusId,
								 //GBaseRefNo = pp.GBaseRefNo,
								 StatusId = sp.StatusId,
								 StatusDescription = st.Description,
								 GBaseRefNo = sp.GBaseRefNo,

								 SwiftPaymentId = sp.SwiftPaymentId,
								 Processed = sp.Processed,
								 PaymentDate = (DbFunctions.TruncateTime(sp.PaymentDate).ToString()).Substring(0, 10),
								 Currency = sp.Currency,
								 Amount = sp.Amount,
								 SenderBank = sp.SenderBank,
								 ReceiverBank = sp.ReceiverBank,
								 CreatedWhen = (DbFunctions.TruncateTime(sp.CreatedWhen).ToString()).Substring(0, 10),
							 })
							.ToList();
				return entry;
			}
		}

		public string DeleteSwiftPayment(SwiftPaymentStatus status)
		{
			try
			{
				using (GUIDEDBEntities entities = new GUIDEDBEntities())
				{
					var swiftPayment = entities.PaymentControl_SwiftPayment
									  .Where(e => e.SwiftPaymentId == status.SwiftPaymentId)
									  .FirstOrDefault();

					var entryLogs = entities.PaymentControl_SwiftPaymentLog
								   .Where(e => e.SwiftPaymentId == status.SwiftPaymentId)
								   .ToList();

					var entryAcks = entities.PaymentControl_SwiftAck
						.Where(e => e.SwiftPaymentId == status.SwiftPaymentId)
						.ToList();

					if (swiftPayment != null)
					{
						var preStatus = swiftPayment.StatusId;
						var REF_NO = swiftPayment.GBaseRefNo.ToString();

						AUDIT_LOG auditLog = new AUDIT_LOG
						{
							USER_ID = status.AUTHORIZE_BY,
							AUDIT_DATE = DateTime.Now,

							REF_NO = swiftPayment.GBaseRefNo.ToString(),
							TRANS_NO = status.SwiftPaymentId.ToString(),
							AUDIT_DESC = "Delete SwiftPayment Transaction",
							APP_NAME = "PaymentControl",
							TABLE_NAME = "PaymentControl_SwiftPayment",
							ORIGINAL_VALUE = preStatus.ToString(),
							NEW_VALUE = "n/a",
						};

						entities.PaymentControl_SwiftPaymentLog.RemoveRange(entryLogs);
						entities.SaveChanges();

						entities.PaymentControl_SwiftAck.RemoveRange(entryAcks);
						entities.SaveChanges();

						entities.PaymentControl_SwiftPayment.Remove(swiftPayment);
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


