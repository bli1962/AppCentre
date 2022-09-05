using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mhcb.syd.DataAccess.View
{
    public class MoneyMarketReportView
    {
        //public int MMTicketId { get; set; }
        public string GBaseRefNo { get; set; }

        public string CounterpartyAbbreviation { get; set; }
        public string CounterpartyShortName { get; set; }
        //public string CounterpartyDescription { get; set; }
        public string TicketTemplate { get; set; }



        //public string LegalEntity { get; set; }
        public string Event { get; set; }
        public string Typology { get; set; }

        //public string MurexTradeId { get; set; }
        //public string PortfolioGroup { get; set; }
        public string PortfolioDesk { get; set; }
        public string PortfolioBook { get; set; }

        public int ContractVersion { get; set; }

        public string OperationDate { get; set; }
        public string ContractDate { get; set; }
        public string ValueDate { get; set; }
        public string FinalDueDate { get; set; }
        public string DueDate { get; set; }
        public string TradeDate { get; set; }
        public string PaymentDate { get; set; }

        //public string DealingMethod { get; set; }
        //public string DealingContact { get; set; }

        //public string IBF { get; set; }

        //public int DivisionCode { get; set; }
        //public Nullable<int> Discount { get; set; }
        //public string SchemeNumber { get; set; }
        //public string Authorised { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> PrincipalAmount { get; set; }
        //public Nullable<int> RolloverNumber { get; set; }
        //public string RateConvention { get; set; }


        //public string Broker { get; set; }
        //public string BrokerName { get; set; }
        //public Nullable<int> TermDays { get; set; }
        //public Nullable<int> RateType { get; set; }
        public Nullable<decimal> InterestRate { get; set; }
        public Nullable<decimal> InterestAmount { get; set; }
        //public string FundType { get; set; }
        public Nullable<decimal> InternalRate { get; set; }
        //public string InterestSet { get; set; }
        //public string InterestSetLabel { get; set; }
        //public string WithholdingTaxBearer { get; set; }
        //public string WithholdingTaxBearerLabel { get; set; }
        //public string WithholdingTaxCode { get; set; }
        //public Nullable<decimal> WithholdingTaxRate { get; set; }
        //public Nullable<decimal> WithholdingTaxAmount { get; set; }
        //public string Remarks { get; set; }
        //public string FrontOfficeInputBy { get; set; }
        //public string FrontOfficeAuthorisedBy { get; set; }

        public string SettlementDetails { get; set; }


        //-- not for ('Call Depo', 'Call Loan') , 
        //-- not for ('CD')
        //-- but for Typology in ('Depo', 'Loan') 
        public Nullable<decimal> NetInterest { get; set; }
        public Nullable<decimal> TotalDue { get; set; }
        //public Nullable<decimal> Brokerage { get; set; }


        //-- not for Typology in ('Depo', 'Loan') 
        //-- not for ('CD')  
        public Nullable<decimal> PrincipalAmountPrevious { get; set; }
        public string RolloverType { get; set; }
        public string RolloverTypeLabel { get; set; }
        public Nullable<decimal> PrincipalAmountClosing { get; set; }
        public string SettlementDirectionLabel { get; set; }

        //-- For ('CD')  only
        public string BuySell { get; set; }

        //-- not for ('Call Depo', 'Call Loan') , 
        //-- not for ('Depo', 'Loan')  
        public Nullable<decimal> Denomination { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> FaceAmount { get; set; }
        public Nullable<decimal> ConsiderationAmount { get; set; }

        public string TransactionStatus { get; set; }

        //public string Issuer { get; set; }
        //public string IssuerFullName { get; set; }
        //public string IssuerGBaseAbbreviation { get; set; }
        //public Nullable<decimal> Yield { get; set; }
        //public Nullable<decimal> IssuePrice { get; set; }

        //public string CustomerCode { get; set; }
        //public bool PrintStatus { get; set; }
        //public string PayoutType { get; set; }

        //public string Nature { get; set; }
        //public string Instrument { get; set; }

        //public System.DateTime TicketCreatedTimestamp { get; set; }
        //public bool GIDUpload { get; set; }

        public string DEPARTMENT_CD { get; set; }
    }
}

