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
	public class CovenantMasterRecord : ICovenantMasterRecord
	{
		//SELECT
		//distinct 
		//--cm.*
		//cm.COVENANT_NO
		//, cr.RECORD_NO
		//, cm.CUST_ABBR
		//, lm.CCIF_NAME as Customer
		//--, ct.COVENANT_TYPE
		//, ct.COVENANT_DECS As COVENANT_TYPE
		//, cg.CONDITION_GROUP_DESC as ConditionGroup
		//, cc.CONDITION_DESC as Ratio
		//,cr.RATE_ADJ, DATE_RECIEVED_ADJ
		//, cr.REPORT_DATE, cr.DUE_DATE, cr.DATE_RECIEVED, cr.RATE
		//FROM [dbo].[COVENANT_MASTER] cm
		//inner join[dbo].[COVENANT_RECORD] cr on cm.COVENANT_NO = cr.COVENANT_NO
		//inner join [dbo].[COVENANT_TYPE] ct on cm.COV_TYPE = ct.COVENANT_TYPE
		//inner join [dbo].[COVENANT_CONDITION] cc on cm.CONDITION_TYPE = cc.CONDITION_TYPE
		//left join [dbo].[COVENANT_CONDITION_GROUP] cg on cg.CONDITION_GROUP_TYPE = cc.CONDITION_GROUP
		//inner join [dbo].[LOAN_MASTER_COVENANT] lmc on cm.COVENANT_NO = lmc.COVENANT_NO
		//Inner join [dbo].[LOAN_MASTER] lm on lmc.LOAN_NO = lm.LOAN_NO
		//where ct.COVENANT_DECS = 'Covenant'
		//and (lm.STATUS <> 'T' )
		//and (cm.STATUS = 'L')
		//order by  cm.CUST_ABBR, cr.RECORD_NO

		public IEnumerable<CovenantRatioView> GetCovenantRatio(string covenantType)
		{
			using (LoanDBEntities entities = new LoanDBEntities())
			{
				DateTime defaultDatetime = DateTime.Parse("1900-01-01 00:00:00");

				var entry = (from cm in entities.COVENANT_MASTER
							 join cr in entities.COVENANT_RECORD on cm.COVENANT_NO equals cr.COVENANT_NO
							 join ct in entities.COVENANT_TYPE on cm.COV_TYPE equals ct.COVENANT_TYPE1
							 join cc in entities.COVENANT_CONDITION on cm.CONDITION_TYPE equals cc.CONDITION_TYPE
							 join cg in entities.COVENANT_CONDITION_GROUP on cc.CONDITION_GROUP equals cg.CONDITION_GROUP_TYPE
							 join lmc in entities.LOAN_MASTER_COVENANT on cm.COVENANT_NO equals lmc.COVENANT_NO
							 join lm in entities.LOAN_MASTER on lmc.LOAN_NO equals lm.LOAN_NO

							 where ct.COVENANT_DECS == covenantType  /*"Covenant"*/
							  && (lm.STATUS != "T")
							  && (cm.STATUS == "L")
							 orderby cm.CUST_ABBR, cr.RECORD_NO
							 select new CovenantRatioView()
							 {
								 COVENANT_NO = cm.COVENANT_NO,
								 CUST_ABBR = cm.CUST_ABBR,
								 CCIF_NAME = lm.CCIF_NAME,
								 COVENANT_DECS = ct.COVENANT_DECS,

								 CONDITION_GROUP_DESC = cg.CONDITION_GROUP_DESC,
								 CONDITION_DESC = cc.CONDITION_DESC,

								 RECORD_NO = cr.RECORD_NO,

								 DATE_RECIEVED_ADJ = (DbFunctions.TruncateTime(cr.DATE_RECIEVED_ADJ) == DbFunctions.TruncateTime(defaultDatetime)) ? "" : (DbFunctions.TruncateTime(cr.DATE_RECIEVED_ADJ).ToString()).Substring(0, 10),

								 DATE_RECIEVED = (DbFunctions.TruncateTime(cr.DATE_RECIEVED) == DbFunctions.TruncateTime(defaultDatetime)) ? "" : (DbFunctions.TruncateTime(cr.DATE_RECIEVED).ToString()).Substring(0, 10),

								 REPORT_DATE = (DbFunctions.TruncateTime(cr.REPORT_DATE) == DbFunctions.TruncateTime(defaultDatetime)) ? "" : (DbFunctions.TruncateTime(cr.REPORT_DATE).ToString()).Substring(0, 10),

								 DUE_DATE = (DbFunctions.TruncateTime(cr.DUE_DATE) == DbFunctions.TruncateTime(defaultDatetime)) ? "" : (DbFunctions.TruncateTime(cr.DUE_DATE).ToString()).Substring(0, 10),

								 RATE_ADJ = (cr.RATE_ADJ == (decimal)(-9999999999.99)) ? "" : cr.RATE_ADJ.ToString(),
								 RATE = (cr.RATE == (decimal)(-9999999999.99)) ? "" : cr.RATE.ToString(),
							 })
							.ToList();
				return entry;
			}
		}


		// *************************************************
		//SELECT
		//  distinct
		//  lm.CUST_ABBR,
		//	cust.CUST_NAME,

		//	cm.COVENANT_NO,
		//	cm.FREQ_TYPE,
		//	cm.MATH_SYMBOL,
		//	cm.VALUE,
		//	cm.DENOMINATION,
		//	cm.DESCRIPTION,

		//	ct.COVENANT_DECS,

		//	cf.FREQ_DESC,
		//	rt.TIMING_DESC,
		//	cc.CONDITION_DESC,

		//	cr.REPORT_DATE,
		//	cr.DUE_DATE,
		//	cr.RATE,
		//	cr.DATE_RECIEVED,
		//	cr.RATE_ADJ,
		//	cr.DATE_RECIEVED_ADJ,
		//	cr.STRESS_VALUE

		//FROM dbo.COVENANT_RECORD cr
		//INNER JOIN dbo.COVENANT_MASTER cm on cr.COVENANT_NO = cm.COVENANT_NO
		//INNER JOIN dbo.COVENANT_TYPE ct on cm.COV_TYPE = ct.COVENANT_TYPE
		//INNER JOIN dbo.COVENANT_RECEIPT_TIMING rt on cm.TIME_OF_RECEIPT = rt.TIMING_TYPE
		//INNER JOIN dbo.LOAN_MASTER_COVENANT lc on cr.COVENANT_NO = lc.COVENANT_NO
		//LEFT OUTER JOIN dbo.COVENANT_CONDITION cc on cc.CONDITION_TYPE = cr.CONDITION_TYPE
		//LEFT OUTER JOIN dbo.COVENANT_FREQ_TYPE cf on cf.FREQ_TYPE = cm.REPORT_TYPE
		//INNER JOIN LOAN_MASTER lm on lm.CUST_ABBR = cm.CUST_ABBR
		//INNER JOIN Guide.dbo.CUSTOMER_MASTER cust on cust.CUST_ABBR = lm.CUST_ABBR
		//WHERE
		//cr.STATUS<>'T' 
		//AND cm.STATUS<>'T' 
		//AND cm.COV_TYPE IN ('COV', 'REP')
		//AND lm.STATUS ='L'

		//--AND
		//--WHERE LC.LOAN_NO= @LOAN_NO AND cr.STATUS<>'F' AND cm.STATUS<>'F' AND cm.COV_TYPE IN ('COV', 'REP') AND 
		//--LC.LOAN_NO= @LOAN_NO AND
		//--DATEDIFF(day, @DATEFROM, cr.REPORT_DATE) >= 0  --AND
		//--DATEDIFF(day, @DATETO, cr.DUE_DATE)  <= 0

		//ORDER BY
		//--REPORT_DATE, cc.CONDITION_GROUP, cm.CONDITION_TYPE, cm.DESCRIPTION, cm.REPORT_TYPE asc
		//cust.CUST_NAME, cm.COVENANT_NO, cr.REPORT_DATE
		public IEnumerable<CovenantMasterRecordView> GetCovenantMasterRecord()
		{

			using (LoanDBEntities entities = new LoanDBEntities())
			{
				using (GUIDEDBEntities entities2 = new GUIDEDBEntities())
				{
					string[] covenantType = { "COV", "REP" };
					DateTime defaultDatetime = DateTime.Parse("1900-01-01 00:00:00");

					var entry = (from cr in entities.COVENANT_RECORD
								 join cm in entities.COVENANT_MASTER on cr.COVENANT_NO equals cm.COVENANT_NO
								 join ct in entities.COVENANT_TYPE on cm.COV_TYPE equals ct.COVENANT_TYPE1
								 join crt in entities.COVENANT_RECEIPT_TIMING on cm.TIME_OF_RECEIPT equals crt.TIMING_TYPE
								 join lmc in entities.LOAN_MASTER_COVENANT on cr.COVENANT_NO equals lmc.COVENANT_NO

								 join cc in entities.COVENANT_CONDITION on cm.CONDITION_TYPE equals cc.CONDITION_TYPE
								 join cf in entities.COVENANT_FREQ_TYPE on cm.REPORT_TYPE equals cf.FREQ_TYPE

								 join lm in entities.LOAN_MASTER on lmc.LOAN_NO equals lm.LOAN_NO

								 //join cm in entities.vGUIDE_CUSTOMER_MASTER
								 //  on new { fm.CUST_ABBR, fm.BRANCH_NO } equals new { cm.CUST_ABBR, cm.BRANCH_NO }

								 //join cg in entities.COVENANT_CONDITION_GROUP on cg.CONDITION_GROUP equals cg.CONDITION_GROUP_TYPE                            
								 join cust in entities2.CUSTOMER_MASTER //on lm.CUST_ABBR equals cust.CUST_ABBR
									on new { lm.CUST_ABBR, lm.BRANCH_NO } equals new { cust.CUST_ABBR, cust.BRANCH_NO }

								 where ct.COVENANT_DECS == "Covenant"
											 && covenantType.Contains(cm.COV_TYPE)
											 && cr.STATUS != "T"
											 && cm.STATUS != "T"
											 && lm.STATUS == "L"
								 orderby cm.COVENANT_NO, cr.REPORT_DATE
								 select new CovenantMasterRecordView()
								 {
									 CUST_NAME = cust.CUST_NAME,
									 CUST_ABBR = cm.CUST_ABBR,  // lm.CUST_ABBR,
									 CCIF_NAME = lm.CCIF_NAME,

									 COVENANT_NO = cm.COVENANT_NO,
									 FREQ_TYPE = cm.FREQ_TYPE,
									 MATH_SYMBOL = cm.MATH_SYMBOL,
									 VALUE = cm.VALUE,
									 DENOMINATION = cm.DENOMINATION,
									 DESCRIPTION = cm.DESCRIPTION,

									 COVENANT_DECS = ct.COVENANT_DECS,
									 FREQ_DESC = cf.FREQ_DESC,
									 TIMING_DESC = crt.TIMING_DESC,
									 CONDITION_DESC = cc.CONDITION_DESC,

									 //CONDITION_GROUP_DESC = cg.CONDITION_GROUP_DESC,
									 //CONDITION_DESC = cg.CONDITION_DESC,

									 RECORD_NO = cr.RECORD_NO,
									 DATE_RECIEVED = (DbFunctions.TruncateTime(cr.DATE_RECIEVED) == DbFunctions.TruncateTime(defaultDatetime)) ? "" : (DbFunctions.TruncateTime(cr.DATE_RECIEVED).ToString()).Substring(0, 10),
									 REPORT_DATE = (DbFunctions.TruncateTime(cr.REPORT_DATE) == DbFunctions.TruncateTime(defaultDatetime)) ? "" : (DbFunctions.TruncateTime(cr.REPORT_DATE).ToString()).Substring(0, 10),
									 DUE_DATE = (DbFunctions.TruncateTime(cr.DUE_DATE) == DbFunctions.TruncateTime(defaultDatetime)) ? "" : (DbFunctions.TruncateTime(cr.DUE_DATE).ToString()).Substring(0, 10),
									 RATE = cr.RATE,
									 RATE_ADJ = cr.RATE_ADJ,
									 DATE_RECIEVED_ADJ = (DbFunctions.TruncateTime(cr.DATE_RECIEVED_ADJ) == DbFunctions.TruncateTime(defaultDatetime)) ? "" : (DbFunctions.TruncateTime(cr.DATE_RECIEVED_ADJ).ToString()).Substring(0, 10),
									 STRESS_VALUE = cr.STRESS_VALUE

								 })
								.ToList();
					return entry;
				}
			}
		}
	}

}