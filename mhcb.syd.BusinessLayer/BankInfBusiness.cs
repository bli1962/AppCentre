using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using mhcb.syd.DataAccess.View;
using mhcb.syd.DataAccess;
using mhcb.syd.Interface;

namespace mhcb.syd.BusinessLayer
{
	public class BankInfBusiness : IBankInf
	{
		public IEnumerable<BankInfView> GetPendingTrans()
		{
			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				string[] validStatus = { "F", "A", "M", "V" };
				var entry = (from b in entities.BANK_INF
							 join c in entities.BANK_INF_CORR on b.SWIFT_ID equals c.SWIFT_ID into c1
							 from bankInfcorr in c1.DefaultIfEmpty()
							 join y in entities.FXFCcies on bankInfcorr.CCY_CD equals y.CCY_CD into y1
							 from fxccy in y1.DefaultIfEmpty()
							 where (!validStatus.Contains(b.STATUS))
							 select new BankInfView()
							 {
								 SWIFT_ID = b.SWIFT_ID,
								 BRANCH_NO = b.BRANCH_NO,
								 SETTL_BRANCH_NO = b.SETTL_BRANCH_NO,
								 ACRONYM = b.ACRONYM,
								 NAME_ADDR_1 = b.NAME_ADDR_1,
								 NAME_ADDR_2 = b.NAME_ADDR_2,
								 NAME_ADDR_3 = b.NAME_ADDR_3,
								 NAME_ADDR_4 = b.NAME_ADDR_4,
								 LOCATION_COUNTRY = b.LOCATION_COUNTRY,
								 SWIFT_FLG = b.SWIFT_FLG,
								 CHIPS_UID = b.CHIPS_UID,
								 COR_BANK_CD = b.COR_BANK_CD,
								 STATUS = b.STATUS,
								 DELETION_STATUS = b.DELETION_STATUS,
								 GIP_STATUS = b.GIP_STATUS,
								 GIP_DESCRIPTION = b.GIP_DESCRIPTION,
								 AUTHORIZE_BY = b.AUTHORIZE_BY,
								 TRAN_NO = b.TRAN_NO,

								 MM_SETTL_BANK_ID = bankInfcorr.MM_SETTL_BANK_ID ?? String.Empty,
								 CCY = fxccy.CCY ?? String.Empty
							 })
							.ToList();
				return entry;
			}
		}

