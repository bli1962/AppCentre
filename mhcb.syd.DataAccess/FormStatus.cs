
namespace mhcb.syd.DataAccess.View
{
    public class SIFStatus
    {
        public int SIF_ID { get; set; }

        public string AUTHORIZE_BY { get; set; }
        public string STATUS { get; set; }
        public string DELETION_STATUS { get; set; }
        public string GIP_STATUS { get; set; }
        public string GIP_DESCRIPTION { get; set; }
        public string TRAN_NO { get; set; }
    }

    public class BankInfStatus
    {
        public string SWIFT_ID { get; set; }
        public string AUTHORIZE_BY { get; set; }
        //public System.DateTime DELETE_ON { get; set; }
        public string STATUS { get; set; }
        public string DELETION_STATUS { get; set; }
        public string GIP_STATUS { get; set; }
        public string GIP_DESCRIPTION { get; set; }
        public string TRAN_NO { get; set; }
    }

    public class FXTranStatus
    {
        public string REF_NO { get; set; }
        public string AUTHORIZE_BY { get; set; }
        //public System.DateTime DELETE_ON { get; set; }
        public string STATUS { get; set; }
        public string DELETION_STATUS { get; set; }
        public string GIP_STATUS { get; set; }
        public string GIP_DESCRIPTION { get; set; }
        public string TRAN_NO { get; set; }
    }

    public class FXDeliveryCorp
    {
        public string Date { get; set; }
        public int Summary { get; set; }
    }

    public class FXFCustStatus
    {
        public string Abbreviation { get; set; }
        public string DeptCode { get; set; }
        public string TaxCode { get; set; }
        public string STATUS { get; set; }
        public string AUTHORIZE_BY { get; set; }
        public string GIP_STATUS { get; set; }
        public string GIP_DESCRIPTION { get; set; }
        public string TRAN_NO { get; set; }
    }

    public class FXFCustAttribute
    {
        public string Abbreviation { get; set; }
        public string DeptCode { get; set; }
        public string TaxCode { get; set; }
        public string STATUS { get; set; }
        public string AUTHORIZE_BY { get; set; }
    }

    public class UserAttribute
    {
        public string USER_ID { get; set; }
        //public string PWD_CHANGE_IND { get; set; }
        //public string RACF_STATUS_CODE { get; set; }
        //public string BU_STATUS_CODE { get; set; }
        //public string ACU_STATUS_CODE { get; set; }
        //public string NO_OF_LOGIN_ATTEMPTS { get; set; }
        //public string ENABLED_IND { get; set; }
        //public string PWD_CREATE_DATE { get; set; }
        public string PASSWORD { get; set; }
        //public string RACF_PREVIOUS_PWD { get; set; }
        //public string GBASE_PREVIOUS_PWD { get; set; }
    }


    public class FXGIDStatus
    {
        public string REF_NO { get; set; }
        public string optDate { get; set; }
        public string AUTHORIZE_BY { get; set; }
    }

    public class MMGIDStatus
    {
        public int TicketId { get; set; }
        public string optDate { get; set; }
        public string AUTHORIZE_BY { get; set; }
    }


    public class EucBalanceAttribute
    {
        public string GBaseReferenceNo { get; set; }
        public string AUTHORIZE_BY { get; set; }
    }

    public class EDocumentAttribute
    {
        public int Id { get; set; }
        public string ReportType { get; set; }
        //public string AccountNo { get; set; }
        public string AUTHORIZE_BY { get; set; }
    }

    public class RateAttribute
    {
        public string CcyCode { get; set; }
        public decimal Rate { get; set; }
        public string AUTHORIZE_BY { get; set; }
    }

    public class SwiftPaymentStatus
    {
        public int PaymentId { get; set; }
        public int SwiftPaymentId { get; set; }
        public int StatusId { get; set; }
        public string AUTHORIZE_BY { get; set; }
    }


    public class OnDemandBatchAttribute
    {
        public int BatchId { get; set; }
        public int BatchNo { get; set; }
        //public string Started { get; set; }
        //public string Completed { get; set; }
        //public string Errored { get; set; }
        //public string IsRec { get; set; }
        //public string WhoRequested { get; set; }
        //public string CreatedWhen { get; set; }
        //public string UpdatedWhen { get; set; }
        public string AUTHORIZE_BY { get; set; }
    }

    public class GASAttribute
    {
        public string Min_No { get; set; }
        public string HasDCSPayment { get; set; }
        public string Authorize_By { get; set; }
        public string valueDate { get; set; }
    }

}
