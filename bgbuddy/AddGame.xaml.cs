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
        {
            InitializeComponent();
        }

        private void AddThisGame(object sender, RoutedEventArgs e)
            //It may happen that a game title is a number, in this case this approach will fetch the wrong game. 
            //Search manually and add by title.
        {
            string SearchInput  = Search.Text;
            if (Int32.TryParse(SearchInput, out int SearchInt))
            {
                try
                {
                    MainWindow.MessageOut = Boardgame.GetAllString(Boardgame.CleanDataFromXml(Boardgame.GetGameById(SearchInt)));
                }
                catch (Exception ex)
                {
                    MessageBoxResult AddError = MessageBox.Show(ex.Message, "Error");
                }
            }
            else
            {
                try
                {
                    MainWindow.MessageOut = Boardgame.GetAllString(Boardgame.CleanDataFromXml(Boardgame.GetGameById(Boardgame.GetGamebyTitle(SearchInput))));
                }
                catch (Exception ex)
                {
                    MessageBoxResult AddError = MessageBox.Show(ex.Message, "Error");
                }
            }
            MessageBoxResult AddMsg = MessageBox.Show("Game added!", "Success!");



        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }




    }
}
