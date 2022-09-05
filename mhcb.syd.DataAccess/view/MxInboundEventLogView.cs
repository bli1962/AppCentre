namespace mhcb.syd.DataAccess.View
{
    public class MxInboundEventLogView
    {
        public int MessageId{ get; set; }
        public string SendingSystem{ get; set; }
        public string Category{ get; set; }
        public string EventType{ get; set; }
        public string  StatusName{ get; set; }
        //public string  GBaseReferenceNo{ get; set; }
        //public string  CustomerAbbreviation{ get; set; }
        //public string  BatchNo{ get; set; }
        public int LogId{ get; set; }
        public string Logger{ get; set; }
        public string Description{ get; set; }
        public string Status{ get; set; }

    }
}
