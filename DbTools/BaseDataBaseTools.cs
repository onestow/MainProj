using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Data.SqlClient;

namespace DbTools
{
    public abstract class BaseDataBaseTools
    {
        protected BaseDataBaseTools() { }

        public static DbConnection GetDbConnection(string connStr = "sqlserver", ConnDbType connDbType = ConnDbType.SqlServer)
        {
            var fullConnStr = ConfigurationManager.ConnectionStrings[connStr].ToString();
            if (connDbType == ConnDbType.SqlServer)
                return new SqlConnection(fullConnStr);
            else if (connDbType == ConnDbType.Oracle)
                return new OracleConnection(fullConnStr);
            else
                return null;
        }
    }

    public enum ConnDbType
    {
        SqlServer,
        Oracle
    }
}
