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
    
    public partial class BANK_INF
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BANK_INF()
        {
            this.BANK_INF_CORR = new HashSet<BANK_INF_CORR>();
        }
    
        public string SWIFT_ID { get; set; }
        public string BRANCH_NO { get; set; }
        public string SETTL_BRANCH_NO { get; set; }
        public string ACRONYM { get; set; }
        public string NAME_ADDR_1 { get; set; }
        public string NAME_ADDR_2 { get; set; }
        public string NAME_ADDR_3 { get; set; }
        public string NAME_ADDR_4 { get; set; }
        public string LOCATION_COUNTRY { get; set; }
        public string SWIFT_FLG { get; set; }
        public string CHIPS_UID { get; set; }
        public string CHIPS_ABA { get; set; }
        public string FED_ID { get; set; }
        public string CHAPS_SORT_CD { get; set; }
        public string COR_BANK_CD { get; set; }
        public string MHCB_BR_ABBR { get; set; }
        public string CUST_ABBR { get; set; }
        public string REMARKS { get; set; }
        public string ADD_BY { get; set; }
        public System.DateTime ADD_ON { get; set; }
        public string AUTHORIZE_BY { get; set; }
        public System.DateTime AUTHORIZE_ON { get; set; }
        public string MODIFY_BY { get; set; }
        public System.DateTime MODIFY_ON { get; set; }
        public string DELETE_BY { get; set; }
        public System.DateTime DELETE_ON { get; set; }
        public string STATUS { get; set; }
        public string DELETION_STATUS { get; set; }
        public string GIP_STATUS { get; set; }
        public string GIP_DESCRIPTION { get; set; }
        public string TRAN_NO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BANK_INF_CORR> BANK_INF_CORR { get; set; }
    }
}
