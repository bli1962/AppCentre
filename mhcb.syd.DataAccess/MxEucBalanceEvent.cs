//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace mhcb.syd.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class MxEucBalanceEvent
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public System.DateTime TimeSent { get; set; }
        public string Status { get; set; }
        public System.DateTime StatusUpdateTime { get; set; }
        public string Category { get; set; }
        public string EventType { get; set; }
        public string GBaseReferenceNo { get; set; }
        public string CustomerAbbreviation { get; set; }
        public string BranchNo { get; set; }
        public Nullable<int> Account { get; set; }
        public string TradeDate { get; set; }
        public string EffectiveDate { get; set; }
        public Nullable<decimal> NominalAmount { get; set; }
        public Nullable<decimal> SettlementAmount { get; set; }
        public string Currency { get; set; }
        public string BorrowOrLend { get; set; }
        public string ClosingDate { get; set; }
        public string MaturityDate { get; set; }
        public string Revolving { get; set; }
        public string FacilityType { get; set; }
        public string TradeExternalId { get; set; }
        public Nullable<decimal> Rates { get; set; }
        public Nullable<decimal> InitialRate { get; set; }
        public Nullable<decimal> SalesMargin { get; set; }
        public Nullable<int> BatchNo { get; set; }
    }
}
