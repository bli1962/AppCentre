using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mhcb.syd.DataAccess.View
{
    public class LoanMasterTrancheView
    {
        public int LOAN_NO { get; set; }
        public string BRANCH_NO { get; set; }
        public string CUST_ABBR { get; set; }
        public Nullable<bool> MULTI_BRW { get; set; }
        public Nullable<System.DateTime> AGREEM_DATE1 { get; set; }
        public Nullable<System.DateTime> AGREEM_DATE2 { get; set; }
        public string AMENDMENT_DATE { get; set; }
        public string RATING { get; set; }
        public Nullable<short> PROD_TYPE2 { get; set; }
        public Nullable<short> PROD_TYPE4 { get; set; }
        public string REMARKS { get; set; }
        public string AGENT { get; set; }
        public string ADDRESS { get; set; }
        public string CONTACT { get; set; }
        public string TEL_NO { get; set; }
        public string FAX_NO { get; set; }
        public string STATUS { get; set; }
        public string STATUS_DESC { get; set; }
        //public string PROC_BY { get; set; }
        //public Nullable<System.DateTime> PROC_ON { get; set; }
        //public string APPR_BY { get; set; }
        //public Nullable<System.DateTime> APPR_ON { get; set; }
        public string CCIF { get; set; }
        public string CCIF_NAME { get; set; }
        public string GCIF { get; set; }
        public string RATIO_SPREAD1 { get; set; }
        public string RATIO_SPREAD2 { get; set; }


        public int TRANCHE_NO { get; set; }
        //public Nullable<int> LOAN_NO { get; set; }
        public string TRANCHE_TYPE { get; set; }
        public string TRANCHE_NAME { get; set; }
        public Nullable<short> PROD_TYPE1 { get; set; }
        public Nullable<short> PROD_TYPE5 { get; set; }
        public string APPLN_NO { get; set; }
        public Nullable<System.DateTime> APPLN_DATE { get; set; }
        public Nullable<System.DateTime> HO_APPR_DATE { get; set; }
        public string APPLN_NO_RELT { get; set; }
        public Nullable<System.DateTime> APPLN_DATE_RELT { get; set; }
        public string BASE_CCY { get; set; }
        public Nullable<bool> MULTI_CCY { get; set; }
        public Nullable<decimal> TRANCHE_PORTION { get; set; }
        public Nullable<decimal> SYND_PORTION { get; set; }
        public string PORTION_NOTES { get; set; }
        public Nullable<System.DateTime> AVAIL_START { get; set; }
        public Nullable<System.DateTime> AVAIL_END { get; set; }
        public string PERIOD_NOTES { get; set; }
        public Nullable<System.DateTime> MAT_DATE { get; set; }
        public string MAT_DATE_NOTES { get; set; }
        public string MIN_DD { get; set; }
        public string MULTIPLE { get; set; }
        public string STATUS2 { get; set; }
        public string STATUS2_DESC { get; set; }
        //public string PROC_BY { get; set; }
        //public Nullable<System.DateTime> PROC_ON { get; set; }
        //public string APPR_BY { get; set; }
        //public Nullable<System.DateTime> APPR_ON { get; set; }
        public string MULTI_CCY_DESC { get; set; }
        public short MAXI_DRAWDOWN_PERIOD { get; set; }
        public short MAXI_DRAWDOWN_DAYS { get; set; }
        public short MAXI_DRAWDOWN_TIMES { get; set; }
        public bool SILENT_MARGIN { get; set; }
        public string TRD_INTERNAL_RATE { get; set; }
        public string CFD_BASE_RATE { get; set; }
        public string MARGIN_11AM { get; set; }
        public string MARGIN_REMARK_11AM { get; set; }
        public bool RATING_TYPE { get; set; }
        public bool RATIIO_TYPE { get; set; }
        public string FINANCIAL_NAME { get; set; }
    }
}
