using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mhcb.syd.DataAccess.View
{
    public class FXDeliveryView
    {
        public int ID { get; set; }
        public string REF_NO { get; set; }
        public string CUST_ABBR { get; set; }
        public string REC_SETTL_CODE { get; set; }
        public string REC_CUST_ABBR { get; set; }
        public string REC_CCY_CD { get; set; }
        public string PAY_SETTL_CODE { get; set; }
        public string PAY_CUST_ABBR { get; set; }
        public string PAY_CCY_CD { get; set; }

        public string STATUS { get; set; }
        public string DELETION_STATUS { get; set; }
        public string GIP_STATUS { get; set; }
        public string GIP_DESCRIPTION { get; set; }
        public string TRAN_NO { get; set; }
        public string AUTHORIZE_BY { get; set; }

    }
}
