using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using mhcb.syd.DataAccess;
using mhcb.syd.DataAccess.View;
using mhcb.syd.Interface;

namespace mhcb.syd.BusinessLayer
{
	public class FXSettlementInfBusiness : IFXSettlementInf
	{
		public IEnumerable<FXDeliveryView> GetPendingTrans()
		{
			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				string[] validStatus = { "F", "A", "V" };
				DateTime defaultDatetime = DateTime.Parse("1900-01-01 00:00:00");

				var entry = (from b in entities.FX_SETTLEMENT_INF
							 where (!validStatus.Contains(b.STATUS)
									   && (!b.CUST_ABBR.Contains("NO GBASE"))
										 && (!b.CUST_ABBR.Contains("NODREAMINPU"))
										  && (!b.CUST_ABBR.Contains("999"))
									   //&& DbFunctions.TruncateTime(b.ADD_ON) == DbFunctions.TruncateTime(date)
									   )
							 select new FXDeliveryView()
							 {
								 REF_NO = b.REF_NO,
								 CUST_ABBR = b.CUST_ABBR,
								 REC_SETTL_CODE = b.REC_SETTL_CODE,
								 REC_CUST_ABBR = b.REC_CUST_ABBR,
								 REC_CCY_CD = b.REC_CCY_CD,
								 PAY_SETTL_CODE = b.PAY_SETTL_CODE,
								 PAY_CUST_ABBR = b.PAY_CUST_ABBR,
								 PAY_CCY_CD = b.PAY_CCY_CD,

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

		public IEnumerable<FxDeliveryView> getfxDeliveryCorpByDate(string strDeliveryDate)
		{
			string connectionString = ConfigurationManager.ConnectionStrings["GUIDESP"].ConnectionString;
			List<FxDeliveryView> fxDeliveries = new List<FxDeliveryView>();

			if (strDeliveryDate == null) return fxDeliveries;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand("USP_GUIDE_TODAY_FX_DELIVERY_CORPORATE", con);
				cmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramDeliveryDate = new SqlParameter();
				paramDeliveryDate.ParameterName = "@Date";
				paramDeliveryDate.Value = DateTime.Parse(strDeliveryDate);
				cmd.Parameters.Add(paramDeliveryDate);

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
						REC_SETTL_CODE = rdr["REC_SETTL_CODE"].ToString(),
						BUY_ACCT_TYPE = rdr["BUY_ACCT_TYPE"].ToString(),
						REC_ACT_CD = rdr["REC_ACT_CD"].ToString(),
						REC_ACT_NO = rdr["REC_ACT_NO"].ToString(),
						REC_CUST_ABBR = rdr["REC_CUST_ABBR"].ToString(),
						SellAmt = Convert.ToDecimal(rdr["SellAmt"]),
						SellCCY = rdr["SellCCY"].ToString(),
						PAY_SETTL_CODE = rdr["PAY_SETTL_CODE"].ToString(),
						PAY_ACCT_TYPE = rdr["PAY_ACCT_TYPE"].ToString(),
						PAY_ACT_CD = rdr["PAY_ACT_CD"].ToString(),
						PAY_ACT_NO = rdr["PAY_ACT_NO"].ToString(),
						PAY_CUST_ABBR = rdr["PAY_CUST_ABBR"].ToString(),
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

		public IEnumerable<FxDeliveryCorpSummaryView> getfxDeliveryCorpSummaryByDate(string strDeliveryDate)
		{
			string connectionString = ConfigurationManager.ConnectionStrings["GUIDESP"].ConnectionString;
			List<FxDeliveryCorpSummaryView> fxDeliveries = new List<FxDeliveryCorpSummaryView>();

			if (strDeliveryDate == null) return fxDeliveries;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand("USP_GUIDE_TODAY_FX_DELIVERY_CORPORATE_SUM", con);
				cmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramDeliveryDate = new SqlParameter();
				paramDeliveryDate.ParameterName = "@Date";
				paramDeliveryDate.Value = DateTime.Parse(strDeliveryDate);
				cmd.Parameters.Add(paramDeliveryDate);

				con.Open();
				SqlDataReader rdr = cmd.ExecuteReader();
				while (rdr.Read())
				{
					FxDeliveryCorpSummaryView fxDelivery = new FxDeliveryCorpSummaryView();

					fxDelivery.CCY = rdr["CCY"].ToString();
					fxDelivery.Acct_Type = rdr["Acct_Type"].ToString();
					fxDelivery.totalNet = Convert.ToDecimal(rdr["totalNet"]);

					fxDeliveries.Add(fxDelivery);
				}
			}
			return fxDeliveries;
		}

		public FX_SETTLEMENT_INF LoadFXSettlementByRefNo(string refNo)
		{
			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				var entry = entities.FX_SETTLEMENT_INF
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

					var entry = entities.FX_SETTLEMENT_INF
						 //.Include(x => x.FX_TRANSACTION)
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
							AUDIT_DESC = "Sent FX Settlement to finalised",
							APP_NAME = "GUIDE",
							TABLE_NAME = "FX Settlement",
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
