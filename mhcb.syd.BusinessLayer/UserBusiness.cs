using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using mhcb.syd.DataAccess;
using mhcb.syd.DataAccess.View;
using mhcb.syd.Encryption;
using mhcb.syd.Interface;

namespace mhcb.syd.BusinessLayer
{
	public class UserBusiness : IUser
	{
		public IEnumerable<UserView> LoadUsersByRecStatus(string status)
		{
			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				var entry = (from b in entities.USERS
							.Where(e => e.REC_STATUS == status)
							 select new UserView()
							 {
								 USER_ID = b.USER_ID,
								 FIRST_NAME = b.FIRST_NAME,
								 LAST_NAME = b.LAST_NAME,
								 PWD_CHANGE_IND = b.PWD_CHANGE_IND,
								 RACF_STATUS_CODE = b.RACF_STATUS_CODE,
								 BU_STATUS_CODE = b.BU_STATUS_CODE,
								 ACU_STATUS_CODE = b.ACU_STATUS_CODE,
								 NO_OF_LOGIN_ATTEMPTS = b.NO_OF_LOGIN_ATTEMPTS,
								 ENABLED_IND = b.ENABLED_IND,
								 REC_STATUS = b.REC_STATUS,
								 GBASE_STATUS_CODE = b.GBASE_STATUS_CODE,
								 GBASE_PWD_BU_STATUS_CODE = b.GBASE_PWD_BU_STATUS_CODE,
								 PWD_CREATE_DATE = b.PWD_CREATE_DATE
							 })
							.ToList();
				return entry;
			}
		}

