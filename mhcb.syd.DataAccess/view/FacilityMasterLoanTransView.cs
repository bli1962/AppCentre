using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mhcb.syd.DataAccess.View
{
    public class FacilityMasterLoanTransView
    {
        public string REF_NO { get; set; }
        public string APPLN_NO { get; set; }
        public string APPLN_NO1 { get; set; }
        public string RATING1 { get; set; }
        public Nullable<short> RATING2 { get; set; }
        public Nullable<short> PROD_TYPE1 { get; set; }
        public Nullable<short> PROD_TYPE2 { get; set; }
        public Nullable<short> PROD_TYPE3 { get; set; }
        public Nullable<short> PROD_TYPE4 { get; set; }
        public Nullable<System.DateTime> APPR_DATE { get; set; }
        public Nullable<System.DateTime> APPR_DATE1 { get; set; }
        public Nullable<bool> MULTI_CCY { get; set; }
        public string BASE_CCY { get; set; }
        public Nullable<decimal> LIMIT { get; set; }
        public Nullable<System.DateTime> MAT_DATE { get; set; }
        public Nullable<System.DateTime> SIGHT_DATE { get; set; }
        public string MAX_INT_PERIOD { get; set; }
        public string BASE_RATE { get; set; }
        public string BASE_RATE_REMARK { get; set; }
        public Nullable<decimal> MARGIN { get; set; }
        public Nullable<short> INT_CALC_METHOD { get; set; }
        public string INT_CALC_REMARK { get; set; }
        public Nullable<short> REPAY_TYPE { get; set; }
        public string REPAY_REMARK { get; set; }
        public string OTHER_REMARK { get; set; }
        public Nullable<short> G_FEE_TYPE { get; set; }
        public Nullable<decimal> G_FEE_RATE { get; set; }
        public byte FEE_KIND_ID { get; set; }
        public decimal FEE_RATE { get; set; }
        public byte FEE_CALC_METHOD_TYPE { get; set; }
        public System.DateTime FEE_START_DATE { get; set; }
        public System.DateTime FEE_END_DATE { get; set; }
        public byte FEE_PAYMENT_TYPE { get; set; }
        public byte FREQ_TYPE { get; set; }
        public byte DAY_OF_MONTH_TYPE { get; set; }
        public System.DateTime FEE_COL_DATE { get; set; }
        public string FEE_REMARKS { get; set; }
        public string STATUS { get; set; }
        public string STATUS_DESC { get; set; }
        public string CUST_ABBR { get; set; }
        public string BRANCH_NO { get; set; }
        public bool DIRECT_DEBIT { get; set; }


        //public string REF_NO { get; set; }
        public int TRANS_NO { get; set; }
        public Nullable<int> RO_NO { get; set; }
        public string STATUS2 { get; set; }
        public string STATUS2_DESC { get; set; }
        public string START_DATE { get; set; }
        public string DUE_DATE { get; set; }
        public Nullable<bool> INC_DUE_DATE { get; set; }
        public Nullable<int> DAYS { get; set; }
        public string DRAW_CCY { get; set; }
        public Nullable<decimal> DRAW_AMT { get; set; }
        public Nullable<decimal> DRAW_EQUIV { get; set; }
        public string DRAW_EXCH_RATE_IND { get; set; }
        public Nullable<decimal> DRAW_EXCH_RATE { get; set; }
        public string REPAY_CCY { get; set; }
        public Nullable<decimal> REPAY_AMT { get; set; }
        public Nullable<decimal> REPAY_EQUIV { get; set; }
        public string REPAY_EXCH_RATE_IND { get; set; }
        public Nullable<decimal> REPAY_EXCH_RATE { get; set; }
        public Nullable<decimal> BAL_AMT { get; set; }
        public Nullable<decimal> INT_RATE { get; set; }
        public Nullable<decimal> BASE_RATE2 { get; set; }
        public Nullable<decimal> INT_AMT { get; set; }
        public Nullable<decimal> INT_AMT_CALC { get; set; }
        public string INT_RECV_DATE { get; set; }
        public string INT_RECV_BY { get; set; }
        //public Nullable<System.DateTime> CREATE_DATE { get; set; }
        //public string PROC_BY1 { get; set; }
        //public string PROC_BY2 { get; set; }
        //public string APPR_BY1 { get; set; }
        //public string APPR_BY2 { get; set; }
        //public string CHK_BY { get; set; }
        //public string FLAG_DEL_BY { get; set; }
        public string REMARKS { get; set; }
        public Nullable<bool> INC_START_DATE { get; set; }
        public string DRAWDOWN_NO { get; set; }
        public string SCHEME_NO { get; set; }
        public string AUTHORIZED { get; set; }
        public Nullable<System.DateTime> FINAL_DUE_DATE { get; set; }
        public Nullable<System.DateTime> INTEREST_TO_DATE { get; set; }
        public string INTEREST_CALC_CODE { get; set; }
        public string BASE_RATE_CODE { get; set; }
        public Nullable<decimal> SPREAD { get; set; }
        public string INTEREST_COLLECT { get; set; }
        public string INTERNAL_RATE { get; set; }
        public string RECEIVING_METHOD { get; set; }
        public string PAYING_METHOD { get; set; }
        public Nullable<int> ACCOUNT_CODE { get; set; }
        public string CUSTOMER_ACCOUNT_CODE_BORROWER { get; set; }
        public Nullable<int> CUSTOMER_ACCOUNT_CODE { get; set; }
        public Nullable<int> ACCOUNT_NO1 { get; set; }
        public Nullable<int> ACCOUNT_NO2 { get; set; }
        public string ACCOUNT_NO1_MANUAL_ENTRY { get; set; }
        public string ACCOUNT_NO2_MANUAL_ENTRY { get; set; }
        public string ATTENTION { get; set; }
        public string Old_RO_InterestCollect { get; set; }
        public Nullable<decimal> Old_RO_IntAmt { get; set; }
        public string RO_Type1 { get; set; }
        public string RO_Type2 { get; set; }
        public Nullable<System.DateTime> InitialInputDateTime { get; set; }
        public Nullable<System.DateTime> InitialApprovalDateTime { get; set; }
        public Nullable<System.DateTime> FinalApprovalDateTime { get; set; }
        public Nullable<System.DateTime> ModifyRateDateTime { get; set; }
        public Nullable<System.DateTime> ApproveModifiedRateDateTime { get; set; }
        //public string ModifyRatesBy { get; set; }
        //public string AppRatesBy { get; set; }


        public string CUST_NAME { get; set; }
        public string DEPARTMENT { get; set; }
        public string PROD_TYPE1_DESC { get; set; }
        public string PROD_TYPE2_DESC { get; set; }
        public string PROD_TYPE3_DESC { get; set; }
        public string PROD_TYPE4_DESC { get; set; }
        public string TransType { get; set; }
    }
}
