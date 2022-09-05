using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using mhcb.syd.DataAccess.View;
using mhcb.syd.DataAccess;
using System.Data.Entity;
using mhcb.syd.Interface;

namespace mhcb.syd.BusinessLayer
{
	public class FXTransactionBusiness : IFXTransaction
	{
		public IEnumerable<FXTransactionView> GetPendingTrans()
		{
			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				string[] validStatus = { "F", "A", "V" };
				DateTime date = DateTime.Now;
				DateTime defaultDatetime = DateTime.Parse("1900-01-01 00:00:00");

				var entry = (from b in entities.FX_TRANSACTION
							 where (!validStatus.Contains(b.STATUS)
										&& (!b.CUST_ABBR.Contains("NO GBASE"))
										&& (!b.CUST_ABBR.Contains("NODREAMINPU"))
										&& (!b.CUST_ABBR.Contains("999"))
									   //&& DbFunctions.TruncateTime(b.ADD_ON) == DbFunctions.TruncateTime(date)
									   )
							 select new FXTransactionView()
							 {
								 REF_NO = b.REF_NO,
								 CUST_ABBR = b.CUST_ABBR,

								 CONTRACT_DATE_OPE = (DbFunctions.TruncateTime(b.CONTRACT_DATE_OPE).ToString()).Substring(0, 10),
								 CONTRACT_DATE_VALUE = (DbFunctions.TruncateTime(b.CONTRACT_DATE_VALUE).ToString()).Substring(0, 10),
								 SETTLEMENT_VALUE_DATE = (DbFunctions.TruncateTime(b.SETTLEMENT_VALUE_DATE) == DbFunctions.TruncateTime(defaultDatetime)) ? "" : (DbFunctions.TruncateTime(b.SETTLEMENT_VALUE_DATE).ToString()).Substring(0, 10),

								 CONTRACT_CCY_ABBR = b.CONTRACT_CCY_ABBR,
								 CONTRACT_CCY_AMT = b.CONTRACT_CCY_AMT,
								 EQUIV_CCY_ABBR = b.EQUIV_CCY_ABBR,
								 EQUIV_CCY_AMT = b.EQUIV_CCY_AMT,

								 STATUS = b.STATUS,
								 DELETION_STATUS = b.DELETION_STATUS,
								 GIP_STATUS = b.GIP_STATUS,
								 GIP_DESCRIPTION = b.GIP_DESCRIPTION,
								 AUTHORIZE_BY = b.AUTHORIZE_BY,
								 TRAN_NO = b.TRAN_NO,
							 })
							.ToList();
				return entry;
			}
		}

