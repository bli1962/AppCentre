using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mhcb.syd.DataAccess.View
{
    public class RFRInterestLoanView
    {
        //-- EUC --'
        public string DATA_DATE { get; set; }
        public string RO_VALUE { get; set; }
        public string MATURITY_DATE { get; set; }
        public string RO_DUE { get; set; }
        public string FINAL_DUE { get; set; }
        public string REF_NO { get; set; }
        public string DD_NO { get; set; }
        public string CUST_ABBR { get; set; }
        public string CUST_NAME { get; set; }
        public string AccountTypeName { get; set; }
        public string DD_CCY { get; set; }
        public string DD_AMT { get; set; }
        public string DD_EQUIVALENT { get; set; }

        //-- Murex --'
        public string CORP_NAME { get; set; }
        public string INT_AMOUNT_RO { get; set; }
        public string INT_RATE_RO { get; set; }
        public string IntAccrual_RFR { get; set; }
        public string IntRate_RFR { get; set; }
        public string IntAccrualCreatedDate { get; set; }
    }
}
