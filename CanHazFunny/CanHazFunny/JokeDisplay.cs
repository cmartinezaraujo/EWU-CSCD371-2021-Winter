﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanHazFunny
{
    public class JokeDisplay : IJokeDisplay
    {
        public void DisplayJoke(string joke)
        {
            Console.WriteLine(joke);
        }
    }
}
