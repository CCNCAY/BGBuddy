using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace bgbuddy
{
    /// <summary>
    /// Interaction logic for EditGames.xaml
    /// </summary>
    public partial class EditGames : System.Windows.Window
    {
        public EditGames()
        {
            InitializeComponent();
            string GameDataRaw = SqlHandler.ReadData(SqlHandler.CreateConnection(), ["id", "title"], "games");
            string[] GameData = GameDataRaw.Split("\n");

            for (int i = 1; i < GameData.Length - 1; i++)
            {
                string game = GameData[i].Replace("|", ":\t");
                GameBox.Items.Add(new ComboBoxItem { Content = game });
            }
            GameBox.SelectedIndex = 0;




            this.PreviewKeyDown += new KeyEventHandler(KeyUp);
        }

        private void KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            { 
            
            }

            else if (e.Key == Key.Escape)
            { this.Close(); }
        }

        private void EditThisGame(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void EditThisGameStay(object sender, RoutedEventArgs e)
        {  }

        private void CancelButton(object sender, RoutedEventArgs e)
        { this.Close(); }




        private void Select(object sender, SelectionChangedEventArgs e)
        {


            this.Close();


        }


    }
}
