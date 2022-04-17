using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Controllers;
using MoviesApi.Models;
using MoviesApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace MoviesTests
{
    public class MovieControllerTests
    {
        public string FakeTitle() => "Fake Movie Title"; // method that returns "string". same as FakeTitle(){var string; return string;}

        public int FakeCounter()
        {
            int counter = 0;

            for (int i = 0; i < 10; i++)
            {
                counter += i; // 
            }
            return counter;
        }

        [Fact]
        public void GetAllMovies_Returns_OkResult_NotNull()
        {
            //// Arrange
            //var dataStore = A.Fake<IMoviesService>();
            //var param = new MovieParameters();

            //A.CallTo(() => dataStore.FindAll(param)).Returns(GetTestMovies());
            //var moviesController = new MoviesController(dataStore);

            //// Act
            //var actionResult = moviesController.GetMovies(param);

            //var result = actionResult.Result as OkObjectResult;
            //var returnMovies = result.Value as IEnumerable<Movie>;

            //// Assert
            //Assert.Equal(GetTestMovies().Count, returnMovies.Count());
            //Assert.NotNull(returnMovies);

        }

        [Fact]
        public async Task GetAllMovies_Returns_1()
        {
            // Arrange
            var dataStore = A.Fake<IMoviesService>();
            var fakeMovie = GetTestMovies().FirstOrDefault();

            A.CallTo(() => dataStore.Find(fakeMovie.Id)).Returns(fakeMovie);
            var moviesController = new MoviesController(dataStore);

            // Act
            var actionResult = await moviesController.GetMovie(fakeMovie.Id);

            var result = actionResult.Result as OkObjectResult;
            var returnMovie = result.Value as Movie;

            // Assert
            Assert.Equal(fakeMovie, returnMovie);
            Assert.NotNull(returnMovie);
        }

        [Fact]
        public async Task DeleteMovie_Returns_NoContent()
        {
            // Arrange
            var dataStore = A.Fake<IMoviesService>();
            var fakeMovie = GetTestMovies().FirstOrDefault();

            A.CallTo(() => dataStore.Find(fakeMovie.Id)).Returns(fakeMovie);
            var moviesController = new MoviesController(dataStore);

            // Act
            var actionResult = await moviesController.DeleteMovie(fakeMovie.Id); // Id 1

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async Task DeleteMovie_BadId_Returns_NotFound()
        {
            // Arrange
            var dataStore = A.Fake<IMoviesService>();
            var fakeMovie = GetTestMovies().FirstOrDefault();

            A.CallTo(() => dataStore.Find(fakeMovie.Id)).Returns(fakeMovie);
            var moviesController = new MoviesController(dataStore);

            // Act
            var actionResult = await moviesController.DeleteMovie(99999); // fake id

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async Task PostMovie_Returns_201()
        {
            // Arrange
            var dataStore = A.Fake<IMoviesService>();
            var fakeMovie = GetTestMovies().FirstOrDefault();

            A.CallTo(() => dataStore.Find(fakeMovie.Id)).Returns(fakeMovie);
            var moviesController = new MoviesController(dataStore);

            // Act
            var actionResult = await moviesController.PostMovie(fakeMovie); // Id 1
            var result = actionResult.Result as CreatedAtActionResult;

            // Assert
            Assert.Equal(result.StatusCode, (int)HttpStatusCode.Created);
        }


        // Fake Movies Setup
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
