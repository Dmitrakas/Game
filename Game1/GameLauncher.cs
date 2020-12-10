using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Security.Cryptography;

namespace Game
{
    public class GameLauncher
    {
        private GameModel GameModel;

        private string computerMove;

        public string getKey(int size)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var key = new byte[size];
                rng.GetBytes(key);
                return Convert.ToBase64String(key).ToUpper();
            }

        }

        public static string HMACHASH(string str, string key)
        {
            byte[] bkey = Encoding.Default.GetBytes(key);
            using (var hmac = new HMACSHA256(bkey))
            {
                byte[] bstr = Encoding.Default.GetBytes(str);
                var bhash = hmac.ComputeHash(bstr);
                return Convert.ToBase64String(hmac.ComputeHash(bstr)).ToUpper();

            }
        }

        public void StartGame(string[] args)
        {
            List<string> moves = new List<string>(args);

            try
            {

                if (moves.Count < 3 || moves.Count % 2 == 0 || moves == moves.Distinct().ToList())
                {
                    throw new Exception("Wrong format. Example: 1->2->3->... (rock->scissors->paper->...)");
                }

                GameModel = new GameModel(moves);

                while (true)
                {
                    computerMove = GameModel.Moves[new Random().Next(0, GameModel.Moves.Count)];
                    StartMenu();
                    startParty();

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void StartMenu()
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine(HMACHASH(computerMove, getKey(16)));
            Console.WriteLine("\nAvailable moves:");

            for (int i = 0; i < GameModel.Moves.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {GameModel.Moves[i]}");
            }

            Console.WriteLine("0 - exit");
        }

        private void startParty()
        {
            Console.WriteLine("Enter your move: ");
            int selection = Int32.Parse(Console.ReadLine());

            if (selection == 0)
            {
                Environment.Exit(0);
            }

            if (selection < 0 || selection > GameModel.Moves.Count)
            {
                StartMenu();
                return;
            }

            Console.WriteLine($"Your move: {GameModel.Moves[selection - 1]}");

            Console.WriteLine($"Computer move: {computerMove}");

            Console.WriteLine(GameModel.ProvideWinner(GameModel.Moves[selection - 1], computerMove));
            Console.WriteLine($"HMAC key: {getKey(16)}");

        }
    }
}