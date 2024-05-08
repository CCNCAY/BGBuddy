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

namespace bgbuddy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window


    {
        public string TestString = "Test";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddFriendButton(object sender, RoutedEventArgs rea)
        {
            MainTextBox.Text = TestString;
        }

        private void AddGameButton(object sender, RoutedEventArgs rea)
        {

        }


    }
}