using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using mhcb.syd.DataAccess.View;
using mhcb.syd.Interface;

namespace mhcb.syd.BusinessLayer
{
	public class RFRInterest : IRFRInterest
	{
		public IEnumerable<RFRInterestLoanView> getRFRInterestForLoanByReportDate(string strReportDate, string custAbbr)
		{
			string connectionString = ConfigurationManager.ConnectionStrings["GUIDESP"].ConnectionString;
			List<RFRInterestLoanView> RfrInterestLoan = new List<RFRInterestLoanView>();

			if (strReportDate == null) return RfrInterestLoan;
			custAbbr = custAbbr ?? "";

			DateTime defaultDatetime = DateTime.Parse("1900-01-01 00:00:00");

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand("USP_GUIDE_RFR_Interest_Loan", con);
				cmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramDeliveryDate = new SqlParameter
				{
					ParameterName = "@ReportDate",
					Value = DateTime.Parse(strReportDate)
				};
				cmd.Parameters.Add(paramDeliveryDate);

				SqlParameter paramCustAbbr = new SqlParameter
				{
					ParameterName = "@CustAbbr",
					Value = custAbbr
				};
				cmd.Parameters.Add(paramCustAbbr);

				con.Open();
				SqlDataReader rdr = cmd.ExecuteReader();
				while (rdr.Read())
				{
					RFRInterestLoanView RfrInterest = new RFRInterestLoanView();

					RfrInterest.DATA_DATE = rdr["DATA_DATE"].ToString().Substring(0, 10);
					RfrInterest.RO_VALUE = rdr["RO_VALUE"].ToString().Substring(0, 10);

					RfrInterest.MATURITY_DATE = rdr["MATURITY_DATE"].ToString().Substring(0, 10);
					//RfrInterest.RO_DUE = rdr["RO_DUE"].ToString().Substring(0, 10);
					//RfrInterest.FINAL_DUE = rdr["FINAL_DUE"].ToString().Substring(0, 10);

					RfrInterest.REF_NO = rdr["REF_NO"].ToString();

					RfrInterest.DD_NO = rdr["DD_NO"].ToString();
					RfrInterest.CUST_ABBR = rdr["CUST_ABBR"].ToString();
					RfrInterest.CUST_NAME = rdr["CUST_NAME"].ToString();
					RfrInterest.AccountTypeName = rdr["AccountTypeName"].ToString();
					RfrInterest.DD_CCY = rdr["DD_CCY"].ToString();
					RfrInterest.DD_AMT = rdr["DD_AMT"].ToString();

					RfrInterest.INT_AMOUNT_RO = rdr["INT_AMOUNT_RO"].ToString();
					RfrInterest.INT_RATE_RO = rdr["INT_RATE_RO"].ToString();

					RfrInterest.IntAccrual_RFR = rdr["IntAccrual_RFR"].ToString();
					RfrInterest.IntRate_RFR = rdr["IntRate_RFR"].ToString();

					RfrInterest.IntAccrualCreatedDate = (rdr["IntAccrualCreatedDate"].ToString() == defaultDatetime.ToString()) ? "" : rdr["IntAccrualCreatedDate"].ToString().Substring(0, 10);

					RfrInterestLoan.Add(RfrInterest);
				}
			}
			return RfrInterestLoan;
		}


		public IEnumerable<RFRInterestSwapView> getRFRInterestForSwapByReportDate(string strReportDate, string custAbbr)
		{
			string connectionString = ConfigurationManager.ConnectionStrings["GUIDESP"].ConnectionString;
			List<RFRInterestSwapView> RfrInterestLoan = new List<RFRInterestSwapView>();

			if (strReportDate == null) return RfrInterestLoan;
			custAbbr = custAbbr ?? "";

			DateTime defaultDatetime = DateTime.Parse("1900-01-01 00:00:00");

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand("USP_GUIDE_RFR_Interest_Swap", con);
				cmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramDeliveryDate = new SqlParameter
				{
					ParameterName = "@ReportDate",
					Value = DateTime.Parse(strReportDate)
				};
				cmd.Parameters.Add(paramDeliveryDate);

				SqlParameter paramCustAbbr = new SqlParameter
				{
					ParameterName = "@CustAbbr",
					Value = custAbbr
				};
				cmd.Parameters.Add(paramCustAbbr);

				con.Open();
				SqlDataReader rdr = cmd.ExecuteReader();
				while (rdr.Read())
				{
					RFRInterestSwapView RfrInterest = new RFRInterestSwapView();

					RfrInterest.DATA_DATE = rdr["DATA_DATE"].ToString().Substring(0, 10);

					RfrInterest.REF_NO = rdr["REF_NO"].ToString();
					RfrInterest.CUST_ABBR = rdr["CUST_ABBR"].ToString();
					RfrInterest.CUST_NAME = rdr["CUST_NAME"].ToString();

					RfrInterest.START_VALUE = rdr["START_VALUE"].ToString().Substring(0, 10);
					RfrInterest.CONTRACT_DATE_VALUE = rdr["CONTRACT_DATE_VALUE"].ToString().Substring(0, 10);
					RfrInterest.MATURITY = rdr["MATURITY"].ToString().Substring(0, 10);

					RfrInterest.CCY_ABBR = rdr["CCY_ABBR"].ToString();
					RfrInterest.BALANCE = rdr["BALANCE"].ToString();
					//RfrInterest.PRINCIPAL_AMOUNT = rdr["PRINCIPAL_AMOUNT"].ToString();
					//RfrInterest.SPREAD = rdr["SPREAD"].ToString();
					//RfrInterest.INT_RATE = rdr["INT_RATE"].ToString();

					RfrInterest.INT_ACCRUAL = rdr["INT_ACCRUAL"].ToString();

					RfrInterest.PAY_REV = rdr["PAY_REV"].ToString();
					RfrInterest.FIXED_FLOAT = rdr["FIXED_FLOAT"].ToString();

					//RfrInterest.IntAccrual_RFR = Convert.ToDecimal(rdr["IntAccrual_RFR"]).ToString("#,##0.00");
					RfrInterest.IntAccrual_RFR = rdr["IntAccrual_RFR"].ToString();
					RfrInterest.IntRate_RFR = rdr["IntRate_RFR"].ToString();
					RfrInterest.IntAccrualCreatedDate = (rdr["IntAccrualCreatedDate"].ToString() == defaultDatetime.ToString()) ? "" : rdr["IntAccrualCreatedDate"].ToString().Substring(0, 10);

					RfrInterestLoan.Add(RfrInterest);
				}
			}
			return RfrInterestLoan;
		}
	}
}
