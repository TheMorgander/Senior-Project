using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Taskbar
{
    public static class History
    {
        public static SQLiteConnection sqlite_connection;

        public static void Initalize()
        {
            CreateConnection();
            CreateTable();
        }

        static void CreateConnection()
        {
            sqlite_connection = new SQLiteConnection("Data Source= database.db; Version = 3; New = True; Compress = True;");
            sqlite_connection.Open();
        }

        static void CreateTable()
        {
            SQLiteCommand sqlite_cmd;
            string cpu_table = "CREATE TABLE CPU(Time VARCHAR(64), Max DOUBLE, Min DOUBLE, Average DOUBLE)";
            string gpu_table = "CREATE TABLE GPU(Time VARCHAR(64), Max DOUBLE, Min DOUBLE, Average DOUBLE)";
            string ram_table = "CREATE TABLE RAM(Time VARCHAR(64), Max DOUBLE, Min DOUBLE, Average DOUBLE)";
            string disk_table = "CREATE TABLE DISK(Time VARCHAR(64), Max DOUBLE, Min DOUBLE, Average DOUBLE)";
            string network_table = "CREATE TABLE NETWORK(Time VARCHAR(64), Max DOUBLE, Min DOUBLE, Average DOUBLE)";
            sqlite_cmd = sqlite_connection.CreateCommand();
            sqlite_cmd.CommandText = cpu_table;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = gpu_table;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = ram_table;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = disk_table;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = network_table;
            sqlite_cmd.ExecuteNonQuery();
        }

        public static void Insert(string query)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_connection.CreateCommand();
            sqlite_cmd.CommandText = query;
            sqlite_cmd.ExecuteNonQuery();
        }
    }
}
