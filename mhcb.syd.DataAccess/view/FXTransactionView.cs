using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mhcb.syd.DataAccess.View
{
    public class FXTransactionView
    {
        public string REF_NO { get; set; }
        public string CUST_ABBR { get; set; }      
        public string CUST_NAME { get; set; }

        public string ACT_CD { get; set; }
        public string ACT_ABBR { get; set; }

        public string CONTRACT_DATE_OPE { get; set; }
        public string CONTRACT_DATE_VALUE { get; set; }
        public string DELIVARY_DATE { get; set; }

        public string SETTLEMENT_VALUE_DATE { get; set; }
        public string CONTRACT_CCY_ABBR { get; set; }
        public Nullable<decimal> CONTRACT_CCY_AMT { get; set; }
        public string EQUIV_CCY_ABBR { get; set; }
        public Nullable<decimal> EQUIV_CCY_AMT { get; set; }

        public Nullable<decimal> EXCHANGE_RATE { get; set; }
        public Nullable<decimal> INTERNAL_RATE { get; set; }
        public Nullable<decimal> SPREAD { get; set; }

        public string STATUS { get; set; }
        public string DELETION_STATUS { get; set; }
        public string GIP_STATUS { get; set; }
        public string GIP_DESCRIPTION { get; set; }
        public string TRAN_NO { get; set; }
        public string AUTHORIZE_BY { get; set; }
    }
}
