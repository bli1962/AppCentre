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
    
    public partial class CUSTOMER_MASTER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CUSTOMER_MASTER()
        {
            this.FX_TRANSACTION = new HashSet<FX_TRANSACTION>();
        }
    
        public Nullable<int> CUST_ID { get; set; }
        public string CUST_CODE { get; set; }
        public string BRANCH_NO { get; set; }
        public string CUST_ABBR { get; set; }
        public string CUST_NAME { get; set; }
        public string ADDRESS { get; set; }
        public string ADDRESS2 { get; set; }
        public string TEL_NO { get; set; }
        public string TELEX_NO { get; set; }
        public string STAFF_LOAN_FLG { get; set; }
        public string MAIL_COUNTRY { get; set; }
        public string LOCATION_COUNTRY { get; set; }
        public string NATIONALITY { get; set; }
        public string STATUS_CD { get; set; }
        public string DEPARTMENT_CD { get; set; }
        public string OFFICER_CD { get; set; }
        public Nullable<System.DateTime> OPENING_DATE { get; set; }
        public Nullable<System.DateTime> LAST_UPDATED { get; set; }
        public string STEPS_CUST_CD { get; set; }
        public string INDUSTRY_CD { get; set; }
        public string DKB_COMPANY_CD { get; set; }
        public string GROUP_COMPANY_CD { get; set; }
        public string STANCE { get; set; }
        public string JAPANESE_FLG { get; set; }
        public string ATTRIBUTE_CD { get; set; }
        public string ATTRIBUTE_SUB_CD { get; set; }
        public Nullable<decimal> RATIO_OF_INV_JP { get; set; }
        public Nullable<decimal> RATIO_OF_INV_DKB { get; set; }
        public Nullable<decimal> RATIO_OF_INV_GVN { get; set; }
        public string PARENT_CUST_CD { get; set; }
        public string PARENT_NATIONALITY { get; set; }
        public string TAX_NO { get; set; }
        public string GOOD_ASSET_FLG { get; set; }
        public string OFFSHORE_CD { get; set; }
        public string TAX_CD { get; set; }
        public string TAX_ID { get; set; }
        public string BOE_CD { get; set; }
        public string AREA_CD { get; set; }
        public string BUSINESS_CONDITION { get; set; }
        public string OBLIGOR_SUPERVISION { get; set; }
        public string CREDIT_SUPERVISION { get; set; }
        public string LOCAL_INDUSTRY_CD { get; set; }
        public string LICENCE_FLG { get; set; }
        public string BANK_FLG { get; set; }
        public string CFS_IO { get; set; }
        public string BANK_MANAGEMENT_CD { get; set; }
        public string GROUP_CD { get; set; }
        public string COMPANY_CD { get; set; }
        public string GUARANTOR_CD { get; set; }
        public string GUARANTOR_COUNTRY { get; set; }
        public string GUARANTOR_ATTR_CD { get; set; }
        public string GUARANTOR_NAME { get; set; }
        public string COLLATERAL_NO { get; set; }
        public string BANK_GROUP_ABBR { get; set; }
        public string SUBLIMIT_BANK_ABBR { get; set; }
        public string MARK3_CD { get; set; }
        public string CORRES_BANK_CD { get; set; }
        public string GRADE_CD { get; set; }
        public Nullable<int> RATING_POINT { get; set; }
        public Nullable<System.DateTime> RATING_DATE { get; set; }
        public Nullable<System.DateTime> NEXT_REVIEW_DATE { get; set; }
        public string RETAIL_ACT_NO { get; set; }
        public string REMARKS_1 { get; set; }
        public string REMARKS_2 { get; set; }
        public string REMARKS_3 { get; set; }
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
        public virtual ICollection<FX_TRANSACTION> FX_TRANSACTION { get; set; }
    }
}