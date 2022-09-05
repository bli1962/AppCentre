using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using mhcb.syd.DataAccess;
using mhcb.syd.DataAccess.View;
using mhcb.syd.Interface;

namespace mhcb.syd.BusinessLayer
{
    public class GASBusiness : IGASBusiness
    {
        public IEnumerable<GASDebitView> getGASTransByDate(string optDate)
        {
            using (GUIDEDBEntities entities = new GUIDEDBEntities())
            {
                DateTime date = DateTime.Parse(optDate);

                // not working due to reference object 
                //var entry1 = entities.GAS_GO_DEBIT
                //                    .Where(e => DbFunctions.TruncateTime(e.VALUEDATE_DT) == DbFunctions.TruncateTime(date))
                //                    .OrderBy(e => e.MIN_NO)
                //                    //.Take(50)                             
                //                    .ToList();


                var entry = (from g in entities.GAS_GO_DEBIT
                              where DbFunctions.TruncateTime(g.VALUEDATE_DT) == DbFunctions.TruncateTime(date)
                              orderby g.MIN_NO
                              select new GASDebitView()
                              {
                                  MIN_NO = g.MIN_NO,
                                  VALUEDATE_DT = g.VALUEDATE_DT,
                                  STATUS_INT = g.STATUS_INT,
                                  DREAMS_REF_CODE = g.DREAMS_REF_CODE,
                                  REMARKS_TX = g.REMARKS_TX,
                                  CCY_TX = g.CCY_TX,
                                  AMOUNT_AMT = g.AMOUNT_AMT,
                                  CUSTID_TX = g.CUSTID_TX,
                                  ACC_CD_TX = g.ACC_CD_TX,
                                  ACCNUM_TX = g.ACCNUM_TX,
                                  DESCRIPTION_TX = g.DESCRIPTION_TX,
                                  CREATED_BY = g.CREATED_BY,
                                  CREATED_DATE = g.CREATED_DATE,
                                  UPLOADED_BY = g.UPLOADED_BY,
                                  UPLOADED_DATE = g.UPLOADED_DATE,
                                  TRANSNO_TX = g.TRANSNO_TX
                              })
                            .ToList();

                return entry;
            }
        }


