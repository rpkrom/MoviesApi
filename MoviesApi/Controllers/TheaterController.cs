using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Models;
using MoviesApi.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesApi.Controllers
{
    [Route("api/Theaters")]
    [ApiController]
    public class TheaterController : ControllerBase
    {
        private readonly ITheaterService _theatersService;
        private readonly IMoviesService _moviesService;
        private readonly IShowtimeService _showtimeService;

        public TheaterController(ITheaterService theatersService, IMoviesService moviesService, IShowtimeService showtimeService)
        {
            _theatersService = theatersService;
            _moviesService = moviesService;
            _showtimeService = showtimeService;
        }

        // GET: api/Movies
        [HttpGet]
        public ActionResult<IEnumerable<Theater>> GetTheaters([FromQuery] MovieParameters movieParameters)
        {
            var theaters = _theatersService.FindAll(movieParameters);

            foreach (var item in theaters)
            {
                var movies = GetMoviesByTheater(item);

                item.Movies = movies;               
            }

            var metadata = new
            {
                theaters.TotalCount,
                theaters.PageSize,
                theaters.CurrentPage,
                theaters.HasNext,
                theaters.HasPrevious
            };
            Response.Headers.Add("TestHeader", "TestContent");
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(theaters);
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Theater>> GetTheater(int id)
        {
            var theater = await _theatersService.Find(id);

            var movies = GetMoviesByTheater(theater);

            if (theater == null)
            {
                return NotFound();
            }

            theater.Movies = movies;

            return Ok(theater);
        }

        private List<Movie> GetMoviesByTheater(Theater theater)
        {
            //var movies = _moviesService.MoviesByTheaterId(theater.Id);
            var movies2 = _moviesService.MoviesByShowtimeTheaterId(theater.Id);

            return movies2;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Theater theater)
        {
            if (id != theater.Id)
            {
                return BadRequest();
            }

            try
            {
                await _theatersService.Update(theater);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_theatersService.MovieExists(id))
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
        public async Task<ActionResult<Theater>> PostMovie(Theater theater)
        {
            _theatersService.Add(theater);
            await _theatersService.Save();

            return CreatedAtAction("GetTheater", new { id = theater.Id }, theater);
        }

        //PATCH: api/Movies/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchTheater(int id, JsonPatchDocument<Theater> theaterUpdates)
        {
            var theater = await _theatersService.Find(id);

            if (theater == null)
            {
                return NotFound();
            }

            theaterUpdates.ApplyTo(theater);// this is how you patch
            await _theatersService.Save();

            return NoContent();
        }


        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var theater = await _theatersService.Find(id);
            if (theater == null)
            {
                return NotFound();
            }

            _theatersService.Delete(theater);
            await _theatersService.Save();

            return NoContent();
        }
    }
}
