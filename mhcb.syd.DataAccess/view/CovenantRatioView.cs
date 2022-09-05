using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mhcb.syd.DataAccess.View
{
    public class CovenantRatioView
    {
        public int COVENANT_NO { get; set; }
        public string CUST_ABBR { get; set; }
        public string CCIF_NAME { get; set; }
        public string COVENANT_DECS { get; set; }

        public string CONDITION_GROUP_DESC { get; set; }
        public string CONDITION_DESC { get; set; }

        public int RECORD_NO { get; set; }
        public string DATE_RECIEVED_ADJ { get; set; }
        public string RATE_ADJ { get; set; }

        public string DATE_RECIEVED { get; set; }
        public string REPORT_DATE { get; set; }
        public string DUE_DATE { get; set; }
        public string RATE { get; set; }

    }
}