        public string UpdateDCSStatus(GASAttribute gasAttribute)
        {
            try
            {
                using (GUIDEDBEntities entities = new GUIDEDBEntities())
                {
                    string gBaseRefNo;
                    string preGASStatus;
                    string prePosingStatus;
                    string prePaymentStatus;
                    int LastGOPostingId;
                    int PaymentId;

                    DateTime valueDate = DateTime.Parse(gasAttribute.valueDate);

                    var GAS_Debit = entities.GAS_GO_DEBIT
                                .Where(e => e.MIN_NO.ToLower() == gasAttribute.Min_No.ToLower()
                                    && DbFunctions.TruncateTime(e.VALUEDATE_DT) == DbFunctions.TruncateTime(valueDate))
                                .FirstOrDefault();

                    if (GAS_Debit != null)
                    {
                        gBaseRefNo = GAS_Debit.DREAMS_REF_CODE;
                        preGASStatus = GAS_Debit.STATUS_INT.ToString();

                        var GASPosting = entities.DomesticClearing_GOPosting
                                    .Where(e => e.GBaseRefNo.ToLower() == gBaseRefNo
                                                && e.GasDescription.ToLower() != "Approved".ToLower()
                                                && e.StatusId != 9
                                           )
                                    .OrderByDescending(x => x.GOPostingId)
                                    .FirstOrDefault();

                        if (GASPosting != null)
                        {
                            LastGOPostingId = GASPosting.GOPostingId;
                            prePosingStatus = GASPosting.StatusId.ToString();

                            var GASPayment = entities.DomesticClearing_Payment
                                    .Where(e => e.MainPostId == LastGOPostingId)
                                    .OrderByDescending(x => x.MainPostId)
                                    .FirstOrDefault();

                            if (GASPayment != null)
                            {
                                PaymentId = GASPayment.PaymentId;
                                prePaymentStatus = GASPayment.StatusId.ToString();

                                var updateGASPosting = entities.DomesticClearing_GOPosting
                                    .Where(e => e.GOPostingId == LastGOPostingId)
                                    .FirstOrDefault();
                                updateGASPosting.StatusId = 1;


                                var updateGASDebit = entities.GAS_GO_DEBIT
                                    .Where(e => e.MIN_NO == gasAttribute.Min_No)
                                    .FirstOrDefault();   
                            
                                updateGASDebit.STATUS_INT = 10;
                                updateGASDebit.DESCRIPTION_TX = "Approved";


                                AUDIT_LOG auditLog1 = new AUDIT_LOG
                                {
                                    USER_ID = gasAttribute.Authorize_By,
                                    AUDIT_DATE = DateTime.Now,
                                    REF_NO = gasAttribute.Min_No,
                                    TRANS_NO = PaymentId.ToString(),

                                    AUDIT_DESC = "Reset DomesticClearing_GOPosting",
                                    APP_NAME = "GUIDE",
                                    TABLE_NAME = "DomesticClearing_GOPosting",
                                    ORIGINAL_VALUE = prePosingStatus,
                                    NEW_VALUE = "1",
                                };
                                AuditLog.InsertAuditLog(auditLog1);

                                AUDIT_LOG auditLog2 = new AUDIT_LOG
                                {
                                    USER_ID = gasAttribute.Authorize_By,
                                    AUDIT_DATE = DateTime.Now,
                                    REF_NO = gasAttribute.Min_No,
                                    TRANS_NO = gasAttribute.Min_No,

                                    AUDIT_DESC = "Reset GAS Transaction",
                                    APP_NAME = "GUIDE",
                                    TABLE_NAME = "GAS_GO_DEBIT",
                                    ORIGINAL_VALUE = preGASStatus,
                                    NEW_VALUE = "10",
                                };
                                AuditLog.InsertAuditLog(auditLog2);

                                entities.SaveChanges();
                                return "OK";
                            }
                            else
                            {
                                return "NotFound";
                            }
                        }
                        else
                        {
                            return "NotFound";
                        }
                    }
                    else
                    {
                        return "NotFound";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


        public string UpdateGASStatus(GASAttribute gasAttribute)
        {
            try
            {
                using (GUIDEDBEntities entities = new GUIDEDBEntities())
                {
                    string gBeRefNo;
                    string preGASStatus;
                    DateTime valueDate = DateTime.Parse(gasAttribute.valueDate);

                    var GAS_Debit = entities.GAS_GO_DEBIT
                                .Where(e => e.MIN_NO.ToLower() == gasAttribute.Min_No.ToLower()
                                    && DbFunctions.TruncateTime(e.VALUEDATE_DT) == DbFunctions.TruncateTime(valueDate))
                                .FirstOrDefault();


                    if (GAS_Debit != null)
                    {
                        gBeRefNo = GAS_Debit.DREAMS_REF_CODE;
                        preGASStatus = GAS_Debit.STATUS_INT.ToString();

                        GAS_Debit.DESCRIPTION_TX = "Approved";
                        GAS_Debit.STATUS_INT = 8;                       

                        AUDIT_LOG auditLog1 = new AUDIT_LOG
                        {
                            USER_ID = gasAttribute.Authorize_By,
                            AUDIT_DATE = DateTime.Now,
                            REF_NO = gasAttribute.Min_No,
                            TRANS_NO = gasAttribute.Min_No,

                            AUDIT_DESC = "Reset GAS Transaction",
                            APP_NAME = "GUIDE",
                            TABLE_NAME = "GAS_GO_DEBIT",
                            ORIGINAL_VALUE = preGASStatus,
                            NEW_VALUE = "8",
                        };

                        AuditLog.InsertAuditLog(auditLog1);

                        entities.SaveChanges();
                        return "OK";
                    }
                    else
                    {
                        return "NotFound";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
