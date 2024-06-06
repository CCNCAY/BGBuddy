using System;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
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

            string[] Columns = ["bgg_id", "title", "year", "min_player", "max_player", "best_player", "bgg_rating", "complexity", "remark"];

            InitializeComponent();
            MainTextBox.Text = "All games by title:\n" + MakeTable(SqlHandler.ReadData(SqlHandler.CreateConnection(), Columns, "games", "title"));

        }

        private void AddFriendButton(object sender, RoutedEventArgs e)
        {
            AddPlayer addplayer = new AddPlayer();
            addplayer.ShowDialog();
            MainTextBox.Text = "All games by id:\n" + MakeTable(SqlHandler.ReadData(SqlHandler.CreateConnection(), ["name"], "players", "name"));


        }

        private void AddGameButton(object sender, RoutedEventArgs e)
        {
            AddGame addgame = new AddGame();
            addgame.ShowDialog();
            string[] Columns = [ "title", "year", "min_player", "max_player", "best_player", "bgg_rating", "complexity", "remark"];
            MainTextBox.Text = "All games by id:\n" + MakeTable(SqlHandler.ReadData(SqlHandler.CreateConnection(), Columns, "games", "id DESC"));


        }

        private void AddSessionButton(object sender, RoutedEventArgs e)
        {
            AddSession Addsession = new AddSession();
            Addsession.ShowDialog();
            string[] Columns = ["id", "game_id", "date"];
            MainTextBox.Text = "All games by id:\n" + MakeTable(SqlHandler.ReadData(SqlHandler.CreateConnection(), Columns, "sessions", "id DESC"));
        }

        private void AddLikingButton(object sender, RoutedEventArgs e)
        {
            AddLiking AddLiking = new AddLiking();
            AddLiking.ShowDialog();
            string[] Columns = ["games.title", "players.name", "game_relations.like_level"];
            MainTextBox.Text = "All games by id:\n" + MakeTable(SqlHandler.ReadData(SqlHandler.CreateConnection(), Columns, "games join game_relations on games.'id' = game_relations.game_id join players on game_relations.player_id = players.id", "players.name"));

        }

        private void EditFriendButton(object sender, RoutedEventArgs e)
        {

        }

        private void EditGameButton(object sender, RoutedEventArgs e)
        {
            EditGames EditGame = new EditGames();
            EditGame.ShowDialog();
        }

        private void EditSessionButton(object sender, RoutedEventArgs e)
        {

        }

        private void EditLikingButton(object sender, RoutedEventArgs e)
        {

        }

        private void ShowByMin(object sender, RoutedEventArgs e)
        {
            string[] Columns = ["title", "year", "min_player", "bgg_rating", "complexity", "remark"];
            MainTextBox.Text = "All games by minimum player number:\n" + MakeTable(SqlHandler.ReadData(SqlHandler.CreateConnection(), Columns, "games", "min_player"));
        }

        private void ShowByMax(object sender, RoutedEventArgs e)
        {
            string[] Columns = ["title", "year", "max_player", "bgg_rating", "complexity", "remark"];
            MainTextBox.Text = "All games by maximum player number:\n" + MakeTable(SqlHandler.ReadData(SqlHandler.CreateConnection(), Columns, "games", "max_player DESC"));
        }

        private void ShowByBGGrate(object sender, RoutedEventArgs e)
        {
            string[] Columns = ["title", "year", "min_player", "max_player", "bgg_rating", "complexity", "remark"];
            MainTextBox.Text = "All games by maximum player number:\n" + MakeTable(SqlHandler.ReadData(SqlHandler.CreateConnection(), Columns, "games", "bgg_rating DESC"));
        }

        private void ShowByComplexity(object sender, RoutedEventArgs e)
        {
            string[] Columns = ["title", "year", "min_player", "max_player", "bgg_rating", "complexity", "remark"];
            MainTextBox.Text = "All games by maximum player number:\n" + MakeTable(SqlHandler.ReadData(SqlHandler.CreateConnection(), Columns, "games", "complexity DESC"));
        }

        private void ShowAll(object sender, RoutedEventArgs e)
        {
            string[] Columns = ["id", "bgg_id", "title", "year", "min_player", "max_player", "best_player", "bgg_rating", "complexity", "remark"];
            MainTextBox.Text = "All games by id:\n" + MakeTable(SqlHandler.ReadData(SqlHandler.CreateConnection(), Columns, "games", "title"));

        }

        private void ShowCustom(object sender, RoutedEventArgs e)
        {

        }
        private void ShowPlayersBySession(object sender, RoutedEventArgs e)
        {
            string[] Columns = ["sessions.date", "games.title",  "players.name"];
            MainTextBox.Text = "All sessions:\n" + MakeTable(SqlHandler.ReadData(SqlHandler.CreateConnection(), Columns, "games JOIN sessions ON games.id = sessions.game_id JOIN presences on sessions.id " +
                "= presences.session_id JOIN players ON players.id = presences.player_id", "sessions.date DESC"));
        }
        private void ShowPlayersByGames(object sender, RoutedEventArgs e)
        {
            string[] Columns = ["name", "title",  "like_level"];
            MainTextBox.Text = "All sessions:\n" + MakeTable(SqlHandler.ReadData(SqlHandler.CreateConnection(), Columns, "games JOIN game_relations ON game_relations.game_id = games.id JOIN players on players.id " +
                "= game_relations.player_id", "name"));
        }
        private void ShowPlayersAll(object sender, RoutedEventArgs e)
        {
            string[] Columns = ["sessions.id", "games.title", "sessions.date", "players.name"];
            MainTextBox.Text = "All sessions:\n" + MakeTable(SqlHandler.ReadData(SqlHandler.CreateConnection(), Columns, "games JOIN sessions ON games.id = sessions.game_id JOIN presences on sessions.id " +
                "= presences.session_id JOIN players ON players.id = presences.player_id", "date DESC"));
        }
        private void ShowPlayersCustom(object sender, RoutedEventArgs e)
        {
            string[] Columns = ["sessions.id", "games.title", "sessions.date", "players.name"];
            MainTextBox.Text = "All sessions:\n" + MakeTable(SqlHandler.ReadData(SqlHandler.CreateConnection(), Columns, "games JOIN sessions ON games.id = sessions.game_id JOIN presences on sessions.id " +
                "= presences.session_id JOIN players ON players.id = presences.player_id", "date DESC"));
        }
        private string MakeTable(string SqlOutput)
        {
            string[][] WordLists = ArrayToNestedArray(StringToArray(SqlOutput));
            
            string Out = "";
            int NumberOfColumns = WordLists[0].Length;
            int[] LongestInEachCol = new int[NumberOfColumns];
            for (int i = 0; i < NumberOfColumns; i++)
            {
                LongestInEachCol[i] = 0;
            }

            foreach (string[] WordList in WordLists)
            {
                for (int i = 0; i < WordList.Length; i++)
                {
                    if (LongestInEachCol[i] < WordList[i].Length)
                    {
                        LongestInEachCol[i] = WordList[i].Length;
                    }

                }
            }
            foreach (string[] WordList in WordLists)
            {
                for (int i = 0; i < WordList.Length; i++)
                {
                    WordList[i] = WordList[i].PadRight(LongestInEachCol[i], ' ');
                }
            }
            string HeadLine = "| " + string.Join(" | ", WordLists[0]) + " |";
            Out += "-".PadRight(HeadLine.Length, '-') + "\n";
            Out += HeadLine + "\n" + "=".PadRight(HeadLine.Length, '=') + "\n";

            for (int i = 1; i < WordLists.Length; i++)
            {
                string Line = "| " + string.Join(" | ", WordLists[i]) + " |";
                Line += "\n" + "-".PadRight(HeadLine.Length, '-') + "\n";
                Out += Line;
            }

            return Out;
        }


        private string[] StringToArray(string PrimaryInput)
        {
            return PrimaryInput.Split('\n');
        }

        private string[][] ArrayToNestedArray(string[] SecondaryInput)
        {
            string[][] Out = new string[SecondaryInput.Length-1][];
            for (int i = 0; i < SecondaryInput.Length-1; i++)
            {
                Out[i] = SecondaryInput[i].Split('|'); 
            }

            return Out;
        }
    }
}