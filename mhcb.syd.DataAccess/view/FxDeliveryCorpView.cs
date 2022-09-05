using System;

namespace mhcb.syd.DataAccess.View
{
	public class FxDeliveryView
	{
		public string REF_NO { get; set; }
		public string CUST_ABBR { get; set; }
		public string CUST_NAME { get; set; }
		public Nullable<decimal> BuyAmt { get; set; }
		public string BuyCCY { get; set; }
		public string REC_SETTL_CODE { get; set; }
		public string BUY_ACCT_TYPE { get; set; }
		public string REC_ACT_CD { get; set; }
		public string REC_ACT_NO { get; set; }
		public string REC_CUST_ABBR { get; set; }
		public Nullable<decimal> SellAmt { get; set; }
		public string SellCCY { get; set; }
		public string PAY_SETTL_CODE { get; set; }
		public string PAY_ACCT_TYPE { get; set; }
		public string PAY_ACT_CD { get; set; }
		public string PAY_ACT_NO { get; set; }
		public string PAY_CUST_ABBR { get; set; }
		public Nullable<decimal> EXCHANGE_RATE { get; set; }
		public string CONTRACT_DATE_OPE { get; set; }
        public string CONTRACT_DATE_VALUE { get; set; }
        public string DELIVARY_DATE { get; set; }

    }
}
