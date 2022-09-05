using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mhcb.syd.DataAccess.View
{
    public class MoneyMarketTicketView
    {
        public int TicketId { get; set; }

        public string REF_NO { get; set; }
        public string Counterparty { get; set; }

        public string TradeId { get; set; }
        public string Event { get; set; }
        public string Typology { get; set; }
    
        public string PortfolioGroup { get; set; }
        public string PortfolioDesk { get; set; }
        public string PortfolioBook { get; set; }

        public string Currency { get; set; }
        public Nullable<decimal> PrincipalAmount { get; set; }
        public string InputBy { get; set; }
        public string AuthorisedBy { get; set; }
        public string TradeDate { get; set; }
        public string PrintDateTime { get; set; }
        public string PrintStatus { get; set; }
        public string GIDUpload { get; set; }
    }
}
