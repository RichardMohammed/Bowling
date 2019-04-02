using System;

namespace Bowling.Client
{
    public class Program
    {
        private static IBowling _bowling;
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Bowling 2000\n\nPlease enter your name");
            var playerName = Console.ReadLine();
            var ball = Ball.Instance;

            _bowling = new Bowling(playerName);

            Console.WriteLine($"\nWelcome {playerName}! Press any key to roll your first ball.");
            Console.ReadLine();
            _bowling.Bowl(ball.Roll());

            while (_bowling.Status() != GameStatus.Ended)
            {
                Console.WriteLine($"Your current score is {_bowling.TotalScore()}");

                Console.WriteLine("Press Enter to roll your next ball...");
                Console.ReadLine();
                _bowling.Bowl(ball.Roll());
            }

            Console.WriteLine($"Congratulations! You scored {_bowling.TotalScore()}");
            Console.ReadLine();
            Console.ReadLine();
        }
    }
}
