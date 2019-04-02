using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling.Client
{
    public class Ball
    {
        private static Ball _instance;
        private readonly Random _rand;

        static Ball() { }

        private Ball()
        {
            _rand = new Random();
        }

        public static Ball Instance => _instance ?? (_instance = new Ball());

        public int Roll()
        {
            return _rand.Next(0, 10);
        }
    }
}
