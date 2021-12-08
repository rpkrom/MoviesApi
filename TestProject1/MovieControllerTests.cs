using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Controllers;
using MoviesApi.Models;
using MoviesApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MoviesTests
{
    public class MovieControllerTests
    {
        [Fact]
        public async void GetAllMovies_Returns_OkResult_NotNull()
        {
            // Arrange
            var dataStore = A.Fake<IMoviesService>();
            A.CallTo(() => dataStore.FindAll()).Returns(Task.FromResult(GetTestMovies()));
            var moviesController = new MoviesController(dataStore);

            // Act
            var actionResult = await moviesController.GetMovies();

            // Assert
            var result = actionResult.Result as OkObjectResult;
            var returnMovies = result.Value as IEnumerable<Movie>;

            Assert.Equal(GetTestMovies().Count, returnMovies.Count());
            Assert.NotNull(returnMovies);

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
