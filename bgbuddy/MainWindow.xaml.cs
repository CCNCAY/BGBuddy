using System.Data;
using System.Data.Common;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Data.Sqlite;
using Microsoft.Identity.Client;

namespace bgbuddy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 




        public partial class MainWindow : Window
    {
        public string address = @"Data Source=E:\Dropbox\bgbuddy\bgbuddy\bgbuddy.db";
        public static string MessageOut = "Test main";
        public string AddGame = "";


        static SqliteConnection CreateConnection()
        {

            SqliteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SqliteConnection(@"Data Source=E:\Dropbox\bgbuddy\bgbuddy\db\bgbuddy_test.db");
         // Open the connection:
         try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {

            }
            return sqlite_conn;
        }

        static void CreateTable(SqliteConnection conn)
        {
            SqliteCommand sqlite_cmd;
            string Createsql = "create table name ();";
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            sqlite_cmd.ExecuteNonQuery();
        }

        static void InsertData(SqliteConnection conn)
        {
            SqliteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO SampleTable (Col1, Col2) VALUES('Test Text ', 1); ";
            sqlite_cmd.ExecuteNonQuery();
        }

        static string ReadData(SqliteConnection conn)
        {
            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM games";
            string myreader = "";
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read())
            {
                myreader += sqlite_datareader.GetString(4);
                myreader += "\n";

            }
            
            conn.Close();
            return myreader;
        }


        public MainWindow()
        {
            
            
            InitializeComponent();
        }

        private void AddFriendButton(object sender, RoutedEventArgs rea)
        {
            MainTextBox.Text = ReadData(CreateConnection());
        }

        private void AddGameButton(object sender, RoutedEventArgs rea)
        {
            AddGame addgame = new AddGame();
            addgame.ShowDialog();
            MainTextBox.Text = MessageOut;

        }



    }



}