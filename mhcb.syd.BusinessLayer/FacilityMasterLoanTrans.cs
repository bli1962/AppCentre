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
	public class FacilityMasterLoanTrans : IFacilityMasterLoanTrans
	{
		public IEnumerable<FacilityMasterLoanTransView> getLoanTransactionByDates(string strDateFrom, string strDateTo)
		{
			if (!(strDateFrom != null && strDateTo != null)) return new List<FacilityMasterLoanTransView>();

			using (LoanDBEntities entities = new LoanDBEntities())
			{
				using (GUIDEDBEntities entities2 = new GUIDEDBEntities())
				{
					DateTime dateFrom = DateTime.Parse(strDateFrom);
					DateTime dateTo = DateTime.Parse(strDateTo);
					DateTime defaultDatetime = DateTime.Parse("1900-01-01 00:00:00");

					var entry = (from lt in entities.LOAN_TRANS
								 join fm in entities.FACILITY_MASTER on lt.REF_NO equals fm.REF_NO
								 //join p1 in entities.PROD_TYPE on f.PROD_TYPE1 equals p1.PROD_ID
								 join p2 in entities.PROD_TYPE on fm.PROD_TYPE2 equals p2.PROD_ID
								 join p3 in entities.PROD_TYPE on fm.PROD_TYPE3 equals p3.PROD_ID
								 //join p4 in entities.PROD_TYPE on f.PROD_TYPE4 equals p4.PROD_ID

								 join cv in entities.vGUIDE_CUSTOMER_MASTER
									on new { fm.CUST_ABBR, fm.BRANCH_NO } equals new { cv.CUST_ABBR, cv.BRANCH_NO }

									// not working. why?
									//join cust in entities2.CUSTOMER_MASTER on f.CUST_ABBR equals cust.CUST_ABBR 
								 where lt.STATUS != "D"
									   && DbFunctions.TruncateTime(lt.START_DATE) >= DbFunctions.TruncateTime(dateFrom)
									   && DbFunctions.TruncateTime(lt.START_DATE) <= DbFunctions.TruncateTime(dateTo)
								 orderby lt.REF_NO, lt.START_DATE, lt.TRANS_NO
								 select new FacilityMasterLoanTransView()
								 {
									 DEPARTMENT =
												(
													cv.DEPARTMENT_CD == "20" ? "JCD" :
													cv.DEPARTMENT_CD == "30" ? "CLD" :
													cv.DEPARTMENT_CD == "31" ? "PFD" :
													cv.DEPARTMENT_CD == "32" ? "LBO" : ""
												),
									 CUST_NAME = cv.CUST_NAME,

									 REF_NO = lt.REF_NO,

									 START_DATE = (DbFunctions.TruncateTime(lt.START_DATE) == DbFunctions.TruncateTime(defaultDatetime)) ? "" : (DbFunctions.TruncateTime(lt.START_DATE).ToString()).Substring(0, 10),

									 DUE_DATE = (DbFunctions.TruncateTime(lt.DUE_DATE) == DbFunctions.TruncateTime(defaultDatetime)) ? "" : (DbFunctions.TruncateTime(lt.DUE_DATE).ToString()).Substring(0, 10),

									 DRAW_CCY = lt.DRAW_CCY,
									 DRAW_AMT = lt.DRAW_AMT,
									 REPAY_CCY = lt.REPAY_CCY,
									 REPAY_AMT = lt.REPAY_AMT,
									 INT_RATE = lt.INT_RATE,
									 INTERNAL_RATE = lt.INTERNAL_RATE ?? "",

									 SPREAD = lt.SPREAD,
									 INT_AMT = lt.INT_AMT,
									 REMARKS = lt.REMARKS,

									 TransType = (
												lt.DRAW_AMT > 0 && lt.RO_NO == 0 ? "Drawdown" :
												lt.DRAW_AMT > 0 && lt.RO_NO > 0 ? "Rollover" :
												lt.REPAY_AMT > 0 ? "Repayment" : ""
											),
									 PROD_TYPE2_DESC = p2.PROD_DESC,
									 PROD_TYPE3_DESC = p3.PROD_DESC


								 })
								.ToList();
					return entry;
				}
			}
		}

		public IEnumerable<FacilityMasterLoanTransView> getLoanTransactionByCcyDates(string strDateFrom, string strDateTo, string ccy)
		{
			if (!(strDateFrom != null && strDateTo != null)) return new List<FacilityMasterLoanTransView>();

			using (LoanDBEntities entities = new LoanDBEntities())
			{
				using (GUIDEDBEntities entities2 = new GUIDEDBEntities())
				{
					DateTime dateFrom = DateTime.Parse(strDateFrom);
					DateTime dateTo = DateTime.Parse(strDateTo);
					DateTime defaultDatetime = DateTime.Parse("1900-01-01 00:00:00");

					var entry = (from lt in entities.LOAN_TRANS
								 join fm in entities.FACILITY_MASTER on lt.REF_NO equals fm.REF_NO
								 join st in entities.STATUS_CODE on lt.STATUS equals st.STATUS_CODE1

								 join cv in entities.vGUIDE_CUSTOMER_MASTER
									  on new { fm.CUST_ABBR, fm.BRANCH_NO } equals new { cv.CUST_ABBR, cv.BRANCH_NO }
									  // not working. why?
									  //join cv in entities2.CUSTOMER_MASTER on fm.CUST_ABBR equals cv.CUST_ABBR
								 where lt.STATUS != "F"
									   && lt.DRAW_CCY == ccy
									   && DbFunctions.TruncateTime(lt.START_DATE) >= DbFunctions.TruncateTime(dateFrom)
									   && DbFunctions.TruncateTime(lt.START_DATE) <= DbFunctions.TruncateTime(dateTo)
								 orderby lt.REF_NO, lt.START_DATE, lt.TRANS_NO
								 select new FacilityMasterLoanTransView()
								 {
									 REF_NO = lt.REF_NO,
									 TRANS_NO = lt.TRANS_NO,
									 CUST_NAME = cv.CUST_NAME,

									 START_DATE = (DbFunctions.TruncateTime(lt.START_DATE) == DbFunctions.TruncateTime(defaultDatetime)) ? "" : (DbFunctions.TruncateTime(lt.START_DATE).ToString()).Substring(0, 10),

									 DUE_DATE = (DbFunctions.TruncateTime(lt.DUE_DATE) == DbFunctions.TruncateTime(defaultDatetime)) ? "" : (DbFunctions.TruncateTime(lt.DUE_DATE).ToString()).Substring(0, 10),

									 DAYS = lt.DAYS,
									 DRAW_CCY = lt.DRAW_CCY,
									 DRAW_AMT = lt.DRAW_AMT,
									 REPAY_AMT = lt.REPAY_AMT,
									 REPAY_CCY = lt.REPAY_CCY,

									 INT_RATE = lt.INT_RATE,
									 INTERNAL_RATE = lt.INTERNAL_RATE ?? "",
									 STATUS = st.STATUS_DESC,

									 SPREAD = lt.SPREAD,
									 INT_AMT = lt.INT_AMT,
									 REMARKS = lt.REMARKS,
								 })
								.ToList();
					return entry;
				}
			}
		}
	}
}
