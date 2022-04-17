using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Attributes;
using MoviesApi.Entity.Models;
using MoviesApi.Services;
using Newtonsoft.Json;

namespace MoviesApi.Controllers
{
    [Route("api/Movies")]
    [ApiController]
    //[ApiKey]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _moviesService;

        public MoviesController(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        // GET: api/Movies
        [HttpGet]
        public ActionResult<IEnumerable<Movie>> GetMovies([FromQuery] MovieParameters movieParameters) 
        {
            var movies = _moviesService.FindAll(movieParameters);

            var metadata = new
            {
                movies.TotalCount,
                movies.PageSize,
                movies.CurrentPage,
                movies.HasNext,
                movies.HasPrevious
            };
            Response.Headers.Add("TestHeader", "TestContent"); // how to add headers
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(movies);
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id) 
        {
            var movie = await _moviesService.Find(id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }


        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            try
            {
                await _moviesService.Update(movie);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_moviesService.MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _moviesService.Add(movie);
            await _moviesService.Save();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        //PATCH: api/Movies/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchMovie(int id, JsonPatchDocument<Movie> movieUpdates) 
        {
            var movie = await _moviesService.Find(id);

            if (movie == null)
            {
                return NotFound();
            }

            movieUpdates.ApplyTo(movie);
            // this is how you patch 
            // "path": "/duration",   // NAME of property
            // "op": "replace",       // Action to perform
            // "value": "3h 30 mins"  // New value
            await _moviesService.Save();

            return NoContent();
        }


        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _moviesService.Find(id);
            if (movie == null || movie.Id != id)
            {
                return NotFound();
            }

            _moviesService.Delete(movie);
            await _moviesService.Save();

            return NoContent();
        }

    }
}
