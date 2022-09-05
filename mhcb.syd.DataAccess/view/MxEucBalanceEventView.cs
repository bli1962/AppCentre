using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mhcb.syd.DataAccess.View
{
    public class MxEucBalanceEventView
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Nullable<int> BatchNo { get; set; }
        public string Category { get; set; }
        public string EventType { get; set; }
        public string ClosingDate { get; set; }
        public string TradeDate { get; set; }
        public string EffectiveDate { get; set; }

        public string TimeSent { get; set; }
        public string Status { get; set; }
        public string StatusUpdateTime { get; set; }
       
        public string GBaseReferenceNo { get; set; }
        public string CustomerAbbreviation { get; set; }
       
    }
}
