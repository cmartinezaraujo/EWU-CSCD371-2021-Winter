using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace CanHazFunny.Tests
{
    [TestClass]
    public class JesterTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Jester_TakesNullJokeService_ThrowsArgumentException()
        {
            Jester joker = new Jester(null, new JokeDisplay());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Jester_TakesNullJokeDisplay_ThrowsArgumentException()
        {
            Jester joker = new Jester(new JokeService(), null);
        }

        [TestMethod]
        public void Jester_TakesValidServices()
        {
            JokeService jokeService= new();
            JokeDisplay jokeDisplay = new();

            Jester joker = new Jester(jokeService, jokeDisplay);

            Assert.AreEqual(joker.JokeService,jokeService);
            Assert.AreEqual(joker.DisplayService, jokeDisplay);
        }

        [TestMethod]
        public void Jester_TellsJoke()
        {
            Mock<IJokeService> mockJokeService = new();
            mockJokeService.Setup(JokeService => JokeService.GetJoke()).Returns("I am joke");

            Mock<IJokeDisplay> mockDisplayService = new();
            mockDisplayService.Setup(DisplayService => DisplayService.DisplayJoke("I am joke"));


            Jester joker = new Jester(mockJokeService.Object, mockDisplayService.Object);

            joker.TellJoke();

            mockDisplayService.VerifyAll();
        }

        [TestMethod]
        public void Jester_FiltersOutChuckNorris()
        {
            Mock<IJokeService> mockJokeService = new();
            mockJokeService.SetupSequence(JokeService => JokeService.GetJoke())
                .Returns("I am Chuck")
                .Returns("I am Norris")
                .Returns("I am joke");

            Jester joker = new Jester(mockJokeService.Object, new JokeDisplay());

            joker.TellJoke();

            mockJokeService.Verify(JokeService => JokeService.GetJoke(), Times.Exactly(3));


        }


    }
}
