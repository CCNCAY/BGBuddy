using System;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
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





        public MainWindow()
        {
            
            
            InitializeComponent();
        }

        private void AddFriendButton(object sender, RoutedEventArgs e)
        {
            MainTextBox.Text = SqlHandler.ReadData(SqlHandler.CreateConnection(), ["id", "title", "min_player"], "games");
        }

        private void AddGameButton(object sender, RoutedEventArgs e)
        {
            AddGame addgame = new AddGame();
            addgame.ShowDialog();
            MainTextBox.Text = MessageOut;

        }



    }



}