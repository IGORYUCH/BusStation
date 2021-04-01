using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace BusStation
{
    public class Core
    {
        public static string connectionString = "";
        public static string user_id = "1";

        public static int ExecuteNonQuery(string query)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                SQLiteCommand Command = new SQLiteCommand(query, connection);
                int affected = Command.ExecuteNonQuery();
                return affected;
            }
        }

        public static void ExecureReader(string query, SQLiteDataReader sqlReader)
        {
            using (SQLiteConnection connection = new SQLiteConnection(Core.connectionString))
            {
                connection.Open();
                SQLiteCommand Command = new SQLiteCommand(query, connection);
                sqlReader = Command.ExecuteReader();
            }
        }

        public static object ExecuteScalar(string query)
        {
            using (SQLiteConnection connection = new SQLiteConnection(Core.connectionString))
            {
                connection.Open();
                SQLiteCommand Command = new SQLiteCommand(query, connection);
                object o = Command.ExecuteScalar();
                return o;
            }
        }
    }
}
