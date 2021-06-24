using System;
using System.Collections.Generic;
using System.Linq;

namespace AndrosCardGame
{
    public class Program
    {
        private const int numberOfPlayers = 5;
        private const int numberOfCardsPerPlayer = 2;

        static readonly string[] positiveAnswers = {
            "yes",
            "y",
            "ya",
            "yep",
            "yup",
            "aye",
            "yea",
            "yeah",
            "indeed",
            "forsooth",
            "totally",
            "totes",
            "sure",
            "for sure",
            "surely",
            "certainly",
            "definitely",
            "gladly",
            "obviously",
            "sounds good",
            "you bet",
            "of course",
            "please",
            "do you really need to ask?",
            "life's too short to say no",
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Andros Casino! Take a seat and watch this most glorious card game!");

            // If there are too many players for each player to have the same number of cards, don't play 
            // (this won't happen unless someone changes the numberOfPlayers or numberOfCardsPerPlayer constants)
            if ((numberOfPlayers + 1) * numberOfCardsPerPlayer > 52)
            {
                Console.WriteLine("It looks like there are too many people to play. Come back later.");
                return;
            }

            // There are 5 players
            var players = CreatePlayers(numberOfPlayers);

            // There is 1 dealer
            players.Add(new Person()
            {
                Name = "Dealer",
                Number = players.Count + 1,
                IsDealer = true
            });

            // There is 1 standard 52 card deck (no jokers)
            var deck = CreateDeck();

            var game = (Game)null;

            try
            {
                game = new Game(deck, players, numberOfCardsPerPlayer);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            catch
            {
                Console.WriteLine("It looks like there was a problem setting up the game. Come back later.");
                return;
            }

            var play = true;

            while (play)
            {
                game.NewRound();

                Console.Write("\nWould you like to watch another game? (yes/no): ");
                var userPrompt = Console.ReadLine();

                if (!positiveAnswers.Contains(userPrompt.ToLower()))
                {
                    play = false;
                }
            }

            Console.WriteLine("\nThanks for visiting the Andros Casino! Come again soon!");
        }

        static List<Card> CreateDeck()
        {
            var deck = new List<Card>();

            for (int i = 2; i < 15; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    deck.Add(new Card()
                    {
                        PointValue = i,
                        SuitNumber = j
                    });
                }
            }

            return deck;
        }

        static List<Person> CreatePlayers(int numberOfPlayers)
        {
            var players = new List<Person>();

            for (int i = 1; i <= numberOfPlayers; i++)
            {
                players.Add(new Person()
                {
                    Name = $"Player {i}",
                    Number = i
                });
            }

            return players;
        }
    }
}
