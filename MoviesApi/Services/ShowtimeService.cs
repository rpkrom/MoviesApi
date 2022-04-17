using MoviesApi.Entity.Models;
using MoviesApi.Entity.DataContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Services
{
    // TODO: Stub out how to display showtimes, will need table data, model, and updated service
    public class ShowtimeService : IShowtimeService
    {
        private readonly MoviesDbContext _context;
        public ShowtimeService(MoviesDbContext context)
        {
            _context = context;
        }


        //public async Task<Showtime> Find(int id)
        //{
        //    return await _context.Showtimes.FindAsync(id);
        //}

        public List<Showtime> GetMoviesByTheaterId(int theaterId) // Get movie ID's by theaterId
        {
            var bb = _context.Showtimes.Select(x => x.Id).ToList(); 

            var x = (from m in _context.Showtimes
                     where m.TheaterId == theaterId
                     select m).ToList();
            //select new Showtime
            //{
            //    Id = m.Id,
            //    TheaterId = m.TheaterId,
            //    MovieId = m.MovieId

            //}).ToList();

            return x;
        }

        //public List<Theater> GetTheatersByMovieId(int movieId)
        //{
        //    return null;
        //}
    }
}
