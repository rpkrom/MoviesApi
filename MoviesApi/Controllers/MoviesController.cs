using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Attributes;
using MoviesApi.Models;
using MoviesApi.Services;

namespace MoviesApi.Controllers
{
    [Route("api/Movies")]
    [ApiController]
    //[ApiKey]
    public class MoviesController : ControllerBase
    {
        //private readonly MoviesDbContext _context;
        private readonly IMoviesService moviesService;

        public MoviesController(IMoviesService moviesService)
        {
            //_context = context;
            this.moviesService = moviesService;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            var movies = await this.moviesService.FindAll();
            return Ok(movies);
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await this.moviesService.Find(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
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
                await this.moviesService.Update(movie);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.moviesService.MovieExists(id))
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
            this.moviesService.Add(movie);
            await this.moviesService.Save();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        //PATCH: api/Movies/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchMovie(int id, JsonPatchDocument<Movie> movieUpdates) 
        {
            var movie = await this.moviesService.Find(id);

            if (movie == null)
            {
                return NotFound();
            }

            movieUpdates.ApplyTo(movie);// this is how you patch
            await this.moviesService.Save();

            return NoContent();
        }


        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await this.moviesService.Find(id);
            if (movie == null)
            {
                return NotFound();
            }

            this.moviesService.Delete(movie);
            await this.moviesService.Save();

            return NoContent();
        }

    }
}
