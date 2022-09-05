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
	public class EUCOnDemandBatchBusiness : IEUCOnDemandBatch
	{
		public IEnumerable<OnDemandBatchView> GetOnDemandBatchNoByDate(string createdDate)
		{
			using (EUCDbArchiveDBEntities entities = new EUCDbArchiveDBEntities())
			{
				DateTime date = DateTime.Parse(createdDate);

				var entry = (from b in entities.OnDemand_Batch
							 where DbFunctions.TruncateTime(b.CreatedWhen) == DbFunctions.TruncateTime(date)  //DateTime.Today
							 select new OnDemandBatchView()
							 {
								 BatchId = b.BatchId,
								 BatchNo = b.BatchNo,

								 Started = (b.Started == true ? "Yes" : "No"),
								 Completed = (b.Completed == true ? "Yes" : "No"),
								 Errored = (b.Errored == true ? "Yes" : "No"),
								 IsRec = (b.IsRec == true ? "Yes" : "No"),

								 WhoRequested = b.WhoRequested,
								 CreatedWhen = (DbFunctions.TruncateTime(b.CreatedWhen).ToString()).Substring(0, 19),
								 UpdatedWhen = (DbFunctions.TruncateTime(b.UpdatedWhen).ToString()).Substring(0, 19),
							 })
							 .ToList();
				return entry;
			}
		}

		public IEnumerable<OnDemandBatchLogView> GetOnDemandBatchLogByBatchNo(string strBatchId)
		{
			using (EUCDbArchiveDBEntities entities = new EUCDbArchiveDBEntities())
			{
				int intBatchId = Int16.Parse(strBatchId);

				var entry = (from b in entities.OnDemand_Batch_Log
							 where b.BatchId == intBatchId
							 //&& (DbFunctions.TruncateTime(b.When) == DateTime.Today)
							 select new OnDemandBatchLogView()
							 {
								 LogId = b.LogId,
								 BatchId = b.BatchId,
								 Level = b.Level,
								 Message = b.Message,
								 CreatedWhen = (DbFunctions.TruncateTime(b.When).ToString()).Substring(0, 19),
							 })
							 .ToList();
				return entry;
			}
		}

		public string UpdateStatus(OnDemandBatchAttribute onDemandBatchAttribute)
		{
			try
			{
				using (EUCDbArchiveDBEntities entities = new EUCDbArchiveDBEntities())
				{
					var entry = entities.OnDemand_Batch.FirstOrDefault(
									e => e.BatchNo == onDemandBatchAttribute.BatchNo
									&& e.Completed == true
									&& DbFunctions.TruncateTime(e.CreatedWhen) == DateTime.Today
									);

					if (entry != null)
					{
						AUDIT_LOG auditLog = new AUDIT_LOG
						{
							USER_ID = onDemandBatchAttribute.AUTHORIZE_BY,
							AUDIT_DATE = DateTime.Now,
							REF_NO = onDemandBatchAttribute.BatchNo.ToString(),
							TRANS_NO = "",
							AUDIT_DESC = "Set Batch to Completed",
							APP_NAME = "EUCDbArchive",
							TABLE_NAME = "OnDemand_Batch",
							ORIGINAL_VALUE = (entry.Completed == true ? "Yes" : "No"),
							NEW_VALUE = "Yes",
						};

						entry.Completed = true;
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
