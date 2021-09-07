using Moq;
using MoviesApi.Controllers;
using MoviesApi.Models;
using MoviesApi.Services;
using NUnit.Framework;
using System.Collections.Generic;

namespace MoviesTests
{
    public class Tests
    {
        MoviesService mService;
        MoviesDbContext dbContext;

        readonly Mock<MoviesDbContext> mockDbContext = new Mock<MoviesDbContext>();


        [SetUp]
        public void Setup()
        {
            dbContext = mockDbContext.Object;

            mService = new MoviesService(dbContext);
        }

        [Test]
        public void Test1()
        {
            var testMovies = GetTestMovies();

            foreach (var item in testMovies)
            {
                mService.Add(item);
            }

            mService.Save();

            var allmovies = mService.FindAll();

            Assert.IsTrue(allmovies.Result.Count == testMovies.Count);

            Assert.Pass();
        }

        private List<Movie> GetTestMovies()
        {
            var testMovies = new List<Movie>();
            testMovies.Add(new Movie { Id = 1, Duration = "3h 00 mins", Genre = "action", Name = "Test 1", ReleaseDate = System.DateTime.Now });
            testMovies.Add(new Movie { Id = 2, Duration = "3h 00 mins", Genre = "action", Name = "Test 2", ReleaseDate = System.DateTime.Now });
            testMovies.Add(new Movie { Id = 3, Duration = "3h 00 mins", Genre = "action", Name = "Test 3", ReleaseDate = System.DateTime.Now });
            testMovies.Add(new Movie { Id = 4, Duration = "3h 00 mins", Genre = "action", Name = "Test 4", ReleaseDate = System.DateTime.Now });

            return testMovies;
        }
    }
}