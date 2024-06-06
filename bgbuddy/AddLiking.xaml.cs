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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace bgbuddy
{
    /// <summary>
    /// Interaction logic for AddLiking.xaml
    /// </summary>
    public partial class AddLiking : Window
    {
        public AddLiking()
        { 
            InitializeComponent();
            string GameDataRaw = SqlHandler.ReadData(SqlHandler.CreateConnection(), ["id", "title"], "games");
            string[] GameData = GameDataRaw.Split("\n");
            string PersonDataRaw = SqlHandler.ReadData(SqlHandler.CreateConnection(), ["id", "name"], "players");
            string[] PersonData = PersonDataRaw.Split("\n");

            for (int i = 1; i < GameData.Length - 1; i++)
            {
                string game = GameData[i].Replace("|", ":\t");
                GameBox.Items.Add(new ComboBoxItem { Content = game });
            }
            GameBox.SelectedIndex = 0;

            for (int i = 1; i < PersonData.Length - 1; i++)
            {
                string game = PersonData[i].Replace("|", ":\t");
                PersonBox.Items.Add(new ComboBoxItem { Content = game });
            }
            PersonBox.SelectedIndex = 0;
            this.PreviewKeyDown += new KeyEventHandler(KeyUp);




        }

        private void AddThisLiking(object sender, RoutedEventArgs e)
        {
            LikingAdder();
            this.Close();
        }



        private void AddThisLikingStay(object sender, RoutedEventArgs e)
            { LikingAdder(); }

        private void CancelButton(object sender, RoutedEventArgs e)
            { this.Close(); }



        private void KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                { LikingAdder(); }

            else if (e.Key == Key.Escape)
                { this.Close(); }
        }

        private void LikingAdder()
        {
            MessageBoxResult AddError;
            int LikeLevel;

            if (Int32.TryParse(Like.Text, out LikeLevel))
            {
                if (0< LikeLevel && LikeLevel < 6)
                {
                    SqlHandler.InsertData(SqlHandler.CreateConnection(), "game_relations", "'game_id', 'player_id', 'like_level'", $"'{GameBox.SelectedIndex+1}', '{PersonBox.SelectedIndex+1}' ,'{LikeLevel}'");
                    MessageBoxResult AddMsg = MessageBox.Show("Session added!", "Success!");
                }
                else 
                    { AddError = MessageBox.Show("Enter a valid number 1 to 5.", "Error"); }
            }
            else
                { AddError = MessageBox.Show("Enter a valid number 1 to 5.", "Error"); }


        }

    }
}
