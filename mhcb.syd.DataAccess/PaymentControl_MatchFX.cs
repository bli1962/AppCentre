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
    
    public partial class PaymentControl_MatchFX
    {
        public int MatchFXId { get; set; }
        public string GBaseRefNo { get; set; }
        public bool Processed { get; set; }
        public Nullable<int> PaymentId { get; set; }
        public bool IsBank { get; set; }
        public Nullable<System.DateTime> UpdatedWhen { get; set; }
        public string UpdatedWho { get; set; }
        public System.DateTime CreatedWhen { get; set; }
    
        public virtual PaymentControl_Payment PaymentControl_Payment { get; set; }
    }
}
