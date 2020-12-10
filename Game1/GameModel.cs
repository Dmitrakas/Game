using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public class GameModel
    {

        public List<string> Moves { get; set; }
        public GameModel(List<string> moves)
        {
            Moves = moves;
        }

        public string ProvideWinner(string moveHuman, string moveComputer)
        {
            var value = Moves.IndexOf(moveHuman) - Moves.IndexOf(moveComputer);
            var count = Moves.Count - 1;
            if (value == 0)
            {
                return "Tie.";
            }
            else if ((value >=0 && value < count / 2) || (value >= -count && value < -count / 2 ))
            {
                return "You Win!";

            }
            else

                return "You Lose.";
        }
    }
}