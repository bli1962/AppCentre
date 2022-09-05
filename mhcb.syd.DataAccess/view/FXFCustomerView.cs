using System;

namespace mhcb.syd.DataAccess.View
{
    public class FXFCustomerView
    {
        public string SHORTNAME { get; set; }
        public string Abbreviation { get; set; }
        public string DeptCode { get; set; }
        public string TaxCode { get; set; }
        public string IBOSNO { get; set; }
        public string Location { get; set; }
        public Nullable<int> Active { get; set; }
        public string STATUS { get; set; }
        public string DELETION_STATUS { get; set; }
        public string CCIF { get; set; }
        public string Category { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
    }
}
