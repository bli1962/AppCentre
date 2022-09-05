using mhcb.syd.DataAccess.View;
using System.Collections.Generic;

namespace mhcb.syd.Interface
{
	public interface IUser
	{
		UserView LoadUserByUserId(string userId);
		IEnumerable<UserView> LoadUsersByRecStatus(string status);
		string updateStatus(UserAttribute userAttr);
	}
}