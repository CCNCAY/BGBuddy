using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace bgbuddy
{
    public class Boardgame

    {
        private const int MIN_VOTE = 100; // I want at least this many votes so that best player numbers are valid. 
        public required string Title {  get; set; }
        public int Year { get; set; }
        public int BggId { get; set; }
        public int MinPlayer { get; set; }
        public int MaxPlayer { get; set; }
        public int BestPlayer { get; set; }

        public float BggRating { get; set; }
        public float Complexity {  get; set; }
        public string? Remark { get; set; }


        public static string GetGameById(int Id)
            //Boardgame API returns xml, empty header is no match, otherwise a game. 
            //The returned game may be have types like game extension or other non-strictly games.
            //This method returns only those that are type="game" and throws exception if not found or not game.
        {
            var BggClient = new HttpClient();
            HttpResponseMessage BggResponse = BggClient.GetAsync($"https://boardgamegeek.com/xmlapi2/thing?id={Id}&stats=1").Result;
            string Out = BggResponse.Content.ReadAsStringAsync().Result;
            if (Regex.IsMatch(Out, $"<item type=\"boardgame\" id=\"{Id}\">"))
                {
                    return Out;
                }
            else
                { 
                    throw new Exception("This BGG game ID is not found or not a game."); 
                }
                 
        }

        public static int GetGamebyTitle(string Title)
            //Trying to search a game by title using strict search. Error if no result or more than 1 reuslt, or if no game type.
            // Space in search has to be replaced by +. 
        {
            Title = Title.Replace(" ", "+");
            var BggClient = new HttpClient();
            HttpResponseMessage BggResponse = BggClient.GetAsync($"https://boardgamegeek.com/xmlapi2/search?query={Title}&type=boardgame&exact=1").Result;
            string ResultText = BggResponse.Content.ReadAsStringAsync().Result;
            int Hits = Int32.Parse((Regex.Match(ResultText, @"(?<=<items total=.)\d+(?=. termsofuse)")).Value); //API gives an int, 0 for no result. Safe to parse.
            if (Hits == 0)
            {
                throw new Exception("This search term did not return any valid result.");
            }
            else if (Hits == 1 && Regex.IsMatch(ResultText, @"<item type=.boardgame. id")) //Interested only if exactly 1 hit and it is a borad game not something else.
            {
                return Int32.Parse((Regex.Match(ResultText, @"(?<=<item type=.boardgame. id=.)\d+")).Value);
            }
            else
            {
                throw new Exception("Too many hits.");
            }
        }

        public static int GetBestPlayer(string BggResponse)
            //Calculates the best player number from BGG votes. 
        {

            if (SecureFloatGet(BggResponse, "User Suggested Number of Players", @"(?<=User Suggested Number of Players. totalvotes=.)(\d+)") >= MIN_VOTE)
            {
                string[] playernumbers = Regex.Matches(BggResponse, @"(?<=<results numplayers=.)\d+(?=.>)").Cast<Match>().Select(m => m.Value).ToArray();
                string[] votes = Regex.Matches(BggResponse, @"(?<=<result value=.Best. numvotes=.)(\d+)").Cast<Match>().Select(m => m.Value).ToArray();
                int Best = 0;
                int BestPlayerIndex = 0;

                for (int i = 0; i<playernumbers.Length; i++)
                {
                    if (Int32.Parse(votes[i]) >= Best) // safe to parse
                    {
                        BestPlayerIndex = i;
                    }
                }
                return Int32.Parse(playernumbers[BestPlayerIndex]); //safe to assume no out of index because of DB structure and match
            }
            else
            {
                return 0;
            }       
        }


        public static string GetAllString(Boardgame game)
            //returns the boardgame values in the order of the BGBuddy database values
        {
            return "'" + game.BggId.ToString() + "', '" + game.Title +"', '"+ game.Year.ToString() + "', '" + game.MinPlayer.ToString()+ "', '" + game.MaxPlayer.ToString()+ "', '" + game.BestPlayer.ToString() + "', '" 
                + game.BggRating.ToString("n1") + "', '" + game.Complexity.ToString("n2") + "'";
        }

        public static Boardgame CleanDataFromXml(string BggResponse)
            //using all the helper functions, returns a full boardgame from the BGG API XML
        {

            Boardgame Out = new Boardgame()
            {
                Title = Regex.Match(BggResponse, @"(?<=(<name type=.primary.*value=.))(.*)(?=(../>))").Value,
                Year= (int)SecureFloatGet(BggResponse, @"<yearpublished value=.\d+", @"(?<=<yearpublished value=.)\d+"),
                BggId = Int32.Parse(Regex.Match(BggResponse, @"(?<=<item type=.boardgame. id=.)\d+").Value),
                MinPlayer = (int)SecureFloatGet(BggResponse, @"<minplayers value=.\d+", @"(?<=<minplayers value=.)\d+"),
                MaxPlayer = (int)SecureFloatGet(BggResponse, @"<maxplayers value=.\d+", @"(?<=<maxplayers value=.)\d+"),
                BestPlayer = GetBestPlayer(BggResponse),
                BggRating = SecureFloatGet(BggResponse, @"<average value=.\d", @"(?<=<average value=.)(\d\D\d+)"),
                Complexity = SecureFloatGet(BggResponse, @"<averageweight value=.\d", @"(?<=<averageweight value=.)\d\D\d+"),
            };
            return Out;
        }

        private static float SecureFloatGet(string BggResponse, string CheckExist, string Extract)
            //Helper function for regex, returns a number from an expression found by regex.
        {
            if (Regex.IsMatch(BggResponse, CheckExist))
            {
                return float.Parse(Regex.Match(BggResponse, Extract).Value);
            }
            else
            { return 0f; }
        }

    }
}
