using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mhcb.syd.DataAccess.View
{
    public class SwiftPaymentView
    {
        public int PaymentId { get; set; }
        public string CustomerAbbreviation { get; set; }
        public string ValueDate { get; set; }
        public string ReleaseDate { get; set; }
        public string PaymentCurrencyCode { get; set; }


        public int SwiftPaymentId { get; set; }
        public bool Processed { get; set; }
        public Nullable<int> StatusId { get; set; }
        public string StatusDescription { get; set; }
        public string GBaseRefNo { get; set; }
        public string PaymentDate { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string SenderBank { get; set; }
        public string ReceiverBank { get; set; }
        public string CreatedWhen { get; set; }

        public string Product { get; set; }

    }
}
