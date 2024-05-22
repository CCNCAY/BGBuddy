using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using System.Xml.Linq;

namespace bgbuddy
{
    /// <summary>
    /// Interaction logic for AddGame.xaml
    /// </summary>
    public partial class AddGame : Window
    {
        public AddGame()
            { InitializeComponent(); }

        private void AddThisGame(object sender, RoutedEventArgs e)
        {
          GameAdder();
          this.Close(); 
        }

        private void AddThisGameStay(object sender, RoutedEventArgs e)
            {GameAdder();}

        private void CancelButton(object sender, RoutedEventArgs e)
            {this.Close();}

        private void KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                { GameAdder(); }

            else if (e.Key == Key.Escape)
                { this.Close(); } 
        }

        private void GameAdder()
        //It may happen that a game title is a number, in this case this approach will fetch the wrong game. 
        //Search manually and add by title.
        {
            string RemarkInput = Remark.Text;
            string Columns = "'bgg_id', 'title', 'year', 'min_player', 'max_player', 'best_player', 'bgg_rating', 'complexity', 'remark'";
            if (RemarkInput == "")
            {RemarkInput = " ";}

            string SearchInput = Search.Text;
            if (Int32.TryParse(SearchInput, out int SearchInt))
            {
                try
                {
                    string Values = Boardgame.GetAllString(Boardgame.CleanDataFromXml(Boardgame.GetGameById(SearchInt))) + ", '" + RemarkInput + "'";
                    SqlHandler.InsertData(SqlHandler.CreateConnection(), "games", Columns, Values) ;
                    MessageBoxResult AddMsg = MessageBox.Show("Game added!", "Success!");

                }
                catch (Exception ex)
                    { MessageBoxResult AddError = MessageBox.Show(ex.Message, "Error");}
            }
            else
            {
                try
                {
                    string Values = Boardgame.GetAllString(Boardgame.CleanDataFromXml(Boardgame.GetGameById(Boardgame.GetGamebyTitle(SearchInput)))) + ", '" + RemarkInput + "'";
                    SqlHandler.InsertData(SqlHandler.CreateConnection(), "games", Columns, Values);
                    MessageBoxResult AddMsg = MessageBox.Show("Game added!", "Success!");

                }
                catch (Exception ex)
                    { MessageBoxResult AddError = MessageBox.Show(ex.Message, "Error"); }
            }
        }
    }
}
