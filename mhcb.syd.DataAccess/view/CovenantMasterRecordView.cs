using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mhcb.syd.DataAccess.View
{
    public class CovenantMasterRecordView
    {
        public int COVENANT_NO { get; set; }
        public string CATEGORY { get; set; }
        public string BRANCH_NO { get; set; }
        public string CUST_ABBR { get; set; }
        public string ACCOUNT_OFFICER { get; set; }
        public string COV_TYPE { get; set; }
        public Nullable<byte> REPORT_TYPE { get; set; }
        public Nullable<byte> FREQ_TYPE { get; set; }
        public Nullable<byte> DAY_OF_MONTH_TYPE { get; set; }
        public string COV_START_DATE { get; set; }
        public string COV_END_DATE { get; set; }
        public Nullable<byte> TIME_OF_RECEIPT { get; set; }
        public Nullable<byte> CONDITION_TYPE { get; set; }
        public string MATH_SYMBOL { get; set; }
        public Nullable<decimal> VALUE { get; set; }
        public string DENOMINATION { get; set; }
        public Nullable<byte> MARGIN_BASE_TYPE { get; set; }
        public string DESCRIPTION { get; set; }
        public string STATUS { get; set; }
        public string STATUS_DESC { get; set; }
        //public string PROC_BY { get; set; }
        //public Nullable<System.DateTime> PROC_ON { get; set; }
        //public string APPR_BY { get; set; }
        //public Nullable<System.DateTime> APPR_ON { get; set; }


        public string CUST_NAME { get; set; }
        public string CCIF_NAME { get; set; }
        public string COVENANT_DECS { get; set; }

        public string CONDITION_GROUP_DESC { get; set; }
        public string CONDITION_DESC { get; set; }

        public string FREQ_DESC { get; set; }
        public string TIMING_DESC { get; set; }

        public int RECORD_NO { get; set; }
        public Nullable<byte> REPORT_FREQ_TYPE { get; set; }
        public string REPORT_DATE { get; set; }
        public string DUE_DATE { get; set; } 
        public string DATE_RECIEVED { get; set; }
        public Nullable<decimal> RATE { get; set; }
        public string DATE_RECIEVED_ADJ { get; set; }
        public Nullable<decimal> RATE_ADJ { get; set; }
        public string IN_COMPLIANCE { get; set; }
        public string REMARKS { get; set; }
        public string ORIGINAL_DESC { get; set; }
        public string AO_REMARKS { get; set; }
        public string STATUS2 { get; set; }
        public string STATUS2_DESC { get; set; }
        public Nullable<decimal> STRESS_VALUE { get; set; }
        //public string PROC_BY { get; set; }
        //public Nullable<System.DateTime> PROC_ON { get; set; }
        //public string APPR_BY { get; set; }
        //public Nullable<System.DateTime> APPR_ON { get; set; }

    }
}
