using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mhcb.syd.DataAccess.View
{
    public class FXTransactionExtView
    {
        public string REF_NO { get; set; }
        public long ContractId { get; set; }
        public long TradeId { get; set; }
        public string Event { get; set; }
        public string PortfolioGroup { get; set; }
        public string PortfolioDesk { get; set; }
        public string PortfolioBook { get; set; }

        public string AuthorisedDealer { get; set; }
        public string CONTRACT_CCY_ABBR_ORIG { get; set; }
        public string EQUIV_CCY_ABBR_ORIG { get; set; }
        public string Printed { get; set; }
        public string PrintDateTime { get; set; }
        public string GIDUpload { get; set; }
    }
}
