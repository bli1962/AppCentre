using System;
using System.Linq;
using mhcb.syd.DataAccess;
using mhcb.syd.Encryption;
using mhcb.syd.Interface;

namespace mhcb.syd.BusinessLayer
{
	public class UsersSecurity : IUsersSecurity
	{
		//static string DecryptPassword(string encPassword)
		//{
		//    Encryption o = new Encryption();
		//    return o.Decrypt(encPassword).ToString();
		//}

		//static string EncryptPassword(string decPassword)
		//{
		//    Encryption o = new Encryption();
		//    return o.Encrypt(decPassword).ToString();
		//}

		public USER GetGUser(string userid, string decPassword)
		{
			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				var queryUser = entities.USERS.Where(x => x.USER_ID.Equals(userid, StringComparison.OrdinalIgnoreCase))
								.Select(s => s.PASSWORD)
								.FirstOrDefault();

				if (queryUser != null)
				{
					var queryPW = entities.USERS.FirstOrDefault(x =>
								x.USER_ID.Equals(userid, StringComparison.OrdinalIgnoreCase)).PASSWORD.ToString();

					var decPassword2 = Krypting.Decrypt(queryPW);

					if (decPassword == decPassword2)
					{
						USER user = entities.USERS.Where(x => x.USER_ID.Equals(userid, StringComparison.OrdinalIgnoreCase))
									.FirstOrDefault();
						return user;
					}
					else
					{
						return null;
					}
				}
				return null;

			}

		}

		public bool Login(string userid, string decPassword)
		{
			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				var queryUser = entities.USERS.Where(x => x.USER_ID.Equals(userid, StringComparison.OrdinalIgnoreCase))
							.Select(s => s.PASSWORD)
							.FirstOrDefault();

				if (queryUser != null)
				{
					var queryPW = entities.USERS.FirstOrDefault(x =>
								x.USER_ID.Equals(userid, StringComparison.OrdinalIgnoreCase)).PASSWORD.ToString();

					var decPassword2 = Krypting.Decrypt(queryPW);

					if (decPassword == decPassword2)
					{
						return entities.USERS.Any(x =>
							x.USER_ID.Equals(userid, StringComparison.OrdinalIgnoreCase) &&
							x.PASSWORD == queryUser);
					}
					else
					{
						return false;
					}
				}
				return false;
			}
		}

		public string GetAuthorisers(string userid)
		{
			using (GUIDEDBEntities entities = new GUIDEDBEntities())
			{
				var queryPermission = (from b in entities.USER_SECURITY_GROUPS
									   join c in entities.SECURITY_GROUPS on b.SG_IDENTIFIER equals c.IDENTIFIER
									   where (b.USER_ID == userid) &&
										(
											c.NAME.Contains("BIF - Authorise") ||
											c.NAME.Contains("SIF - Authorise") ||
											c.NAME.Contains("FX-Trans - Authorise") ||
											c.NAME.Contains("FX-Delivery - Author") ||
											c.NAME.Contains("Customer - Authorise")
										)
									   select c.NAME
									   )
									   .ToList();

				var p = "";
				if (queryPermission != null)
				{
					p = string.Join(";", queryPermission);
				}
				return p;
			}
		}

	}

}