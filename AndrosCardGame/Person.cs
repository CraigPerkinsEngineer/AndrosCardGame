using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndrosCardGame
{
    public class Person
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public bool IsDealer { get; set; }
        private List<Card> Cards { get; set; }

        public Person()
        {
            Cards = new List<Card>();
        }

        public void ReceiveCard(Card card)
        {
            Cards.Add(card);
        }

        public HashSet<Card> ReturnAllCards()
        {
            var tempCards = new HashSet<Card>();

            foreach(var card in Cards)
            {
                tempCards.Add(card);
            }

            Cards.Clear();

            return tempCards;
        }

        public string GetDisplay()
        {
            var display = new StringBuilder();
            if (Cards.Any())
            {
                display.Append($"{Name} has the {Cards.First().ToString()}");

                if (Cards.Count > 1)
                {
                    for(int i = 1; i < Cards.Count; i++)
                    {
                        var card = Cards[i];
                        display.Append($" and the {card.ToString()}");
                    }
                }

                display.Append($" worth {GetScore()} points.");
            }
            else
            {
                display.Append($"{Name} has no cards.");
            }

            return display.ToString();
        }

        public void DealOneCardToEveryone(List<Card> deck, List<Person> players)
        {
            if (this.IsDealer)
            {
                for (int i = 1; i <= players.Count; i++)
                {
                    if (deck.Any())
                    {
                        var card = deck[0];
                        players.FirstOrDefault(p => p.Number == i).ReceiveCard(card);
                        deck.Remove(card);
                    }
                }
            }
        }

        public int GetScore()
        {
            var score = 0;

            foreach (var card in Cards)
            {
                score += card.PointValue;
            }

            return score;
        }
    }
}
