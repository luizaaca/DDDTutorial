using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Repository
{
    internal static class ConnectionFactory
    {
        internal static IDbConnection GetConnection()
        {
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DDDConnectionString"].ConnectionString);
            conn.Open();
            return conn;
        }
    }
}
