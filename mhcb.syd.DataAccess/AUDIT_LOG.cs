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
    
    public partial class AUDIT_LOG
    {
        public int AUDIT_ID { get; set; }
        public string USER_ID { get; set; }
        public Nullable<System.DateTime> AUDIT_DATE { get; set; }
        public string REF_NO { get; set; }
        public string TRANS_NO { get; set; }
        public string AUDIT_DESC { get; set; }
        public string APP_NAME { get; set; }
        public string TABLE_NAME { get; set; }
        public string ORIGINAL_VALUE { get; set; }
        public string NEW_VALUE { get; set; }
    }
}
