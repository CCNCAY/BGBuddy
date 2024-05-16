using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace bgbuddy
{
    public class SqlHandler
    {

       public static SqliteConnection CreateConnection()
        {

            SqliteConnection BgDBconnection;
            // Create a new database connection:
            BgDBconnection = new SqliteConnection(@"Data Source=E:\bgbuddy\bgbuddy\db\bgbuddy_test.db");
            // Open the connection:
            try
            {
                BgDBconnection.Open();
            }
            catch (Exception ex)
            {
                MessageBoxResult DBError = MessageBox.Show(ex.Message, "Error");
                Environment.Exit(1);
            }
            return BgDBconnection;
        }

        static void CreateTable(SqliteConnection conn)
        {
            SqliteCommand SQLiteCmd;
            string Createsql = "create table name ();";
            SQLiteCmd = conn.CreateCommand();
            SQLiteCmd.CommandText = Createsql;
            SQLiteCmd.ExecuteNonQuery();
        }

        public static void InsertData(SqliteConnection conn, string Table, string Columns, string Values) 
        {
            SqliteCommand SQLiteCmd;
            SQLiteCmd = conn.CreateCommand();
            SQLiteCmd.CommandText = $"INSERT INTO {Table} ({Columns}) VALUES ({Values});";
            SQLiteCmd.ExecuteNonQuery();
        }

        public static string ReadData(SqliteConnection conn, string[] Columns, string Table, string OrderBy = "id")
        {
            string ColumnStr = string.Join(" ,", Columns);
            SqliteDataReader SQLiteDatareader;
            SqliteCommand SQLiteCmd;
            SQLiteCmd = conn.CreateCommand();
            SQLiteCmd.CommandText = $"SELECT {ColumnStr} FROM {Table} ORDER BY {OrderBy}";
            string Out = string.Join("|", Columns) + "\n";
            SQLiteDatareader = SQLiteCmd.ExecuteReader();

            while (SQLiteDatareader.Read())
            {
                for (int i = 0; i < Columns.Length; i++)
                {

                    Out += SQLiteDatareader.GetString(i);
                    if (i < Columns.Length - 1)
                    {
                        Out += "|";
                    }

                }
                Out += "\n";
            }
            conn.Close();
            return Out;
        }
    }
}
