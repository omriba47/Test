using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
namespace Pinger
{
    class DbClass
    {
        public static void ConnectDB(out MySqlConnection con)
        {
            string server = "localhost";
            string database = "pingerDB";
            string uid = "root";
            string password = "";
            string connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            con = new MySqlConnection(connectionString);
            con.Open();
        }
        public static MySqlDataReader SelectQuery(MySqlConnection con, string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, con);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();
            return dataReader;



        }
        public static void CloseDB(MySqlConnection con)
        {
            con.Close();
        }
    }
}