		public UserView LoadUserByUserId(string userId)
		{
			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				var entry = (from b in entities.USERS
								.Where(e => e.USER_ID == userId)
							 select new UserView()
							 {
								 USER_ID = b.USER_ID,
								 FIRST_NAME = b.FIRST_NAME,
								 LAST_NAME = b.LAST_NAME,
								 PWD_CHANGE_IND = b.PWD_CHANGE_IND,
								 RACF_STATUS_CODE = b.RACF_STATUS_CODE,
								 BU_STATUS_CODE = b.BU_STATUS_CODE,
								 ACU_STATUS_CODE = b.ACU_STATUS_CODE,
								 NO_OF_LOGIN_ATTEMPTS = b.NO_OF_LOGIN_ATTEMPTS,
								 ENABLED_IND = b.ENABLED_IND,
								 REC_STATUS = b.REC_STATUS,
								 GBASE_STATUS_CODE = b.GBASE_STATUS_CODE,
								 GBASE_PWD_BU_STATUS_CODE = b.GBASE_PWD_BU_STATUS_CODE,
								 PWD_CREATE_DATE = b.PWD_CREATE_DATE
							 })
							 .FirstOrDefault();
				return entry;
			}
		}

		public string updateStatus(UserAttribute userAttr)
		{
			try
			{
				using (GUIDEDBEntities entities = new GUIDEDBEntities())
				{
					var entry = entities.USERS
							 .Where(e => e.USER_ID == userAttr.USER_ID)
							 .FirstOrDefault();

					if (entry != null)
					{

						var v1 = entry.RACF_STATUS_CODE;
						var v2 = entry.BU_STATUS_CODE;
						var v3 = entry.ACU_STATUS_CODE;
						var v4 = entry.PWD_CHANGE_IND;
						var v5 = entry.NO_OF_LOGIN_ATTEMPTS;
						var v6 = entry.ENABLED_IND;

						var v7 = entry.GBASE_PWD_BU_STATUS_CODE;
						var v8 = entry.GBASE_PWD_ACU_STATUS_CODE;
						var v9 = entry.GBASE_PWD_STATUS_CODE;
						var v10 = entry.GBASE_STATUS_CODE;

						var v11 = entry.PWD_CREATE_DATE;
						var v12 = entry.GBASE_UPLOADED_DATE;

						if (!string.IsNullOrEmpty(userAttr.PASSWORD))
						{
							var enccPassword1 = Krypting.Encrypt(userAttr.PASSWORD.ToUpper());
							var enccPassword2 = Krypting.Encrypt(userAttr.PASSWORD.ToUpper());
							var enccPassword3 = Krypting.Encrypt(userAttr.PASSWORD.ToUpper());

							entry.PASSWORD = enccPassword1;
							entry.RACF_PREVIOUS_PWD = enccPassword2;
							entry.GBASE_PREVIOUS_PWD = enccPassword3;
						}


						//AUDIT_LOG auditLog = new AUDIT_LOG
						//{
						//    USER_ID = userAttr.USER_ID,
						//    AUDIT_DATE = DateTime.Now,
						//    REF_NO = userAttr.USER_ID,
						//    TRANS_NO = "",
						//    AUDIT_DESC = "Reset GUIDE user profile status",
						//    APP_NAME = "GUIDE",
						//    TABLE_NAME = "Users",
						//    ORIGINAL_VALUE = "RACF_STATUS_CODE: " + v1 + ", BU_STATUS_CODE :" + v2 + ", ACU_STATUS_CODE: " + v3 +
						//", PWD_CHANGE_IND: " + v4 + ",NO_OF_LOGIN_ATTEMPTS: " + v5,
						//    NEW_VALUE = "N:MS:MS:MS:0",
						//};

						if (v1 != "MS")
						{
							AUDIT_LOG auditLog = new AUDIT_LOG
							{
								USER_ID = userAttr.USER_ID,
								AUDIT_DATE = DateTime.Now,
								REF_NO = userAttr.USER_ID,
								TRANS_NO = "",
								AUDIT_DESC = "Reset GUIDE user profile status",
								APP_NAME = "GUIDE",
								TABLE_NAME = "Users",
								ORIGINAL_VALUE = "RACF_STATUS_CODE: " + v1,
								NEW_VALUE = "MS",
							};
							entry.RACF_STATUS_CODE = "MS";
							AuditLog.InsertAuditLog(auditLog);
						}

						if (v2 != "MS")
						{
							AUDIT_LOG auditLog = new AUDIT_LOG
							{
								USER_ID = userAttr.USER_ID,
								AUDIT_DATE = DateTime.Now,
								REF_NO = userAttr.USER_ID,
								TRANS_NO = "",
								AUDIT_DESC = "Reset GUIDE user profile status",
								APP_NAME = "GUIDE",
								TABLE_NAME = "Users",
								ORIGINAL_VALUE = "BU_STATUS_CODE: " + v2,
								NEW_VALUE = "MS",
							};
							entry.BU_STATUS_CODE = "MS";
							AuditLog.InsertAuditLog(auditLog);
						}

						if (v3 != "MS")
						{
							AUDIT_LOG auditLog = new AUDIT_LOG
							{
								USER_ID = userAttr.USER_ID,
								AUDIT_DATE = DateTime.Now,
								REF_NO = userAttr.USER_ID,
								TRANS_NO = "",
								AUDIT_DESC = "Reset GUIDE user profile status",
								APP_NAME = "GUIDE",
								TABLE_NAME = "Users",
								ORIGINAL_VALUE = "ACU_STATUS_CODE: " + v3,
								NEW_VALUE = "MS",
							};
							entry.ACU_STATUS_CODE = "MS";
							AuditLog.InsertAuditLog(auditLog);
						}

						if (v4 != "N")
						{
							AUDIT_LOG auditLog = new AUDIT_LOG
							{
								USER_ID = userAttr.USER_ID,
								AUDIT_DATE = DateTime.Now,
								REF_NO = userAttr.USER_ID,
								TRANS_NO = "",
								AUDIT_DESC = "Reset GUIDE user profile status",
								APP_NAME = "GUIDE",
								TABLE_NAME = "Users",
								ORIGINAL_VALUE = "PWD_CHANGE_IND: " + v4,
								NEW_VALUE = "N",
							};
							entry.PWD_CHANGE_IND = "N";
							AuditLog.InsertAuditLog(auditLog);
						}

						if (v5 != null)
						{
							AUDIT_LOG auditLog = new AUDIT_LOG
							{
								USER_ID = userAttr.USER_ID,
								AUDIT_DATE = DateTime.Now,
								REF_NO = userAttr.USER_ID,
								TRANS_NO = "",
								AUDIT_DESC = "Reset GUIDE user profile status",
								APP_NAME = "GUIDE",
								TABLE_NAME = "Users",
								ORIGINAL_VALUE = "NO_OF_LOGIN_ATTEMPTS: " + v5,
								NEW_VALUE = null,
							};
							entry.NO_OF_LOGIN_ATTEMPTS = null;
							AuditLog.InsertAuditLog(auditLog);
						}

						if (v6 != "")
						{
							AUDIT_LOG auditLog = new AUDIT_LOG
							{
								USER_ID = userAttr.USER_ID,
								AUDIT_DATE = DateTime.Now,
								REF_NO = userAttr.USER_ID,
								TRANS_NO = "",
								AUDIT_DESC = "Reset GUIDE user profile status",
								APP_NAME = "GUIDE",
								TABLE_NAME = "Users",
								ORIGINAL_VALUE = "ENABLED_IND: " + v6,
								NEW_VALUE = "",
							};
							entry.ENABLED_IND = "";
							AuditLog.InsertAuditLog(auditLog);
						}


						if (v7 != "U")
						{
							AUDIT_LOG auditLog = new AUDIT_LOG
							{
								USER_ID = userAttr.USER_ID,
								AUDIT_DATE = DateTime.Now,
								REF_NO = userAttr.USER_ID,
								TRANS_NO = "",
								AUDIT_DESC = "Reset GUIDE user profile status",
								APP_NAME = "GUIDE",
								TABLE_NAME = "Users",
								ORIGINAL_VALUE = "GBASE_PWD_BU_STATUS_CODE: " + v7,
								NEW_VALUE = "U",
							};
							entry.GBASE_PWD_BU_STATUS_CODE = "U";
							AuditLog.InsertAuditLog(auditLog);
						}

						if (v8 != "U")
						{
							AUDIT_LOG auditLog = new AUDIT_LOG
							{
								USER_ID = userAttr.USER_ID,
								AUDIT_DATE = DateTime.Now,
								REF_NO = userAttr.USER_ID,
								TRANS_NO = "",
								AUDIT_DESC = "Reset GUIDE user profile status",
								APP_NAME = "GUIDE",
								TABLE_NAME = "Users",
								ORIGINAL_VALUE = "GBASE_PWD_ACU_STATUS_CODE: " + v8,
								NEW_VALUE = "U",
							};
							entry.GBASE_PWD_ACU_STATUS_CODE = "U";
							AuditLog.InsertAuditLog(auditLog);
						}

						if (v9 != "U")
						{
							AUDIT_LOG auditLog = new AUDIT_LOG
							{
								USER_ID = userAttr.USER_ID,
								AUDIT_DATE = DateTime.Now,
								REF_NO = userAttr.USER_ID,
								TRANS_NO = "",
								AUDIT_DESC = "Reset GUIDE user profile status",
								APP_NAME = "GUIDE",
								TABLE_NAME = "Users",
								ORIGINAL_VALUE = "GBASE_PWD_STATUS_CODE: " + v9,
								NEW_VALUE = "U",
							};
							entry.GBASE_PWD_STATUS_CODE = "U";
							AuditLog.InsertAuditLog(auditLog);
						}

						if (v10 != "U")
						{
							AUDIT_LOG auditLog = new AUDIT_LOG
							{
								USER_ID = userAttr.USER_ID,
								AUDIT_DATE = DateTime.Now,
								REF_NO = userAttr.USER_ID,
								TRANS_NO = "",
								AUDIT_DESC = "Reset GUIDE user profile status",
								APP_NAME = "GUIDE",
								TABLE_NAME = "Users",
								ORIGINAL_VALUE = "GBASE_STATUS_CODE: " + v10,
								NEW_VALUE = "U",
							};
							entry.GBASE_STATUS_CODE = "U";
							AuditLog.InsertAuditLog(auditLog);
						}


						AUDIT_LOG auditLog1 = new AUDIT_LOG
						{
							USER_ID = userAttr.USER_ID,
							AUDIT_DATE = DateTime.Now,
							REF_NO = userAttr.USER_ID,
							TRANS_NO = "",
							AUDIT_DESC = "Reset GUIDE user profile status",
							APP_NAME = "GUIDE",
							TABLE_NAME = "Users",
							ORIGINAL_VALUE = "PWD_CREATE_DATE: " + v11,
							NEW_VALUE = DateTime.Today.AddDays(-27).ToString(),
						};
						entry.PWD_CREATE_DATE = DateTime.Today.AddDays(-27);
						AuditLog.InsertAuditLog(auditLog1);


						AUDIT_LOG auditLog2 = new AUDIT_LOG
						{
							USER_ID = userAttr.USER_ID,
							AUDIT_DATE = DateTime.Now,
							REF_NO = userAttr.USER_ID,
							TRANS_NO = "",
							AUDIT_DESC = "Reset GUIDE user profile status",
							APP_NAME = "GUIDE",
							TABLE_NAME = "Users",
							ORIGINAL_VALUE = "GBASE_UPLOADED_DATE: " + v12,
							NEW_VALUE = DateTime.Today.AddDays(-27).ToString(),
						};
						entry.GBASE_UPLOADED_DATE = DateTime.Today.AddDays(-27);
						AuditLog.InsertAuditLog(auditLog2);

						entities.SaveChanges();
						//AuditLog.InsertAuditLog(auditLog);
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
