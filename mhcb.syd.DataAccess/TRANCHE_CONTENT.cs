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
    
    public partial class TRANCHE_CONTENT
    {
        public int ID { get; set; }
        public int TRANCHE_NO { get; set; }
        public string REF_NO { get; set; }
        public string CUST_ABBR { get; set; }
        public Nullable<decimal> LIMIT { get; set; }
        public string CCY { get; set; }
        public string STATUS { get; set; }
    
        public virtual TRANCHE_MASTER TRANCHE_MASTER { get; set; }
    }
}
