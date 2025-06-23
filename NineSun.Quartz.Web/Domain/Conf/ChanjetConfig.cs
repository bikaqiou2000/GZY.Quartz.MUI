namespace NineSun.Quartz.Web.Domain.Conf
{
    public class ChanjetConfig
    {
        public const string Url_Server = "https://openapi.chanjet.com";
        public const string Key_encryptKey = "1234567890123456";

        public const string Url_getTokenbyTempCode = "/auth/getToken";
        public const string Url_getTokenbyPemCode = "/auth/token/getTokenByPermanentCode";

        //获取应用凭证
        public const string Url_getAppAccessToken = "/auth/appAuth/getAppAccessToken";
        //获取企业永久授权码
        public const string Url_getOrgPemAuthCode = "/auth/orgAuth/getPermanentAuthCode";
        //获取企业凭证
        public const string Url_getOrgAccessToken = "/auth/orgAuth/getOrgAccessToken";
        //立即触发appTicket推送
        public const string Url_getAppTicket = "/auth/appTicket/resend";

        //缓存key 前缀
        public const string ChatJet_Key_Prefix = "chatjet:";
        //缓存key 
        public const string Code_Key = $"{ChatJet_Key_Prefix}code";
        public const string AppTick_Key = $"{ChatJet_Key_Prefix}apptick";
        public const string OrgPemAuth_Key = $"{ChatJet_Key_Prefix}orgpemauth";

    }
}
