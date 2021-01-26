using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanHazFunny
{
    public class Jester
    {
        private IJokeService _JokeService;
        private IJokeDisplay _DisplayService;

        public IJokeService JokeService { get => _JokeService; }

        public IJokeDisplay DisplayService { get => _DisplayService; }

        public Jester(IJokeService jokeService, IJokeDisplay tellerService)
        {
            _JokeService = jokeService ?? throw new System.ArgumentNullException(nameof(jokeService));

            _DisplayService = tellerService ?? throw new System.ArgumentNullException(nameof(tellerService));
        }

        public void TellJoke()
        {
            string joke = JokeService.GetJoke();

            while(joke.Contains("Chuck") | joke.Contains("Norris"))
            {
                joke = JokeService.GetJoke();
            }

            DisplayService.DisplayJoke(joke);
        }
    }
}
