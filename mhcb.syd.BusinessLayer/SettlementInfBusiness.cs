using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using mhcb.syd.DataAccess;
using mhcb.syd.DataAccess.View;
using mhcb.syd.Interface;

namespace mhcb.syd.BusinessLayer
{
	public class SettlementInfBusiness : ISettlementInf
	{
		public IEnumerable<SettlementInfView> GetPendingTrans()
		{

			//Begin tran

			//	Update [GUIDE].[dbo].[SETTLEMENT_INF]
			//	Set STATUS = 'F'
			//	where STATUS not in ('F', 'A', 'V' )

			//	Update [GUIDE].[dbo].[SETTLEMENT_INF]
			//	Set STATUS = 'S', AUTHORIZE_BY = 'DL002041', AUTHORIZE_ON=getdate(), ADD_ON = getdate()
			//	where SIF_ID  in ('3326', '3325' )

			//	Update [GUIDE].[dbo].[SETTLEMENT_INF]
			//	Set STATUS = 'S', AUTHORIZE_BY = 'GW003368', AUTHORIZE_ON=getdate(), ADD_ON = getdate()
			//	where SIF_ID  in ('3324', '3323' )

			//	Update [GUIDE].[dbo].[SETTLEMENT_INF]
			//	Set STATUS = 'S', AUTHORIZE_BY = 'UA003086', AUTHORIZE_ON=getdate(), ADD_ON = getdate()
			//	where SIF_ID  in ('3322', '3321' )

			//	Update [GUIDE].[dbo].[SETTLEMENT_INF]
			//	Set STATUS = 'S', AUTHORIZE_BY = 'MM003292', AUTHORIZE_ON=getdate(), ADD_ON = getdate()
			//	where SIF_ID  in ('3320', '3319' )

			//	Update [GUIDE].[dbo].[SETTLEMENT_INF]
			//	Set STATUS = 'S', AUTHORIZE_BY = 'BL007010', AUTHORIZE_ON=getdate(), ADD_ON = getdate()
			//	where SIF_ID  in ('3318', '3317' )

			//	SELECT TOP (100) *
			//	FROM [GUIDE].[dbo].[SETTLEMENT_INF]
			//	where STATUS not in ('F', 'A', 'V' )
			//	order by [ADD_ON] desc

			//--Commit tran
			//--rollback tran

			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				string[] validStatus = { "F", "A", "V" };

				var entry = (from s in entities.SETTLEMENT_INF
							 join f in entities.FXFSwifts on s.SIF_ID equals f.SIF_ID
								into fxfSwifts
							 from f1 in fxfSwifts.DefaultIfEmpty()
							 join y in entities.FXFCcies on f1.CCY_CD equals y.CCY_CD
								into fxfccy
							 from y1 in fxfccy.DefaultIfEmpty()
							 where !validStatus.Contains(s.STATUS)
							 select new SettlementInfView()
							 {
								 SIF_ID = s.SIF_ID,
								 CUST_ABBR = s.CUST_ABBR,
								 RECORD_KIND =
											(
												s.RECORD_KIND == "0" ? "FX" :
												s.RECORD_KIND == "2" ? "MM" :
												s.RECORD_KIND == "6" ? "Swap" : "Unknown"
											),
								 CCY = y1.CCY,
								 STATUS = s.STATUS,
								 AUTHORIZE_BY = s.AUTHORIZE_BY,
								 DELETION_STATUS = s.DELETION_STATUS,
								 TRAN_NO = s.TRAN_NO,
								 GIP_STATUS = s.GIP_STATUS,
								 GIP_DESCRIPTION = s.GIP_DESCRIPTION,

								 Addresse = f1.Addresse,
								 PartyIdentifier56 = f1.PartyIdentifier56,
								 BIC56_1 = f1.BIC56_1,
								 PartyIdentifier57 = f1.PartyIdentifier57,
								 FXALM_PartyIdentifier57 = f1.FXALM_PartyIdentifier57,
								 BIC57_1 = f1.BIC57_1,
								 BIC57_2 = f1.BIC57_2,
								 BIC57_3 = f1.BIC57_3,
								 BIC57_4 = f1.BIC57_4,
								 PartyIdentifier58 = f1.PartyIdentifier58,
								 FXALM_PartyIdentifier58 = f1.FXALM_PartyIdentifier58,
								 BIC58_1 = f1.BIC58_1,
								 PartyIdentifier59 = f1.PartyIdentifier59,
								 BIC59_1 = f1.BIC59_1,
								 BIC59_2 = f1.BIC59_2,
								 BIC59_3 = f1.BIC59_3,
								 BIC59_4 = f1.BIC59_4,
								 SpecialInstruction_1 = f1.SpecialInstruction_1,
								 SpecialInstruction_2 = f1.SpecialInstruction_2,
								 SpecialInstruction_3 = f1.SpecialInstruction_3,
								 SpecialInstruction_4 = f1.SpecialInstruction_4,
							 })
							   .ToList();
				return entry;
			}
		}

