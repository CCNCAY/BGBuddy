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
        {
            string SearchInput  = Search.Text;
            
            var BggClient = new HttpClient();
            HttpResponseMessage BggResponse = BggClient.GetAsync($"https://boardgamegeek.com/xmlapi2/thing?id={SearchInput}").Result;
            string FullContent = BggResponse.Content.ReadAsStringAsync().Result;
            string NameRegex = @"(?<=(<name type=.primary.*value=.))(.*)(?=(../>))";
            string GameName =Regex.Match(FullContent, NameRegex).Value;

           

            MainWindow.MessageOut = GameName;



            this.Close();
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }




    }
}
