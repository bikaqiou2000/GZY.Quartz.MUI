namespace NineSun.Quartz.Web.Domain.Chanjet
{
    public class MessageBase
    {
        public string id;
        public string appKey;
        public string msgType;
        public string time;
        public object bizContent;
    }

    public class AppTestContent
    {
        public string message;
    }

    public class AppTicketContent
    {
        public string appTicket;
    }

    public class OrgTempAuthContent
    {
        public string tempAuthCode;
        public string state;
    }

    public class OrderPayContent
    {
        public string orderNo;
        public string orgId;
    }
}