		public IEnumerable<SettlementInfView> LoadSettlementInfBySifId(int id)
		{
			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{

				var entry = (from s in entities.SETTLEMENT_INF
							 join f in entities.FXFSwifts on s.SIF_ID equals f.SIF_ID
								into fxfSwifts
							 from f1 in fxfSwifts.DefaultIfEmpty()
							 join y in entities.FXFCcies on f1.CCY_CD equals y.CCY_CD
								into fxfccy
							 from y1 in fxfccy.DefaultIfEmpty()
							 where s.SIF_ID == id
							 select new SettlementInfView()
							 {
								 SIF_ID = s.SIF_ID,
								 CUST_ABBR = s.CUST_ABBR,
								 RECORD_KIND =
											(
												s.RECORD_KIND == "0" ? "FX" :
												s.RECORD_KIND == "2" ? "MM" :
												s.RECORD_KIND == "6" ? "Swap" : "Unknown"
											),
								 CCY = y1.CCY,
								 STATUS = s.STATUS,
								 DELETION_STATUS = s.DELETION_STATUS,
								 TRAN_NO = s.TRAN_NO,
								 GIP_STATUS = s.GIP_STATUS,
								 GIP_DESCRIPTION = s.GIP_DESCRIPTION,
								 AUTHORIZE_BY = s.AUTHORIZE_BY,

								 Addresse = f1.Addresse,
								 PartyIdentifier56 = f1.PartyIdentifier56,
								 BIC56_1 = f1.BIC56_1,
								 PartyIdentifier57 = f1.PartyIdentifier57,
								 FXALM_PartyIdentifier57 = f1.FXALM_PartyIdentifier57,
								 BIC57_1 = f1.BIC57_1,
								 BIC57_2 = f1.BIC57_2,
								 BIC57_3 = f1.BIC57_3,
								 BIC57_4 = f1.BIC57_4,
								 PartyIdentifier58 = f1.PartyIdentifier58,
								 FXALM_PartyIdentifier58 = f1.FXALM_PartyIdentifier58,
								 BIC58_1 = f1.BIC58_1,
								 PartyIdentifier59 = f1.PartyIdentifier59,
								 BIC59_1 = f1.BIC59_1,
								 BIC59_2 = f1.BIC59_2,
								 BIC59_3 = f1.BIC59_3,
								 BIC59_4 = f1.BIC59_4,
								 SpecialInstruction_1 = f1.SpecialInstruction_1,
								 SpecialInstruction_2 = f1.SpecialInstruction_2,
								 SpecialInstruction_3 = f1.SpecialInstruction_3,
								 SpecialInstruction_4 = f1.SpecialInstruction_4,
							 })
							 .ToList();
				return entry;
			}
		}


