using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using NineSun.Quartz.Web.Domain.Chanjet;
using NineSun.Quartz.Web.Domain.Conf;

namespace NineSun.Quartz.Web.Controllers
{

    /// <summary>  
    /// 畅捷通消息接收控制器  
    /// </summary>  
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {

        private ILogger<MessageController> _logger;
        private IMemoryCache _cache;
        public MessageController(ILogger<MessageController> logger,IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        [HttpGet]
        public string OAuth(string code, string state)
        {
            _logger.LogInformation($"Code:{code}");
            _cache.Set(ChanjetConfig.Code_Key, code);
            return code;
        }


        [HttpPost]
        [Route("Receive")]
        public dynamic Receive([FromBody] ChanjetEncryptMsg encryptMsg)
        {
            string enMsg = encryptMsg.GetEncryptMsg();

            _logger.LogInformation($"解密前的消息{enMsg}");
            String decryptMsg = OpenapiCryptHelper.AesDecrypt(enMsg, ChanjetConfig.Key_encryptKey);

            _logger.LogInformation($"解密后消息{decryptMsg}");
            MessageBase message = JsonConvert.DeserializeObject<MessageBase>(decryptMsg);
            object retObj = null;
            try
            {
                switch (message.msgType)
                {
                    case "APP_TEST":
                        retObj = DealTestMsg(message);
                        break;
                    case "APP_TICKET":
                        retObj = DealTicketMsg(message);
                        break;
                    case "TEMP_AUTH_CODE":
                        retObj = DealOrgTempAuthMsg(message);
                        break;
                    case "PAY_ORDER_SUCCESS":
                        retObj = DealOrderPayMsg(message);
                        break;
                    default:
                        retObj = DealBussnessMsg(message);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retObj;
        }

        private object DealOrderPayMsg(MessageBase message)
        {
            return ReceiveMsgOK();
        }

        private object DealOrgTempAuthMsg(MessageBase message)
        {
            OrgTempAuthContent content = JsonConvert.DeserializeObject<OrgTempAuthContent>(message.bizContent.ToString());
            _logger.LogInformation($"OrgTempAuthCode:{content.tempAuthCode}");
            _cache.Set(ChanjetConfig.OrgPemAuth_Key, content.tempAuthCode);
            return ReceiveMsgOK();
        }

        private object DealTicketMsg(MessageBase message)
        {
            AppTicketContent content = JsonConvert.DeserializeObject<AppTicketContent>(message.bizContent.ToString());
            _logger.LogInformation($"AppTicket:{content.appTicket}");
            _cache.Set(ChanjetConfig.AppTick_Key, content.appTicket);
            return ReceiveMsgOK();
        }

        private object DealTestMsg(MessageBase message)
        {
            return ReceiveMsgOK();
        }

        private object DealBussnessMsg(MessageBase message)
        {
            return ReceiveMsgOK();
        }

        private object ReceiveMsgOK()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>
               {
                   { "result","success"}
               };

            return JsonConvert.SerializeObject(dic);
        }
    }
}
