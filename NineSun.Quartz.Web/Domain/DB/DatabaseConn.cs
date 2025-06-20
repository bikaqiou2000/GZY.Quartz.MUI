using System.Data;
using Dapper;
using Microsoft.AspNetCore.Authentication;
using MySqlConnector;
using NineSun.Quartz.Web.Domain.Conf;

namespace NineSun.Quartz.Web.Domain.DB
{
    public class DatabaseConn
    {
        public const string NINESUN = "ninesun";
        public const string QUARTZ = "quartz";

        private List<DbConfigItem> _confs;

        public DatabaseConn(List<DbConfigItem> confs)
        {
            _confs = confs;
        }


        public MySqlConnection GetNineSunConn()
        {
            var conn = _confs.FirstOrDefault(x => x.Key == NINESUN);
            if (conn == null)
            {
                throw new ApplicationException($"未找到{NINESUN}数据库连接配置");
            }
            return new MySqlConnection(conn.ConnectionString);
        }

        public MySqlConnection GetQuartzConn()
        {
            var conn = _confs.FirstOrDefault(x => x.Key == QUARTZ);
            if (conn == null)
            {
                throw new ApplicationException($"未找到{QUARTZ}数据库连接配置");
            }
            return new MySqlConnection(conn.ConnectionString);
        }


    }



}
