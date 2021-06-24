using System.Text;

namespace AndrosCardGame
{
    public class Card
    {
        public int SuitNumber { get; set; }
        public int PointValue { get; set; }

        public override string ToString()
        {
            var name = new StringBuilder();

            if (this.PointValue == 2) name.Append("Two");
            if (this.PointValue == 3) name.Append("Three");
            if (this.PointValue == 4) name.Append("Four");
            if (this.PointValue == 5) name.Append("Five");
            if (this.PointValue == 6) name.Append("Six");
            if (this.PointValue == 7) name.Append("Seven");
            if (this.PointValue == 8) name.Append("Eight");
            if (this.PointValue == 9) name.Append("Nine");
            if (this.PointValue == 10) name.Append("Ten");
            if (this.PointValue == 11) name.Append("Jack");
            if (this.PointValue == 12) name.Append("Queen");
            if (this.PointValue == 13) name.Append("King");
            if (this.PointValue == 14) name.Append("Ace");

            name.Append(" of ");

            if (this.SuitNumber == 0) name.Append("Hearts");
            if (this.SuitNumber == 1) name.Append("Clubs");
            if (this.SuitNumber == 2) name.Append("Diamonds");
            if (this.SuitNumber == 3) name.Append("Spades");

            return name.ToString();
        }
    }
}
