using mhcb.syd.DataAccess;

namespace mhcb.syd.Interface
{
	public interface IUsersSecurity
	{
		string GetAuthorisers(string userid);
		USER GetGUser(string userid, string decPassword);
		bool Login(string userid, string decPassword);
	}
}