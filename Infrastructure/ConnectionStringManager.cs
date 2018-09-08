using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class ConnectionStringManager
    {
        private static string _sqlExpressString =
            "Data Source=JAMAL\\SQLEXPRESS;Initial Catalog=MyTestingDb;Integrated Security=True;MultipleActiveResultSets=True";

        private static string _connectionString = "";
        private static string _connectionStringEF = "";
        private static string _mode = ConfigurationManager.AppSettings["Mode"];

        public static string ConnectionStringEF
        {
            get
            {
                if (_connectionStringEF == "")
                    _connectionStringEF =
                        string.Format(
                            "metadata=res://*/DataModel.csdl|res://*/DataModel.ssdl|res://*/DataModel.msl;provider=System.Data.SqlClient;provider connection string='{0}'",
                            SetConnectionString());
                return _connectionStringEF;
            }
        }

        public static string ConnectionString
        {
            get
            {
                if (_connectionString == "")
                    _connectionString = SetConnectionString();
                return _connectionString;
            }
        }


        private static string SetConnectionString()
        {
            return _sqlExpressString;
        }
    }
}

