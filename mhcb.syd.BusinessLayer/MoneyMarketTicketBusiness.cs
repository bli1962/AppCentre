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
	public class MoneyMarketTicketBusiness : IMoneyMarketTicket
	{
		public IEnumerable<MoneyMarketTicketView> GetPendingTrans()
		{
			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				string[] validStatus = { "F", "A", "V" };
				DateTime date = DateTime.Now;
				DateTime defaultDatetime = DateTime.Parse("1900-01-01 00:00:00");

				var entry = (from b in entities.MoneyMarketTickets
							 .Where(e => e.PrintStatus == true
									&& e.GIDUpload == true
									&& DbFunctions.TruncateTime(e.TicketCreatedTimestamp) == DbFunctions.TruncateTime(date)
							 //&& e.GIDUpload == true       )                            
							 ).OrderByDescending(e => e.TicketCreatedTimestamp)
							 select new MoneyMarketTicketView()
							 {
								 TicketId = b.MMTicketId,
								 REF_NO = b.GBaseRefNo,
								 Counterparty = b.CounterpartyShortName,
								 Event = b.Event,
								 Typology = b.Typology,
								 TradeId = b.MurexTradeId,

								 PortfolioGroup = b.PortfolioGroup,
								 PortfolioDesk = b.PortfolioDesk,
								 PortfolioBook = b.PortfolioBook,
								 Currency = b.Currency,
								 PrincipalAmount = b.PrincipalAmount,

								 InputBy = b.FrontOfficeInputBy,
								 AuthorisedBy = b.FrontOfficeAuthorisedBy,

								 PrintStatus = b.PrintStatus ? "Y" : "N",
								 PrintDateTime = (DbFunctions.TruncateTime(b.TicketCreatedTimestamp) == DbFunctions.TruncateTime(defaultDatetime)) ? "" : (DbFunctions.TruncateTime(b.TicketCreatedTimestamp).ToString()).Substring(0, 10),

								 GIDUpload = b.GIDUpload ? "Ture" : "False",
							 })

							.ToList();
				return entry;
			}
		}

		public IEnumerable<MoneyMarketReportView> GetMoneyMarketTransactionByDates(string strDateFrom, string strDateTo)
		{
			if (!(strDateFrom != null && strDateTo != null)) return new List<MoneyMarketReportView>();

			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				string[] validStatus = { "F", "A", "V" };

				DateTime date = DateTime.Now;
				DateTime dateFrom = DateTime.Parse(strDateFrom);
				DateTime dateTo = DateTime.Parse(strDateTo);
				DateTime defaultDatetime = DateTime.Parse("1900-01-01 00:00:00");

				var entry = (from mm in entities.MoneyMarketTickets
							 join cm in entities.CUSTOMER_MASTER on mm.CounterpartyAbbreviation equals cm.CUST_ABBR
							 join fc in entities.FXFCusts on cm.CUST_CODE equals fc.CODE
							 join mxCust in entities.MxCustCounterpartyTypes on fc.CUST_ID equals mxCust.CustID
							 join mxType in entities.MxCounterpartyTypes on mxCust.CounterpartyTypeID equals mxType.ID

							 where DbFunctions.TruncateTime(mm.OperationDate) >= DbFunctions.TruncateTime(dateFrom)
								&& DbFunctions.TruncateTime(mm.OperationDate) <= DbFunctions.TruncateTime(dateTo)
								//&& (mxType.ID == 1 || mxType.ID == 14)
								&& mm.PrintStatus == true
								&& mm.GIDUpload == true
							 orderby mm.Typology, mm.GBaseRefNo, mm.ContractVersion

							 select new MoneyMarketReportView()
							 {
								 GBaseRefNo = mm.GBaseRefNo,

								 CounterpartyAbbreviation = mm.CounterpartyAbbreviation,
								 CounterpartyShortName = mm.CounterpartyShortName,
								 TicketTemplate = mm.TicketTemplate,

								 Event = mm.Event,
								 Typology = mm.Typology,

								 PortfolioDesk = mm.PortfolioDesk,
								 PortfolioBook = mm.PortfolioBook,

								 ContractVersion = mm.ContractVersion,

								 OperationDate = (DbFunctions.TruncateTime(mm.OperationDate) == DbFunctions.TruncateTime(defaultDatetime)) ? "" : (DbFunctions.TruncateTime(mm.OperationDate).ToString()).Substring(0, 10),

								 ContractDate = (DbFunctions.TruncateTime(mm.ContractDate) == DbFunctions.TruncateTime(defaultDatetime)) ? "" : (DbFunctions.TruncateTime(mm.ContractDate).ToString()).Substring(0, 10),

								 ValueDate = (DbFunctions.TruncateTime(mm.ValueDate) == DbFunctions.TruncateTime(defaultDatetime)) ? "" : (DbFunctions.TruncateTime(mm.ValueDate).ToString()).Substring(0, 10),

								 FinalDueDate = (DbFunctions.TruncateTime(mm.FinalDueDate) == DbFunctions.TruncateTime(defaultDatetime)) ? "" : (DbFunctions.TruncateTime(mm.FinalDueDate).ToString()).Substring(0, 10),

								 DueDate = (DbFunctions.TruncateTime(mm.DueDate) == DbFunctions.TruncateTime(defaultDatetime)) ? "" : (DbFunctions.TruncateTime(mm.DueDate).ToString()).Substring(0, 10),

								 TradeDate = (DbFunctions.TruncateTime(mm.TradeDate) == DbFunctions.TruncateTime(defaultDatetime)) ? "" : (DbFunctions.TruncateTime(mm.TradeDate).ToString()).Substring(0, 10),

								 PaymentDate = (DbFunctions.TruncateTime(mm.PaymentDate) == DbFunctions.TruncateTime(defaultDatetime)) ? "" : (DbFunctions.TruncateTime(mm.PaymentDate).ToString()).Substring(0, 10),


								 Currency = mm.Currency,
								 PrincipalAmount = mm.PrincipalAmount,

								 InterestRate = mm.InterestRate,
								 InterestAmount = mm.InterestAmount,

								 InternalRate = mm.InternalRate,
								 SettlementDetails = mm.SettlementDetails,

								 //-- not for ('Call Depo', 'Call Loan') , 
								 //-- not for ('CD')
								 //-- but for Typology in ('Depo', 'Loan') 
								 NetInterest = mm.NetInterest,
								 TotalDue = mm.TotalDue,

								 //-- not for Typology in ('Depo', 'Loan') 
								 //-- not for ('CD')  
								 PrincipalAmountPrevious = mm.PrincipalAmountPrevious,
								 RolloverType = mm.RolloverType,
								 RolloverTypeLabel = mm.RolloverTypeLabel,
								 PrincipalAmountClosing = mm.PrincipalAmountClosing,
								 SettlementDirectionLabel = mm.SettlementDirectionLabel,

								 //-- For ('CD')  only
								 BuySell = mm.BuySell,

								 //-- not for ('Call Depo', 'Call Loan') , 
								 //-- not for ('Depo', 'Loan')  
								 Denomination = mm.Denomination,
								 Quantity = mm.Quantity,
								 FaceAmount = mm.FaceAmount,
								 ConsiderationAmount = mm.ConsiderationAmount,

								 TransactionStatus = mm.TransactionStatus,
								 DEPARTMENT_CD = cm.DEPARTMENT_CD,
							 })

							.ToList();
				return entry;
			}
		}

		public string updateStatus(MMGIDStatus status)
		{
			try
			{
				using (GUIDEDBEntities entities = new GUIDEDBEntities())
				{
					DateTime date = DateTime.Parse(status.optDate);

					var entry = entities.MoneyMarketTickets
						.Where(e => e.PrintStatus == true
								&& e.MMTicketId == status.TicketId
								&& DbFunctions.TruncateTime(e.TicketCreatedTimestamp) == DbFunctions.TruncateTime(date)
								//&& e.GIDUpload == true 
								)
						.FirstOrDefault();

					if (entry != null)
					{

						AUDIT_LOG auditLog = new AUDIT_LOG
						{
							USER_ID = status.AUTHORIZE_BY,
							AUDIT_DATE = DateTime.Now,
							REF_NO = status.TicketId.ToString(),
							TRANS_NO = "",
							AUDIT_DESC = "Reset MM GID Upload",
							APP_NAME = "GUIDE",
							TABLE_NAME = "MoneyMarketTicket",
							ORIGINAL_VALUE = entry.GIDUpload.ToString(),
							NEW_VALUE = "false",
						};

						entry.GIDUpload = false;
						entities.SaveChanges();
						AuditLog.InsertAuditLog(auditLog);

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
