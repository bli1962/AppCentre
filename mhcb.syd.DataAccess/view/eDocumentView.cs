using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mhcb.syd.DataAccess.View
{
    public class eDocumentView
    {

        public int Id { get; set; }
        public string ReportType { get; set; }
        public string ReportFileName { get; set; }
        //public bool Processed { get; set; }
        public string Processed { get; set; }
        public string DateProcessed { get; set; }
        //public bool Errored { get; set; }
        public string Errored { get; set; }
        public string ErrorMessage { get; set; }

        public string CompanyName { get; set; }
        public string AccountNo { get; set; }
        public string ReferenceNumber { get; set; }
        public string AccountType { get; set; }

    }
}
