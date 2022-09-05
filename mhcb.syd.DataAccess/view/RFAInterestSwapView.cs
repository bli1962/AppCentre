using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mhcb.syd.DataAccess.View
{
    public class RFRInterestSwapView
    {
        //-- EUC --'
        public string DATA_DATE { get; set; }
        public string REF_NO { get; set; }
        public string CUST_ABBR { get; set; }
        public string CUST_NAME { get; set; }
        public string SWAP_TERM { get; set; }
    
        public string CONTRACT_DATE_OPE { get; set; }
        public string START_VALUE { get; set; }
        public string CONTRACT_DATE_VALUE { get; set; }
        public string MATURITY { get; set; }
        public string CCY_ABBR { get; set; }
        public string BALANCE { get; set; }
        public string PRINCIPAL_AMOUNT { get; set; }
        public string SPREAD { get; set; }
        public string INT_RATE { get; set; }

        //-- Murex --'
        public string CORP_NAME { get; set; }
        public string REF_SUB_NO { get; set; }
 
        public string INT_ACCRUAL_BUY_REC { get; set; }
        public string INT_ACCRUAL_SELL_PAY { get; set; }
        public string INT_ACCRUAL { get; set; }

        public string PAY_REV { get; set; }
        public string FIXED_FLOAT { get; set; }

        public string IntAccrual_RFR { get; set; }
        public string IntRate_RFR { get; set; }
        public string IntAccrualCreatedDate { get; set; }

    }
}
