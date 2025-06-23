using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using NineSun.Quartz.Web.Domain.DB;
using Dapper;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Caching.Memory;
using NineSun.Quartz.Web.Domain.Conf;

namespace NineSun.Quartz.Web.Controllers
{

    /// <summary>
    ///基本测试
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<TestController> _logger;
        private readonly DatabaseConn _conns ;
        private readonly IMemoryCache _cache;

        public TestController(ILogger<TestController> logger, DatabaseConn conns, IMemoryCache cache)
        {
            _logger = logger;
            _conns = conns;
            _cache = cache;
        }

        /// <summary>
        /// 天气
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> GetWeatherForecast()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        /// <summary>
        /// 获取Mysql数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<dynamic> GetMysqlData()
        {
            using (var conn = _conns.GetNineSunConn())
            {
                var sql = "select * from cod_dw ";
                //throw new ApplicationException("test error");
                // var dt = conn.QueryTable(sql, null);
                var dat = conn.Query(sql, null);
                return dat;
            }
        }


        /// <summary>
        /// 获取Mysql数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public DataTable GetMysqlData2()
        {
            using (var conn = _conns.GetNineSunConn())
            {
                var sql = "select * from cod_dw ";
                //throw new ApplicationException("test error");
                var dat = conn.QueryTable(sql, null);
                //var dat = conn.Query(sql, null);
                return dat;
            }
        }


        /// <summary>
        /// 获取code 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetCode()
        {
            var dat = _cache.Get<string>(ChanjetConfig.Code_Key);
            return dat;
        }
    }


    public class WeatherForecast
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}