		public IEnumerable<FXTransactionView> GetFXTransactionByDatesCustAbbr(string strDateFrom, string strDateTo, string dateType, string custName)
		{

			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				if (!(strDateFrom != null && strDateTo != null)) return new List<FXTransactionView>();

				string[] validStatus = { "F", "A", "V" };
				DateTime date = DateTime.Now;

				DateTime dateFrom = DateTime.Parse(strDateFrom);
				DateTime dateTo = DateTime.Parse(strDateTo);
				DateTime defaultDatetime = DateTime.Parse("1900-01-01 00:00:00");

				var entry = (from b in entities.FX_TRANSACTION
							 join cm in entities.CUSTOMER_MASTER
								on new { b.CUST_ABBR, b.BRANCH_NO } equals new { cm.CUST_ABBR, cm.BRANCH_NO }
							 where validStatus.Contains(b.STATUS)
								&& (cm.CUST_NAME.Contains(custName))
								&& (b.DELETION_STATUS == "")
								&&
								   (
									(dateType == "DelivaryDate" && DbFunctions.TruncateTime(b.DELIVARY_DATE) >= DbFunctions.TruncateTime(dateFrom) && DbFunctions.TruncateTime(b.DELIVARY_DATE) <= DbFunctions.TruncateTime(dateTo))
									||
									(dateType == "ContractValueDate" && DbFunctions.TruncateTime(b.CONTRACT_DATE_VALUE) >= DbFunctions.TruncateTime(dateFrom) && DbFunctions.TruncateTime(b.CONTRACT_DATE_VALUE) <= DbFunctions.TruncateTime(dateTo))
									)
							 select new FXTransactionView()
							 {
								 REF_NO = b.REF_NO,
								 CUST_ABBR = b.CUST_ABBR,
								 CUST_NAME = cm.CUST_NAME,
								 ACT_ABBR = b.ACT_ABBR,
								 ACT_CD = b.ACT_CD,

								 CONTRACT_DATE_OPE = (DbFunctions.TruncateTime(b.CONTRACT_DATE_OPE).ToString()).Substring(0, 10),
								 CONTRACT_DATE_VALUE = (DbFunctions.TruncateTime(b.CONTRACT_DATE_VALUE).ToString()).Substring(0, 10),
								 DELIVARY_DATE = (DbFunctions.TruncateTime(b.DELIVARY_DATE) == DbFunctions.TruncateTime(defaultDatetime)) ? "" : (DbFunctions.TruncateTime(b.DELIVARY_DATE).ToString()).Substring(0, 10),

								 CONTRACT_CCY_ABBR = b.CONTRACT_CCY_ABBR,
								 CONTRACT_CCY_AMT = b.CONTRACT_CCY_AMT,
								 EQUIV_CCY_ABBR = b.EQUIV_CCY_ABBR,
								 EQUIV_CCY_AMT = b.EQUIV_CCY_AMT,

								 EXCHANGE_RATE = b.EXCHANGE_RATE,
								 INTERNAL_RATE = b.INTERNAL_RATE,
								 SPREAD = b.SPREAD,

								 STATUS = b.STATUS,
								 DELETION_STATUS = b.DELETION_STATUS,
								 GIP_STATUS = b.GIP_STATUS,
								 GIP_DESCRIPTION = b.GIP_DESCRIPTION,
								 AUTHORIZE_BY = b.AUTHORIZE_BY,
								 TRAN_NO = b.TRAN_NO,
							 })
							.ToList();
				return entry;
			}
		}

		public IEnumerable<FxDeliveryView> getFxTransactioByDates(string strDateFrom, string strDateTo)
		{
			string connectionString = ConfigurationManager.ConnectionStrings["GUIDESP"].ConnectionString;
			List<FxDeliveryView> fxDeliveries = new List<FxDeliveryView>();

			if (!(strDateFrom != null && strDateTo != null)) return new List<FxDeliveryView>();

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand("USP_GUIDE_FX_Transaction_All", con);
				cmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramDateFrom = new SqlParameter
				{
					ParameterName = "@DateFrom",
					Value = DateTime.Parse(strDateFrom)
				};
				cmd.Parameters.Add(paramDateFrom);

				SqlParameter paramDateTo = new SqlParameter
				{
					ParameterName = "@DateTo",
					Value = DateTime.Parse(strDateTo)
				};
				cmd.Parameters.Add(paramDateTo);

				con.Open();
				SqlDataReader rdr = cmd.ExecuteReader();
				while (rdr.Read())
				{
					FxDeliveryView fxDelivery = new FxDeliveryView
					{
						REF_NO = rdr["REF_NO"].ToString(),
						CUST_ABBR = rdr["CUST_ABBR"].ToString(),
						CUST_NAME = rdr["CUST_NAME"].ToString(),
						BuyAmt = Convert.ToDecimal(rdr["BuyAmt"]),
						BuyCCY = rdr["BuyCCY"].ToString(),
						//REC_SETTL_CODE = rdr["REC_SETTL_CODE"].ToString(),
						//BUY_ACCT_TYPE = rdr["BUY_ACCT_TYPE"].ToString(),
						//REC_ACT_CD = rdr["REC_ACT_CD"].ToString(),
						//REC_ACT_NO = rdr["REC_ACT_NO"].ToString(),
						//REC_CUST_ABBR = rdr["REC_CUST_ABBR"].ToString(),
						SellAmt = Convert.ToDecimal(rdr["SellAmt"]),
						SellCCY = rdr["SellCCY"].ToString(),
						//PAY_SETTL_CODE = rdr["PAY_SETTL_CODE"].ToString(),
						//PAY_ACCT_TYPE = rdr["PAY_ACCT_TYPE"].ToString(),
						//PAY_ACT_CD = rdr["PAY_ACT_CD"].ToString(),
						//PAY_ACT_NO = rdr["PAY_ACT_NO"].ToString(),
						//PAY_CUST_ABBR = rdr["PAY_CUST_ABBR"].ToString()

						CONTRACT_DATE_OPE = (rdr["CONTRACT_DATE_OPE"] is DBNull) ? "" : (Convert.ToString(rdr["CONTRACT_DATE_OPE"])).Substring(0, 10),
						CONTRACT_DATE_VALUE = (rdr["CONTRACT_DATE_VALUE"] is DBNull) ? "" : (Convert.ToString(rdr["CONTRACT_DATE_VALUE"])).Substring(0, 10),
						DELIVARY_DATE = (rdr["DELIVARY_DATE"] is DBNull) ? "" : (Convert.ToString(rdr["DELIVARY_DATE"])).Substring(0, 10),
					};

					if (!(rdr["EXCHANGE_RATE"] is DBNull))
					{
						fxDelivery.EXCHANGE_RATE = Convert.ToDecimal(rdr["EXCHANGE_RATE"]);
					}

					fxDeliveries.Add(fxDelivery);
				}
			}
			return fxDeliveries;
		}

		//FXTransactionView
		public FX_TRANSACTION LoadFXTransactionByRefNo(string refNo)
		{
			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				var entry = entities.FX_TRANSACTION
							.Where(e => e.REF_NO.ToLower() == refNo.ToLower())
							.FirstOrDefault();
				return entry;
			}
		}

		public string updateStatus(FXTranStatus status)
		{
			try
			{
				using (GUIDEDBEntities entities = new GUIDEDBEntities())
				{
					string[] validStatus = { "F", "A", "V" };

					var entry = entities.FX_TRANSACTION
						//.Include(x => x.FX_SETTLEMENT_INF)
						//.Include(x => x.CUSTOMER_MASTER)
						.Where(e => e.REF_NO.ToLower() == status.REF_NO.ToLower()
							   && e.AUTHORIZE_BY == status.AUTHORIZE_BY
							   && !validStatus.Contains(e.STATUS))
						.FirstOrDefault();
					//.FirstOrDefault(e => e.REF_NO == status.REF_NO);

					if (entry != null)
					{
						var delstatus = entry.DELETION_STATUS.ToString();
						var guideStatus = entry.STATUS.ToString();

						AUDIT_LOG auditLog = new AUDIT_LOG
						{
							USER_ID = status.AUTHORIZE_BY,
							AUDIT_DATE = DateTime.Now,
							REF_NO = status.REF_NO.ToString(),
							TRANS_NO = status.TRAN_NO ?? "99999",
							AUDIT_DESC = "Sent FX Transaction to finalised",
							APP_NAME = "GUIDE",
							TABLE_NAME = "FX Transaction",
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
	}
}
