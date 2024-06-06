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

namespace bgbuddy
{
    /// <summary>
    /// Interaction logic for AddPlayer.xaml
    /// </summary>
    public partial class AddPlayer : Window
    {
        public AddPlayer()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(KeyUp);
        }

        private void AddThisPlayer(object sender, RoutedEventArgs e)
        {
            PlayerAdder();
            this.Close();
        }

        private void AddThisPlayerStay(object sender, RoutedEventArgs e)
            { PlayerAdder(); }

        private void CancelButton(object sender, RoutedEventArgs e)
            { this.Close(); }



        private void KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                {PlayerAdder();}

            else if (e.Key == Key.Escape)
                { this.Close(); }
        }

        private void PlayerAdder()
        {
            string Value = "'" + Add.Text + "'";
            SqlHandler.InsertData(SqlHandler.CreateConnection(), "players", "name",  Value);
        }
    }
}
