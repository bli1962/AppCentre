using mhcb.syd.DataAccess;
using mhcb.syd.DataAccess.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mhcb.syd.Interface
{
    public interface IGASBusiness
    {
        IEnumerable<GASDebitView> getGASTransByDate(string optDate);

        string UpdateDCSStatus(GASAttribute gasAttribute);

        string UpdateGASStatus(GASAttribute gasAttribute);
    }
}