		public IEnumerable<SettlementInfView> LoadSettlementInfByCustAbbr(string custAbbr)
		{
			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				var entry = (from s in entities.SETTLEMENT_INF
							 join f in entities.FXFSwifts on s.SIF_ID equals f.SIF_ID
								into fxfSwifts
							 from f1 in fxfSwifts.DefaultIfEmpty()
							 join y in entities.FXFCcies on f1.CCY_CD equals y.CCY_CD
								into fxfccy
							 from y1 in fxfccy.DefaultIfEmpty()
							 where s.CUST_ABBR.Contains(custAbbr)
							 select new SettlementInfView()
							 {
								 SIF_ID = s.SIF_ID,
								 CUST_ABBR = s.CUST_ABBR,
								 RECORD_KIND =
											(
												s.RECORD_KIND == "0" ? "FX" :
												s.RECORD_KIND == "2" ? "MM" :
												s.RECORD_KIND == "6" ? "Swap" : "Unknown"
											),
								 CCY = y1.CCY,
								 STATUS = s.STATUS,
								 DELETION_STATUS = s.DELETION_STATUS,
								 TRAN_NO = s.TRAN_NO,
								 GIP_STATUS = s.GIP_STATUS,
								 GIP_DESCRIPTION = s.GIP_DESCRIPTION,
								 AUTHORIZE_BY = s.AUTHORIZE_BY,

								 Addresse = f1.Addresse,
								 PartyIdentifier56 = f1.PartyIdentifier56,
								 BIC56_1 = f1.BIC56_1,
								 PartyIdentifier57 = f1.PartyIdentifier57,
								 FXALM_PartyIdentifier57 = f1.FXALM_PartyIdentifier57,
								 BIC57_1 = f1.BIC57_1,
								 BIC57_2 = f1.BIC57_2,
								 BIC57_3 = f1.BIC57_3,
								 BIC57_4 = f1.BIC57_4,
								 PartyIdentifier58 = f1.PartyIdentifier58,
								 FXALM_PartyIdentifier58 = f1.FXALM_PartyIdentifier58,
								 BIC58_1 = f1.BIC58_1,
								 PartyIdentifier59 = f1.PartyIdentifier59,
								 BIC59_1 = f1.BIC59_1,
								 BIC59_2 = f1.BIC59_2,
								 BIC59_3 = f1.BIC59_3,
								 BIC59_4 = f1.BIC59_4,
								 SpecialInstruction_1 = f1.SpecialInstruction_1,
								 SpecialInstruction_2 = f1.SpecialInstruction_2,
								 SpecialInstruction_3 = f1.SpecialInstruction_3,
								 SpecialInstruction_4 = f1.SpecialInstruction_4,
							 })
							   .ToList();
				return entry;
			}
		}

		public IEnumerable<SettlementInfView> LoadSettlementInfByDetails(string custAbbr, string ccyCD, string recordKind)
		{
			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				var entry = (from s in entities.SETTLEMENT_INF
							 join f in entities.FXFSwifts on s.SIF_ID equals f.SIF_ID
								into fxfSwifts
							 from f1 in fxfSwifts.DefaultIfEmpty()
							 join y in entities.FXFCcies on f1.CCY_CD equals y.CCY_CD
								into fxfccy
							 from y1 in fxfccy.DefaultIfEmpty()
							 where s.CUST_ABBR.Contains(custAbbr)
									&& s.CCY_CD == ccyCD
									&& s.RECORD_KIND == recordKind
							 select new SettlementInfView()
							 {
								 SIF_ID = s.SIF_ID,
								 CUST_ABBR = s.CUST_ABBR,
								 RECORD_KIND =
											(
												s.RECORD_KIND == "0" ? "FX" :
												s.RECORD_KIND == "2" ? "MM" :
												s.RECORD_KIND == "6" ? "Swap" : "Unknown"
											),
								 CCY = y1.CCY,
								 STATUS = s.STATUS,
								 DELETION_STATUS = s.DELETION_STATUS,
								 TRAN_NO = s.TRAN_NO,
								 GIP_STATUS = s.GIP_STATUS,
								 GIP_DESCRIPTION = s.GIP_DESCRIPTION,
								 AUTHORIZE_BY = s.AUTHORIZE_BY,

								 Addresse = f1.Addresse,
								 PartyIdentifier56 = f1.PartyIdentifier56,
								 BIC56_1 = f1.BIC56_1,
								 PartyIdentifier57 = f1.PartyIdentifier57,
								 FXALM_PartyIdentifier57 = f1.FXALM_PartyIdentifier57,
								 BIC57_1 = f1.BIC57_1,
								 BIC57_2 = f1.BIC57_2,
								 BIC57_3 = f1.BIC57_3,
								 BIC57_4 = f1.BIC57_4,
								 PartyIdentifier58 = f1.PartyIdentifier58,
								 FXALM_PartyIdentifier58 = f1.FXALM_PartyIdentifier58,
								 BIC58_1 = f1.BIC58_1,
								 PartyIdentifier59 = f1.PartyIdentifier59,
								 BIC59_1 = f1.BIC59_1,
								 BIC59_2 = f1.BIC59_2,
								 BIC59_3 = f1.BIC59_3,
								 BIC59_4 = f1.BIC59_4,
								 SpecialInstruction_1 = f1.SpecialInstruction_1,
								 SpecialInstruction_2 = f1.SpecialInstruction_2,
								 SpecialInstruction_3 = f1.SpecialInstruction_3,
								 SpecialInstruction_4 = f1.SpecialInstruction_4,
							 })
							   .ToList();
				return entry;
			}
		}

		public IEnumerable<SETTLEMENT_INF> LoadSettlementInfsByStatus(string status = "All")
		{
			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				List<SETTLEMENT_INF> entry;

				switch (status)
				{
					case "All":
						entry = entities.SETTLEMENT_INF.ToList();
						break;
					case "F":
						entry = entities.SETTLEMENT_INF.Where(e => e.STATUS == "F" && e.DELETION_STATUS == "").ToList();
						break;
					case "A":
						entry = entities.SETTLEMENT_INF.Where(e => e.STATUS == "A" && e.DELETION_STATUS == "").ToList();
						break;
					case "V":
						entry = entities.SETTLEMENT_INF.Where(e => e.STATUS == "V" && e.DELETION_STATUS == "").ToList();
						break;
					case "D":
						entry = entities.SETTLEMENT_INF.Where(e => e.STATUS == "F" && e.DELETION_STATUS == "D").ToList();
						break;
					default:
						//break;
						return null;
				}
				return entry;
			}
		}

		//public string InsertSettlementInf(SETTLEMENT_INF settlement_inf)
		//{
		//	try
		//	{
		//		using (GUIDEDBEntities entities = new GUIDEDBEntities())
		//		{
		//			entities.SETTLEMENT_INF.Add(settlement_inf);
		//			entities.SaveChanges();

		//			return settlement_inf.SIF_ID.ToString();
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		// HTTP/1.1 400 Not found  
		//		return ex.ToString();
		//	}
		//}

		public string DeleteSettlementInf(int id)
		{
			try
			{
				using (GUIDEDBEntities entities = new GUIDEDBEntities())
				{
					var entity = entities.SETTLEMENT_INF.FirstOrDefault(e => e.SIF_ID == id);

					if (entity != null)
					{
						entities.SETTLEMENT_INF.Remove(entity);
						entities.SaveChanges();
						return "OK";
					}
					else
					{
						return "not found";
					}
				}
			}
			catch (Exception ex)
			{
				return ex.ToString();
			}
		}

		public string updateStatus(SIFStatus status)
		{
			try
			{
				string[] validStatus = { "F", "A", "V" };

				using (GUIDEDBEntities entities = new GUIDEDBEntities())
				{
					var entry = entities.SETTLEMENT_INF
							   .Where(e => e.SIF_ID == status.SIF_ID
								   && e.AUTHORIZE_BY == status.AUTHORIZE_BY
								   && !validStatus.Contains(e.STATUS))
							   .FirstOrDefault();
					// .FirstOrDefault(e => e.SIF_ID == status.SIF_ID); 

					if (entry != null)
					{
						var delstatus = entry.DELETION_STATUS.ToString();
						var guideStatus = entry.STATUS.ToString();

						AUDIT_LOG auditLog = new AUDIT_LOG
						{
							USER_ID = status.AUTHORIZE_BY,
							AUDIT_DATE = DateTime.Now,
							REF_NO = status.SIF_ID.ToString(),
							TRANS_NO = status.TRAN_NO ?? "99999",
							AUDIT_DESC = "Sent SIF to finalised",
							APP_NAME = "GUIDE",
							TABLE_NAME = "Settlement Information",
							ORIGINAL_VALUE = guideStatus + " : " + delstatus,
							NEW_VALUE = status.STATUS + " : " + status.DELETION_STATUS,
						};

						entry.AUTHORIZE_BY = status.AUTHORIZE_BY;
						entry.STATUS = status.STATUS;
						entry.DELETION_STATUS = status.DELETION_STATUS ?? "";
						entry.GIP_STATUS = status.GIP_STATUS;
						entry.GIP_DESCRIPTION = status.GIP_DESCRIPTION;
						entry.TRAN_NO = status.TRAN_NO ?? "99999";

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


		//public static Boolean InsertAuditLog(AUDIT_LOG audit_Log)
		//{
		//    try
		//    {
		//        using (GUIDEDBEntities entities = new GUIDEDBEntities())
		//        {
		//            entities.AUDIT_LOG.Add(audit_Log);
		//            entities.SaveChanges();

		//            return true;
		//        }
		//    }
		//    catch (Exception ex)
		//    {
		//        // HTTP/1.1 400 Not found  
		//        return false;
		//    }
		//}

	}
}
