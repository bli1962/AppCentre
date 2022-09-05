using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using mhcb.syd.DataAccess.View;
using mhcb.syd.DataAccess;
using mhcb.syd.Interface;

namespace mhcb.syd.BusinessLayer
{
	public class FXFCustBusiness : IFXFCust
	{
		public IEnumerable<FXFCustomerView> LoadFXCustByCustAbbr(string abbreviation)
		{
			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				var entry = entities.FXFCusts
									.Where(e => e.Abbreviation == abbreviation)
									.Select(x => new FXFCustomerView()
									{
										SHORTNAME = x.SHORTNAME,
										Abbreviation = x.Abbreviation,
										DeptCode = x.DeptCode,
										TaxCode = x.TaxCode,
										IBOSNO = x.IBOSNO,
										Location = x.Location,
										Active = x.Active,
										STATUS = x.STATUS,
										DELETION_STATUS = x.DELETION_STATUS,
										CCIF = x.CCIF,
										Category = x.Category,
										Email = x.Email,
										Country = x.Country
									})
									.ToList();
				return entry;
			}
		}

		public string updateStatus(FXFCustAttribute custAttr)
		{
			try
			{
				using (GUIDEDBEntities entities = new GUIDEDBEntities())
				{
					var fxfCust = entities.FXFCusts
							.FirstOrDefault(e => e.Abbreviation == custAttr.Abbreviation);

					// must check the customer exists
					if (fxfCust != null)
					{
						var taxCode = fxfCust.TaxCode;
						var deptCode = fxfCust.DeptCode;
						var statusCode = fxfCust.STATUS;

						AUDIT_LOG auditLogTaxCode = new AUDIT_LOG
						{
							USER_ID = custAttr.AUTHORIZE_BY,
							AUDIT_DATE = DateTime.Now,
							REF_NO = custAttr.Abbreviation.ToString(),
							TRANS_NO = "",
							AUDIT_DESC = "Update FX Customer Tax Code",
							APP_NAME = "GUIDE",
							TABLE_NAME = "FXCustomer",
							ORIGINAL_VALUE = taxCode,
							//NEW_VALUE = status.GIP_STATUS,
						};

						AUDIT_LOG auditLogDeptCode = new AUDIT_LOG
						{
							USER_ID = custAttr.AUTHORIZE_BY,
							AUDIT_DATE = DateTime.Now,
							REF_NO = custAttr.Abbreviation.ToString(),
							TRANS_NO = "",
							AUDIT_DESC = "Update FX Customer Department Code",
							APP_NAME = "GUIDE",
							TABLE_NAME = "FXCustomer",
							ORIGINAL_VALUE = deptCode,
							//NEW_VALUE = status.GIP_STATUS,
						};

						AUDIT_LOG auditLogStatus = new AUDIT_LOG
						{
							USER_ID = custAttr.AUTHORIZE_BY,
							AUDIT_DATE = DateTime.Now,
							REF_NO = custAttr.Abbreviation.ToString(),
							TRANS_NO = "",
							AUDIT_DESC = "Update FX Customer Finalised",
							APP_NAME = "GUIDE",
							TABLE_NAME = "FXCustomer",
							ORIGINAL_VALUE = statusCode,
							NEW_VALUE = custAttr.STATUS,
						};


						custAttr.TaxCode = custAttr.TaxCode ?? "";
						if (custAttr.TaxCode != "NA")
						{
							if (taxCode != custAttr.TaxCode)
							{
								fxfCust.TaxCode = custAttr.TaxCode;
								auditLogTaxCode.NEW_VALUE = custAttr.TaxCode;
							}
						}

						custAttr.DeptCode = custAttr.DeptCode ?? "";
						if (custAttr.DeptCode != "NA")
						{
							if (deptCode != custAttr.DeptCode)
							{
								fxfCust.DeptCode = custAttr.DeptCode;
								auditLogDeptCode.NEW_VALUE = custAttr.DeptCode;
							}
						}

						if (!string.IsNullOrEmpty(custAttr.STATUS))
						{
							if (statusCode != custAttr.STATUS)
							{
								fxfCust.STATUS = custAttr.STATUS;
								auditLogStatus.NEW_VALUE = custAttr.STATUS;
							}
						}

						// save whatever changes
						fxfCust.AUTHORIZE_BY = custAttr.AUTHORIZE_BY;
						entities.SaveChanges();

						// Add audit log if there is a change
						if (custAttr.TaxCode != "NA")
						{
							if (taxCode != custAttr.TaxCode)
							{
								AuditLog.InsertAuditLog(auditLogTaxCode);
							}
						}


						// Add audit log if there is a change
						if (custAttr.DeptCode != "NA")
						{
							if (deptCode != custAttr.DeptCode)
							{
								AuditLog.InsertAuditLog(auditLogDeptCode);
							}
						}

						// Add audit log if there is a change
						if (!string.IsNullOrEmpty(custAttr.STATUS))
						{
							if (statusCode != custAttr.STATUS)
							{
								AuditLog.InsertAuditLog(auditLogStatus);
							}
						}

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
