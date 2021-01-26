namespace CanHazFunny
{
    class Program
    {
        static void Main(string[] args)
        {

            Jester joker = new(new JokeService(), new JokeDisplay());
            joker.TellJoke();

        }
    }
}
