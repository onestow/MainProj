using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTools
{

    public class DbHelper
    {
        public static DataTable Query(string sql)
        {
            using(var conn = BaseDataBaseTools.GetDbConnection() as SqlConnection)
            {
                return conn.Select(sql);
            }
        }

        public static DataSet Query(List<string> sqls)
        {
            using(var conn = BaseDataBaseTools.GetDbConnection() as SqlConnection)
            {

                return conn.Select(sqls);
            }
        }
    }
}
