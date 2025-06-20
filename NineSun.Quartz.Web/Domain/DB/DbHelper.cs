using System.Data;
using Dapper;

namespace NineSun.Quartz.Web.Domain.DB
{
    public static class DbHelper
    {
        public static DataTable QueryTable(this IDbConnection conn, string sql, object args)
        {

            using (var rd = conn.ExecuteReader(sql, args))
            {
                var dt = new DataTable();
                dt.Load(rd);
                return dt;
            }
        }
    }
}
