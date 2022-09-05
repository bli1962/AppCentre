using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mhcb.syd.DataAccess.View
{
    public class OnDemandBatchLogView
    {
        public int LogId { get; set; }
        public int BatchId { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string CreatedWhen { get; set; }

    }
}
