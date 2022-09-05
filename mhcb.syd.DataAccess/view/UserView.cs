using System;

namespace mhcb.syd.DataAccess.View
{
    public class UserView
    {
        public string USER_ID { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string PWD_CHANGE_IND { get; set; }
        public string RACF_STATUS_CODE { get; set; }
        public string BU_STATUS_CODE { get; set; }
        public string ACU_STATUS_CODE { get; set; }
        public Nullable<int> NO_OF_LOGIN_ATTEMPTS { get; set; }
        public string ENABLED_IND { get; set; }
        public string REC_STATUS { get; set; }
        public string GBASE_STATUS_CODE { get; set; }
        public string GBASE_PWD_BU_STATUS_CODE { get; set; }
        public System.DateTime PWD_CREATE_DATE { get; set; }
    }
}
