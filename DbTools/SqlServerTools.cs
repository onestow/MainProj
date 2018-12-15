using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTools
{
    public static class SqlServerTools
    {
        public static bool Execute(this SqlConnection conn, List<string> sqls)
        {
            var trans = conn.BeginTransaction();
            try
            {
                using (var comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    foreach (var sql in sqls)
                    {
                        comm.CommandText = sql;
                        comm.ExecuteNonQuery();
                    }
                }
                trans.Commit();
                return true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public static int Execute(this SqlConnection conn, string sql)
        {
            using (var comm = new SqlCommand(sql, conn))
            {
                return comm.ExecuteNonQuery();
            }
        }

        public static DataTable Select(this SqlConnection conn, string sql)
        {
            var sqls = new List<string> { sql };
            var ds = conn.Select(sqls);
            return ds.Tables[0];
        }

        public static DataSet Select(this SqlConnection conn, List<string> sqls)
        {
            var ds = new DataSet();

            using(var comm = new SqlCommand())
            {
                comm.Connection = conn;
                using (var adapter = new SqlDataAdapter(comm))
                {
                    int seq = 0;
                    foreach (var sql in sqls)
                    {
                        comm.CommandText = sql;
                        adapter.Fill(ds, $"dt{seq++}");
                    }
                }
            }
            return ds;
        }

        public static object ExecuteSclar(this SqlConnection conn, string sql)
        {
            using(var comm = new SqlCommand(sql, conn))
            {
                return comm.ExecuteScalar();
            }
        }
    }
}
