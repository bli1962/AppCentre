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
    
    public partial class OnDemand_Batch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OnDemand_Batch()
        {
            this.OnDemand_Batch_Log = new HashSet<OnDemand_Batch_Log>();
        }
    
        public int BatchId { get; set; }
        public Nullable<int> BatchNo { get; set; }
        public bool Started { get; set; }
        public bool Completed { get; set; }
        public bool Errored { get; set; }
        public bool IsRec { get; set; }
        public string WhoRequested { get; set; }
        public System.DateTime CreatedWhen { get; set; }
        public System.DateTime UpdatedWhen { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OnDemand_Batch_Log> OnDemand_Batch_Log { get; set; }
    }
}