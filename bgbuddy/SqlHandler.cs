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

        static void InsertData(SqliteConnection conn)
        {
            SqliteCommand SQLiteCmd;
            SQLiteCmd = conn.CreateCommand();
            SQLiteCmd.CommandText = "INSERT INTO SampleTable (Col1, Col2) VALUES('Test Text ', 1); ";
            SQLiteCmd.ExecuteNonQuery();
        }

        public static string ReadData(SqliteConnection conn, string[] Columns, string Table)
        {
            string ColumnStr = string.Join(" ,", Columns);
            SqliteDataReader SQLiteDatareader;
            SqliteCommand SQLiteCmd;
            SQLiteCmd = conn.CreateCommand();
            SQLiteCmd.CommandText = $"SELECT {ColumnStr} FROM {Table}";
            string Out = "";
            SQLiteDatareader = SQLiteCmd.ExecuteReader();

            for (int i = 0; i < Columns.Length; i++)
            {
                Out += Columns[i];
                Out += "\t";

            }
            Out += "\n";

            while (SQLiteDatareader.Read())
            {
                for (int i = 0; i < Columns.Length; i++)
                {

                    Out += SQLiteDatareader.GetString(i);
                    Out += "\t";

                }
                Out += "\n";
            }

            conn.Close();
            return Out;
        }
    }
}
