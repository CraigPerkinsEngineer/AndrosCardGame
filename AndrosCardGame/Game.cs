using System;
using System.Collections.Generic;
using System.Linq;

namespace AndrosCardGame
{
    public class Game
    {
        private List<Card> deck;
        private List<Person> players;
        private int numberOfCardsPerPlayer;

        private List<string> congratulatingPhrases = new List<string>() {
            "Good game!",
            "Well played!",
            "Winner, winner, chicken dinner!",
            "You were a star out there!",
            "You done good!",
            "Good job!",
            "Way to go!",
            "Keep it up!",
            "Excellent!",
        };

        public Game(List<Card> deck, List<Person> players, int numberOfCardsPerPlayer)
        {
            this.deck = deck;
            this.players = players;
            this.numberOfCardsPerPlayer = numberOfCardsPerPlayer;

            // If there is no dealer, don't play
            if (!players.Any(p => p.IsDealer))
            {
                throw new InvalidOperationException("There is no dealer. Come back when the dealer is here.");
            }
        }

        public void NewRound()
        {
            Console.WriteLine("");

            NewHand(numberOfCardsPerPlayer);

            foreach (var player in players)
            {
                Console.WriteLine(player.GetDisplay());
            }

            Console.WriteLine("");

            // The highest hand wins
            var winners = GetWinners();

            foreach (var winner in winners)
            {
                // The winner is announced at the end of each hand
                congratulatingPhrases.Shuffle();
                Console.WriteLine($"{winner.Name} is the winner with {winner.GetScore()} points! {congratulatingPhrases.FirstOrDefault()}");
            }
        }

        private void NewHand(int numberOfCardsPerPlayer)
        {
            // Between hands, all cards return to the deck, which must be then shuffled
            foreach (var player in players)
            {
                var playerCards = player.ReturnAllCards();
                deck.AddRange(playerCards);
            }

            deck.Shuffle();

            // Deal 2 cards per player in a round robin manner
            Console.WriteLine($"The dealer is dealing {numberOfCardsPerPlayer} cards to each player.");

            for (int i = 0; i < numberOfCardsPerPlayer; i++)
            {
                players.FirstOrDefault(p => p.IsDealer).DealOneCardToEveryone(deck, players);
            }
        }

        private HashSet<Person> GetWinners()
        {
            var highestScore = 0;
            var winners = new HashSet<Person>();

            foreach (var player in players)
            {
                var score = player.GetScore();

                if (score == highestScore)
                {
                    winners.Add(player);
                }
                else if (score > highestScore)
                {
                    winners = new HashSet<Person>();
                    winners.Add(player);
                    highestScore = score;
                }
            }

            return winners;
        }

    }

    static class MyExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rnd = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
