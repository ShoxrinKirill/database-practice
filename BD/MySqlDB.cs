using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD
{
    class MySqlDB
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "host";
            int port = 33066;
            string database = "database";
            string username = "user_name";
            string password = "password";

            return MySqlDB.GetDBConnection(host, port, database, username, password);
        }

        public static MySqlConnection GetDBConnection(string host, int port, string database, string username, string password)
        {
            String connstr = "server = " + host + "; port = " + port + "; database = " + database + "; userid = " + username + "; password = " + password;

            MySqlConnection conn = new MySqlConnection(connstr);

            return conn;
        }

        public static MySqlDataReader GetDBTable(string sql, MySqlConnection conn)
        {

            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader;

            reader = command.ExecuteReader();

            return reader;
        }
    }
}