		public IEnumerable<BankInfView> LoadBankInfByDetails(string custName, string branchNo)
		{
			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				var entry = (from b in entities.BANK_INF
							 join c in entities.BANK_INF_CORR on b.SWIFT_ID equals c.SWIFT_ID into c1
							 from bankInfcorr in c1.DefaultIfEmpty()
							 join y in entities.FXFCcies on bankInfcorr.CCY_CD equals y.CCY_CD into y1
							 from fxccy in y1.DefaultIfEmpty()
							 where b.NAME_ADDR_1.Contains(custName)
								   && b.BRANCH_NO == branchNo
								   && (!String.IsNullOrEmpty(b.SWIFT_ID))
							 select new BankInfView()
							 {
								 SWIFT_ID = b.SWIFT_ID,
								 BRANCH_NO = b.BRANCH_NO,
								 SETTL_BRANCH_NO = b.SETTL_BRANCH_NO,
								 ACRONYM = b.ACRONYM,
								 NAME_ADDR_1 = b.NAME_ADDR_1,
								 NAME_ADDR_2 = b.NAME_ADDR_2,
								 NAME_ADDR_3 = b.NAME_ADDR_3,
								 NAME_ADDR_4 = b.NAME_ADDR_4,
								 LOCATION_COUNTRY = b.LOCATION_COUNTRY,
								 SWIFT_FLG = b.SWIFT_FLG,
								 CHIPS_UID = b.CHIPS_UID,
								 COR_BANK_CD = b.COR_BANK_CD,
								 STATUS = b.STATUS,
								 DELETION_STATUS = b.DELETION_STATUS,
								 GIP_STATUS = b.GIP_STATUS,
								 GIP_DESCRIPTION = b.GIP_DESCRIPTION,
								 AUTHORIZE_BY = b.AUTHORIZE_BY,
								 TRAN_NO = b.TRAN_NO,

								 MM_SETTL_BANK_ID = bankInfcorr.MM_SETTL_BANK_ID ?? String.Empty,
								 CCY = fxccy.CCY ?? String.Empty
							 })
							.ToList();
				return entry;
			}
		}

		public IEnumerable<BankInfView> LoadBankInfBySwiftID(string swiftId)
		{
			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				var entry = (from b in entities.BANK_INF
							 join c in entities.BANK_INF_CORR on b.SWIFT_ID equals c.SWIFT_ID into c1
							 from bankInfcorr in c1.DefaultIfEmpty()
							 join y in entities.FXFCcies on bankInfcorr.CCY_CD equals y.CCY_CD into y1
							 from fxccy in y1.DefaultIfEmpty()
							 where b.SWIFT_ID == swiftId && (!String.IsNullOrEmpty(b.SWIFT_ID))
							 select new BankInfView()
							 {
								 SWIFT_ID = b.SWIFT_ID,
								 BRANCH_NO = b.BRANCH_NO,
								 SETTL_BRANCH_NO = b.SETTL_BRANCH_NO,
								 ACRONYM = b.ACRONYM,
								 NAME_ADDR_1 = b.NAME_ADDR_1,
								 NAME_ADDR_2 = b.NAME_ADDR_2,
								 NAME_ADDR_3 = b.NAME_ADDR_3,
								 NAME_ADDR_4 = b.NAME_ADDR_4,
								 LOCATION_COUNTRY = b.LOCATION_COUNTRY,
								 SWIFT_FLG = b.SWIFT_FLG,
								 CHIPS_UID = b.CHIPS_UID,
								 COR_BANK_CD = b.COR_BANK_CD,
								 STATUS = b.STATUS,
								 DELETION_STATUS = b.DELETION_STATUS,
								 GIP_STATUS = b.GIP_STATUS,
								 GIP_DESCRIPTION = b.GIP_DESCRIPTION,
								 AUTHORIZE_BY = b.AUTHORIZE_BY,
								 TRAN_NO = b.TRAN_NO,

								 MM_SETTL_BANK_ID = bankInfcorr.MM_SETTL_BANK_ID ?? String.Empty,
								 CCY = fxccy.CCY ?? String.Empty
							 })
							.ToList();
				return entry;
			}
		}

		public IEnumerable<BankInfView> LoadBankInfsByStatus(string status = "All")
		{
			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				if (status != "D")
				{
					var entry = (from b in entities.BANK_INF
								 join c in entities.BANK_INF_CORR on b.SWIFT_ID equals c.SWIFT_ID into c1
								 from bankInfcorr in c1.DefaultIfEmpty()
								 join y in entities.FXFCcies on bankInfcorr.CCY_CD equals y.CCY_CD into y1
								 from fxccy in y1.DefaultIfEmpty()
								 where (b.STATUS == (status.ToUpper() == "ALL" ? b.STATUS : status)
										 && (b.DELETION_STATUS == "")
										 && !String.IsNullOrEmpty(b.SWIFT_ID))
								 select new BankInfView()
								 {
									 SWIFT_ID = b.SWIFT_ID,
									 BRANCH_NO = b.BRANCH_NO,
									 SETTL_BRANCH_NO = b.SETTL_BRANCH_NO,
									 ACRONYM = b.ACRONYM,
									 NAME_ADDR_1 = b.NAME_ADDR_1,
									 NAME_ADDR_2 = b.NAME_ADDR_2,
									 NAME_ADDR_3 = b.NAME_ADDR_3,
									 NAME_ADDR_4 = b.NAME_ADDR_4,
									 LOCATION_COUNTRY = b.LOCATION_COUNTRY,
									 SWIFT_FLG = b.SWIFT_FLG,
									 CHIPS_UID = b.CHIPS_UID,
									 COR_BANK_CD = b.COR_BANK_CD,
									 STATUS = b.STATUS,
									 DELETION_STATUS = b.DELETION_STATUS,
									 GIP_STATUS = b.GIP_STATUS,
									 GIP_DESCRIPTION = b.GIP_DESCRIPTION,
									 AUTHORIZE_BY = b.AUTHORIZE_BY,
									 TRAN_NO = b.TRAN_NO,

									 MM_SETTL_BANK_ID = bankInfcorr.MM_SETTL_BANK_ID ?? String.Empty,
									 CCY = fxccy.CCY ?? String.Empty
								 })
						   .ToList();
					return entry;
				}
				else
				{
					var entry = (from b in entities.BANK_INF
								 join c in entities.BANK_INF_CORR on b.SWIFT_ID equals c.SWIFT_ID into c1
								 from bankInfcorr in c1.DefaultIfEmpty()
								 join y in entities.FXFCcies on bankInfcorr.CCY_CD equals y.CCY_CD into y1
								 from fxccy in y1.DefaultIfEmpty()
								 where ((b.STATUS == "F")
								 && (b.DELETION_STATUS == status)
								 && !String.IsNullOrEmpty(b.SWIFT_ID))
								 select new BankInfView()
								 {
									 SWIFT_ID = b.SWIFT_ID,
									 BRANCH_NO = b.BRANCH_NO,
									 SETTL_BRANCH_NO = b.SETTL_BRANCH_NO,
									 ACRONYM = b.ACRONYM,
									 NAME_ADDR_1 = b.NAME_ADDR_1,
									 NAME_ADDR_2 = b.NAME_ADDR_2,
									 NAME_ADDR_3 = b.NAME_ADDR_3,
									 NAME_ADDR_4 = b.NAME_ADDR_4,
									 LOCATION_COUNTRY = b.LOCATION_COUNTRY,
									 SWIFT_FLG = b.SWIFT_FLG,
									 CHIPS_UID = b.CHIPS_UID,
									 COR_BANK_CD = b.COR_BANK_CD,
									 STATUS = b.STATUS,
									 DELETION_STATUS = b.DELETION_STATUS,
									 GIP_STATUS = b.GIP_STATUS,
									 GIP_DESCRIPTION = b.GIP_DESCRIPTION,
									 AUTHORIZE_BY = b.AUTHORIZE_BY,
									 TRAN_NO = b.TRAN_NO,

									 MM_SETTL_BANK_ID = bankInfcorr.MM_SETTL_BANK_ID ?? String.Empty,
									 CCY = fxccy.CCY ?? String.Empty
								 })
						   .ToList();
					return entry;
				}
			}
		}

		public string updateStatus(BankInfStatus status)
		{
			try
			{
				using (GUIDEDBEntities entities = new GUIDEDBEntities())
				{
					string[] validStatus = { "F", "A", "M", "V" };

					var entry = entities.BANK_INF
						 .Where(e => e.SWIFT_ID.ToLower() == status.SWIFT_ID.ToLower()
							   && e.AUTHORIZE_BY == status.AUTHORIZE_BY
							   && !validStatus.Contains(e.STATUS))
						.FirstOrDefault();


					if (entry != null)
					{
						var delstatus = entry.DELETION_STATUS.ToString();
						var guideStatus = entry.STATUS.ToString();

						AUDIT_LOG auditLog = new AUDIT_LOG
						{
							USER_ID = status.AUTHORIZE_BY,
							AUDIT_DATE = DateTime.Now,
							REF_NO = status.SWIFT_ID.ToString(),
							TRANS_NO = status.TRAN_NO ?? "99999",
							AUDIT_DESC = "Sent BIF to finalised",
							APP_NAME = "GUIDE",
							TABLE_NAME = "Bank Information",
							ORIGINAL_VALUE = guideStatus + " : " + delstatus,
							NEW_VALUE = status.STATUS + " : " + status.DELETION_STATUS,
						};

						entry.AUTHORIZE_BY = string.IsNullOrEmpty(status.AUTHORIZE_BY) ? entry.AUTHORIZE_BY : status.AUTHORIZE_BY;
						entry.STATUS = string.IsNullOrEmpty(status.STATUS) ? entry.STATUS : status.STATUS;
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
	}
}
