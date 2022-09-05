using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mhcb.syd.DataAccess.View
{
    public class OnDemandBatchView
    {
        public int BatchId { get; set; }
        public Nullable<int> BatchNo { get; set; }
        public string Started { get; set; }
        public string Completed { get; set; }
        public string Errored { get; set; }
        public string IsRec { get; set; }
        public string WhoRequested { get; set; }
        public string CreatedWhen { get; set; }
        public string UpdatedWhen { get; set; }
    }
}
