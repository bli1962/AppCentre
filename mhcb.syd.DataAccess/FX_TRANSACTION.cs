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
    
    public partial class FX_TRANSACTION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FX_TRANSACTION()
        {
            this.FX_SETTLEMENT_INF = new HashSet<FX_SETTLEMENT_INF>();
        }
    
        public string REF_NO { get; set; }
        public string DealNo { get; set; }
        public short TransCode { get; set; }
        public string DEALTYPE { get; set; }
        public string SUBTYPE { get; set; }
        public string ACT_CD { get; set; }
        public string ACT_ABBR { get; set; }
        public string CUST_CODE { get; set; }
        public string CUST_ABBR { get; set; }
        public string BRANCH_NO { get; set; }
        public string DIVISION_CD { get; set; }
        public string IBF_ID { get; set; }
        public string SCHEME_NO { get; set; }
        public string AUTHORIZED { get; set; }
        public string REVISE { get; set; }
        public string COVER_PO_FLG { get; set; }
        public Nullable<System.DateTime> CONTRACT_DATE_OPE { get; set; }
        public Nullable<System.DateTime> CONTRACT_DATE_VALUE { get; set; }
        public Nullable<System.DateTime> DELIVARY_DATE { get; set; }
        public Nullable<System.DateTime> DELIVARY_THRU { get; set; }
        public Nullable<System.DateTime> ACCRUAL_FROM { get; set; }
        public Nullable<System.DateTime> ACCRUAL_TO { get; set; }
        public Nullable<System.DateTime> SETTLEMENT_VALUE_DATE { get; set; }
        public string CCY_CD { get; set; }
        public string CONTRACT_CCY_CD { get; set; }
        public string CONTRACT_CCY_ABBR { get; set; }
        public Nullable<decimal> CONTRACT_CCY_AMT { get; set; }
        public Nullable<decimal> CONTRACT_CCY_BAL { get; set; }
        public Nullable<decimal> CONTRACT_CCY_TTB { get; set; }
        public string EQUIV_CCY_CD { get; set; }
        public string EQUIV_CCY_ABBR { get; set; }
        public Nullable<decimal> EQUIV_CCY_AMT { get; set; }
        public Nullable<decimal> EQUIV_CCY_BAL { get; set; }
        public Nullable<decimal> EQUIV_CCY_TTB { get; set; }
        public Nullable<decimal> EXCHANGE_RATE { get; set; }
        public Nullable<decimal> INTERNAL_RATE { get; set; }
        public Nullable<decimal> SPREAD { get; set; }
        public string ACCOUNT_ENTRY { get; set; }
        public string BROKER { get; set; }
        public string REMARKS { get; set; }
        public string REMARKS_TRD { get; set; }
        public string SWAP_REF_NO { get; set; }
        public string STATUS_CD { get; set; }
        public string ADDRESSEE { get; set; }
        public string CODE56 { get; set; }
        public string CODE57 { get; set; }
        public string CODE57A { get; set; }
        public string CODE58 { get; set; }
        public string SetInstruction { get; set; }
        public string DEALER { get; set; }
        public Nullable<int> OptionsFlag { get; set; }
        public Nullable<int> ExecutionRefNo { get; set; }
        public string ADD_BY { get; set; }
        public System.DateTime ADD_ON { get; set; }
        public string AUTHORIZE_BY { get; set; }
        public System.DateTime AUTHORIZE_ON { get; set; }
        public string VERIFY_BY { get; set; }
        public System.DateTime VERIFY_ON { get; set; }
        public string MODIFY_BY { get; set; }
        public System.DateTime MODIFY_ON { get; set; }
        public string DELETE_BY { get; set; }
        public Nullable<System.DateTime> DELETE_ON { get; set; }
        public string STATUS { get; set; }
        public string DELETION_STATUS { get; set; }
        public string GIP_STATUS { get; set; }
        public string GIP_DESCRIPTION { get; set; }
        public string TRAN_NO { get; set; }
    
        public virtual CUSTOMER_MASTER CUSTOMER_MASTER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FX_SETTLEMENT_INF> FX_SETTLEMENT_INF { get; set; }
    }
}
