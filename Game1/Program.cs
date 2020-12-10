using System;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            GameLauncher gameLauncher = new GameLauncher();
            GameLauncher game = gameLauncher;
            game.StartGame(args);


        }
    }
}