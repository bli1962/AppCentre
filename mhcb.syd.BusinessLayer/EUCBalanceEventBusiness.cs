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
	public class EUCBalanceEventBusiness : IEUCBalanceEvent
	{
		public IEnumerable<MxEucBalanceEvent> getEUCBalanceEventByCustAbbr(string custAbbr, string optDate)
		{
			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				DateTime date = DateTime.Parse(optDate);

				var entry = entities.MxEucBalanceEvents
									.Where(e => e.CustomerAbbreviation.Contains(custAbbr)
											&& DbFunctions.TruncateTime(e.StatusUpdateTime) == DbFunctions.TruncateTime(date))
									.OrderByDescending(e => e.Id)
									//.Take(50)
									.ToList();
				return entry;
			}
		}

		public IEnumerable<MxEucBalanceEvent> getEUCBalanceEventByRefno(string refNo, string optDate)
		{
			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				DateTime date = DateTime.Parse(optDate);

				var entry = entities.MxEucBalanceEvents
									.Where(e => e.GBaseReferenceNo.Contains(refNo)
										&& DbFunctions.TruncateTime(e.StatusUpdateTime) == DbFunctions.TruncateTime(date))
									.OrderByDescending(e => e.Id)
									//.Take(50)
									.ToList();
				return entry;
			}
		}

		public IEnumerable<MxInboundEventLogView> getMxInboundEventLogBySender(string sender, string optDate)
		{
			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				DateTime currentYear = DateTime.Now;
				DateTime date = DateTime.Parse(optDate);

				var entry = (from me in entities.MxInboundEvents
							 join ml in entities.MxInboundEventLogs on me.MessageId.ToString() equals ml.EventId
							 join ms in entities.MxInboundMessageStatus on me.StatusId equals ms.StatusId
							 where DbFunctions.DiffYears(ml.Timestamp, currentYear) == 0
								&& DbFunctions.TruncateTime(ml.Timestamp) == DbFunctions.TruncateTime(date)
								&& me.SendingSystem.ToLower() == sender.ToLower()
							 select new MxInboundEventLogView()
							 {
								 MessageId = me.MessageId,
								 SendingSystem = me.SendingSystem,
								 Category = me.Category,
								 EventType = me.EventType,
								 StatusName = ms.StatusName,
								 //mb.GBaseReferenceNo,
								 //mb.CustomerAbbreviation,
								 //mb.BatchNo,
								 LogId = ml.LogId,
								 Logger = ml.Logger,
								 Description = ml.Description,
								 Status = ml.Status,
							 }).OrderByDescending(x => x.MessageId).OrderByDescending(y => y.LogId)
							//.Take(50)
							.ToList();
				return entry;
			}
		}

		public IEnumerable<MxEucBalanceEventView> GetPendingTrans()
		{
			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				var entry = (from b in entities.MxEucBalanceEvents
							 where b.EventType.ToLower() == "deletion".ToLower()
								   && b.Category.ToLower() == "CallDeposit".ToLower()
								   && DbFunctions.TruncateTime(b.StatusUpdateTime) == DateTime.Today
							 select new MxEucBalanceEventView()
							 {
								 Id = b.Id,
								 EventId = b.EventId,
								 BatchNo = b.BatchNo,
								 GBaseReferenceNo = b.GBaseReferenceNo,
								 CustomerAbbreviation = b.CustomerAbbreviation,
								 Category = b.Category,
								 EventType = b.EventType,
								 ClosingDate = b.ClosingDate,
								 TradeDate = b.TradeDate,
								 EffectiveDate = b.EffectiveDate,
								 TimeSent = (DbFunctions.TruncateTime(b.TimeSent).ToString()).Substring(0, 19),
								 Status = b.Status,
								 StatusUpdateTime = (DbFunctions.TruncateTime(b.StatusUpdateTime).ToString()).Substring(0, 19),
							 })
							 .ToList();
				return entry;
			}
		}

		public string UpdateStatus(EucBalanceAttribute balanceAttribute)
		{
			try
			{
				using (GUIDEDBEntities entities = new GUIDEDBEntities())
				{
					var entry = entities.MxEucBalanceEvents
								.FirstOrDefault(e => e.GBaseReferenceNo.ToLower() == balanceAttribute.GBaseReferenceNo.ToLower() && e.EventType.ToLower() == "deletion".ToLower()
									&& e.Category.ToLower() == "CallDeposit".ToLower()
									&& DbFunctions.TruncateTime(e.StatusUpdateTime) == DateTime.Today);

					if (entry != null)
					{
						string strClosedate = DateTime.Now.ToString("yyMMdd").ToString();  // for example "201230";

						AUDIT_LOG auditLog = new AUDIT_LOG
						{
							USER_ID = balanceAttribute.AUTHORIZE_BY,
							AUDIT_DATE = DateTime.Now,
							REF_NO = balanceAttribute.GBaseReferenceNo.ToString(),
							TRANS_NO = "",
							AUDIT_DESC = "Append to Closing Date",
							APP_NAME = "GUIDE",
							TABLE_NAME = "Mx EUC Balance",
							ORIGINAL_VALUE = "",
							NEW_VALUE = strClosedate,
						};

						entry.ClosingDate = strClosedate;
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
