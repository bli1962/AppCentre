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
    
    public partial class COVENANT_RECORD
    {
        public int COVENANT_NO { get; set; }
        public int RECORD_NO { get; set; }
        public Nullable<byte> REPORT_FREQ_TYPE { get; set; }
        public Nullable<System.DateTime> REPORT_DATE { get; set; }
        public Nullable<System.DateTime> DUE_DATE { get; set; }
        public Nullable<byte> CONDITION_TYPE { get; set; }
        public string MATH_SYMBOL { get; set; }
        public Nullable<decimal> VALUE { get; set; }
        public string DENOMINATION { get; set; }
        public Nullable<System.DateTime> DATE_RECIEVED { get; set; }
        public Nullable<decimal> RATE { get; set; }
        public Nullable<System.DateTime> DATE_RECIEVED_ADJ { get; set; }
        public Nullable<decimal> RATE_ADJ { get; set; }
        public string IN_COMPLIANCE { get; set; }
        public string REMARKS { get; set; }
        public string ORIGINAL_DESC { get; set; }
        public string AO_REMARKS { get; set; }
        public string STATUS { get; set; }
        public string PROC_BY { get; set; }
        public Nullable<System.DateTime> PROC_ON { get; set; }
        public string APPR_BY { get; set; }
        public Nullable<System.DateTime> APPR_ON { get; set; }
        public Nullable<decimal> STRESS_VALUE { get; set; }
    
        public virtual COVENANT_MASTER COVENANT_MASTER { get; set; }
    }
}
