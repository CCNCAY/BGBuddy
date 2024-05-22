using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace bgbuddy
{
    /// <summary>
    /// Interaction logic for AddSession.xaml
    /// </summary>
    public partial class AddSession : Window
    {
        public AddSession()
        {
            InitializeComponent();
            string GameDataRaw = SqlHandler.ReadData(SqlHandler.CreateConnection(), ["id", "title"], "games");
            string[] GameData = GameDataRaw.Split("\n");
            
            for (int i = 1; i < GameData.Length-1; i++)
            {   
                string game = GameData[i].Replace("|", ":\t");
                GameBox.Items.Add(new ComboBoxItem { Content = game });
            }
            GameBox.SelectedIndex = 0;
        }


        private void AddThisSession (object sender, RoutedEventArgs e)

        {
            MessageBoxResult AddError;
            string SessionDate = Date.Text;
            if (DateCheck(SessionDate))
            {
                SessionAdder(SessionDate);
                this.Close();
            }

            else
               { AddError = MessageBox.Show("Incorrect date format.", "Error"); }

        }

        private void AddThisSessionStay(object sender, RoutedEventArgs e)

        {
            MessageBoxResult AddError;
            string SessionDate = Date.Text;

            if (DateCheck(SessionDate))
            { SessionAdder(SessionDate); }

            else
                { AddError = MessageBox.Show("Incorrect date format.", "Error"); }
        }

        private void CancelButton(object sender, RoutedEventArgs e)
            { this.Close(); }



        private void KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                MessageBoxResult AddError;
                string SessionDate = Date.Text;

                if (DateCheck(SessionDate))
                    { SessionAdder(SessionDate); }

                else
                    { AddError = MessageBox.Show("Incorrect date format.", "Error"); }
            }
            else if (e.Key == Key.Escape)
                { this.Close(); }
        }

        private void SessionAdder(string Date)
        {
            SqlHandler.InsertData(SqlHandler.CreateConnection(), "sessions", "'game_id', 'date'", $"'{GameBox.SelectedIndex+1}', '{Date}'");
            MessageBoxResult AddMsg = MessageBox.Show("Session added!", "Success!");
        }

        private bool DateCheck(string Date)
            { return DateTime.TryParse(Date, out _); }
    }
}
