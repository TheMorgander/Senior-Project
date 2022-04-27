using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Taskbar
{
    public class History
    {
        /******************************************************************/
        #region General Variables
        private SQLiteConnection sqlite_connection;
        #endregion
        /******************************************************************/

        /******************************************************************/
        protected internal void Initalize()
        {
            try
            {
                CreateConnection();
                CreateTable();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }
        /******************************************************************/

        /******************************************************************/
        private void CreateConnection()
        {
            try
            {
                sqlite_connection = new SQLiteConnection("Data Source=C:/Users/Morgan Anderson/OneDrive/Documents/database.sqlite; Version = 3; New = True; Compress = True;");
                sqlite_connection.Open();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }
        /******************************************************************/

        /******************************************************************/
        private void CreateTable()
        {
            try
            {
                SQLiteCommand sqlite_cmd = sqlite_connection.CreateCommand();
                string cpu_table = "CREATE TABLE CPU(Time VARCHAR(64), Max DOUBLE, Min DOUBLE, Average DOUBLE)";
                string gpu_table = "CREATE TABLE GPU(Time VARCHAR(64), Max DOUBLE, Min DOUBLE, Average DOUBLE)";
                string ram_table = "CREATE TABLE RAM(Time VARCHAR(64), Max DOUBLE, Min DOUBLE, Average DOUBLE)";
                string disk_upload_table = "CREATE TABLE DISK_UPLOAD(Time VARCHAR(64), Max DOUBLE, Min DOUBLE, Average DOUBLE)";
                string disk_download_table = "CREATE TABLE DISK_DOWNLOAD(Time VARCHAR(64), Max DOUBLE, Min DOUBLE, Average DOUBLE)";
                string network_upload_table = "CREATE TABLE NETWORK_UPLOAD(Time VARCHAR(64), Max DOUBLE, Min DOUBLE, Average DOUBLE)";
                string network_download_table = "CREATE TABLE NETWORK_DOWNLOAD(Time VARCHAR(64), Max DOUBLE, Min DOUBLE, Average DOUBLE)";
                sqlite_cmd.CommandText = cpu_table;
                sqlite_cmd.ExecuteNonQuery();
                sqlite_cmd.CommandText = gpu_table;
                sqlite_cmd.ExecuteNonQuery();
                sqlite_cmd.CommandText = ram_table;
                sqlite_cmd.ExecuteNonQuery();
                sqlite_cmd.CommandText = disk_upload_table;
                sqlite_cmd.ExecuteNonQuery();
                sqlite_cmd.CommandText = disk_download_table;
                sqlite_cmd.ExecuteNonQuery();
                sqlite_cmd.CommandText = network_upload_table;
                sqlite_cmd.ExecuteNonQuery();
                sqlite_cmd.CommandText = network_download_table;
                sqlite_cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }
        /******************************************************************/

        /******************************************************************/
        protected internal void Insert(string query)
        {
            try
            {
                CreateConnection();
                SQLiteCommand sqlite_cmd = new SQLiteCommand(query, sqlite_connection);
                sqlite_cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }
        /******************************************************************/
    }
}
