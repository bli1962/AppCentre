using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using mhcb.syd.DataAccess;
using mhcb.syd.DataAccess.View;
using mhcb.syd.Interface;

namespace mhcb.syd.BusinessLayer
{
	public class LoanMasterTrancheBusiness : ILoanMasterTranche
	{
		// *************************************************
		//Covenant_Checklist.sql
		//LoanMasterAndTrancheInfo.sql
		//Select DISTINCT
		//LM.LOAN_NO, 

		//LM.GCIF ,
		//LM.CCIF, 
		//LM.CCIF_NAME, 
		//LM.AGREEM_DATE1, 
		//LM.AGREEM_DATE2, 
		//LM.AMENDMENT_DATE,
		//LM.REMARKS,

		//TM.TRANCHE_NO, 
		//TM.TRANCHE_NAME,
		//CM.CUST_ABBR

		//From LOAN_MASTER LM
		//INNER JOIN Guide.dbo.CUSTOMER_MASTER CM ON LM.CUST_ABBR = CM.CUST_ABBR
		//LEFT JOIN TRANCHE_MASTER TM ON LM.LOAN_NO = TM.LOAN_NO

		public IEnumerable<LoanMasterTrancheView> GetCovenantCheckList()
		{
			using (LoanDBEntities entities = new LoanDBEntities())
			{
				using (GUIDEDBEntities entities2 = new GUIDEDBEntities())
				{
					var entry = (from lm in entities.LOAN_MASTER
								 join cm in entities2.CUSTOMER_MASTER on lm.CUST_ABBR equals cm.CUST_ABBR
								 join tm in entities.TRANCHE_MASTER on lm.LOAN_NO equals tm.LOAN_NO

								 select new LoanMasterTrancheView()
								 {
									 LOAN_NO = lm.LOAN_NO,
									 GCIF = lm.GCIF,
									 CCIF = lm.CCIF,
									 CCIF_NAME = lm.CCIF_NAME,
									 REMARKS = lm.REMARKS,
									 AGREEM_DATE1 = lm.AGREEM_DATE1,
									 AGREEM_DATE2 = lm.AGREEM_DATE2,
									 AMENDMENT_DATE = lm.AMENDMENT_DATE,
									 STATUS = lm.STATUS,

									 TRANCHE_NAME = tm.TRANCHE_NAME,
									 TRANCHE_NO = tm.TRANCHE_NO,
									 CUST_ABBR = cm.CUST_ABBR,
									 //STATUS_DESC = sc.SHORT_DESC
								 })
								.ToList();
					return entry;
				}
			}
		}


		// *************************************************
		//LiborDiscontinuation.sql
		//Select DISTINCT

		//LM.LOAN_NO, 
		//LM.GCIF,
		//LM.CCIF, 
		//LM.CCIF_NAME, 
		//LM.REMARKS,
		//LM.AGREEM_DATE1, 
		//LM.AGREEM_DATE2, 
		//LM.AMENDMENT_DATE,

		//TM.TRANCHE_NAME, 
		//TM.TRANCHE_NO, 
		//SC.SHORT_DESC as Status, 
		//CM.CUST_ABBR,
		//SC.SHORT_DESC

		//from LOAN_MASTER LM
		//INNER JOIN Guide.dbo.CUSTOMER_MASTER CM ON LM.CUST_ABBR = CM.CUST_ABBR
		//LEFT JOIN TRANCHE_MASTER TM ON LM.LOAN_NO = TM.LOAN_NO
		//INNER JOIN LOAN_MASTER_STATUS_CODE SC on LM.STATUS = SC.STATUS_CODE and 'LOAN_MASTER' = SC.MODULE
		public IEnumerable<LoanMasterTrancheView> GetLiborDiscontinuation()
		{
			using (LoanDBEntities entities = new LoanDBEntities())
			{
				using (GUIDEDBEntities entities2 = new GUIDEDBEntities())
				{
					var entry = (from lm in entities.LOAN_MASTER
								 join cm in entities2.CUSTOMER_MASTER on lm.CUST_ABBR equals cm.CUST_ABBR
								 join tm in entities.TRANCHE_MASTER on lm.LOAN_NO equals tm.LOAN_NO
								 join sc in entities.LOAN_MASTER_STATUS_CODE on lm.STATUS equals sc.STATUS_CODE
								 where sc.MODULE == "LOAN_MASTER"
								 select new LoanMasterTrancheView()
								 {
									 LOAN_NO = lm.LOAN_NO,
									 GCIF = lm.GCIF,
									 CCIF = lm.CCIF,
									 CCIF_NAME = lm.CCIF_NAME,
									 REMARKS = lm.REMARKS,
									 AGREEM_DATE1 = lm.AGREEM_DATE1,
									 AGREEM_DATE2 = lm.AGREEM_DATE2,
									 AMENDMENT_DATE = lm.AMENDMENT_DATE,

									 TRANCHE_NAME = tm.TRANCHE_NAME,
									 TRANCHE_NO = tm.TRANCHE_NO,
									 CUST_ABBR = cm.CUST_ABBR,
									 STATUS = lm.STATUS,
									 STATUS_DESC = sc.SHORT_DESC
								 })
								.ToList();
					return entry;
				}
			}
		}
	}
}
