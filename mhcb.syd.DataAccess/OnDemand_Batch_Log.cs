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
    
    public partial class OnDemand_Batch_Log
    {
        public int LogId { get; set; }
        public int BatchId { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public System.DateTime When { get; set; }
    
        public virtual OnDemand_Batch OnDemand_Batch { get; set; }
    }
}
