using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Helpers;
using MoviesApi.Entity.Models;
using MoviesApi.Entity.DataContext;

namespace MoviesApi.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly MoviesDbContext _context;

        public MoviesService(MoviesDbContext context)
        {
            _context = context;
        }

        public Task Save()
        {
            return _context.SaveChangesAsync();
        }

        public void Add(Movie movie)
        {
            _context.Add(movie);
        }

        public async Task<Movie> Find(int id)
        {
            return await _context.Movies.FindAsync(id);
        }

        public List<Movie> FindAll()
        {
            return _context.Movies.OrderBy(m => m.Name).ToList();
        }

        public PagedList<Movie> FindAll(MovieParameters movieParameters)
        {
            return PagedList<Movie>.ToPagedList(FindAll(), movieParameters.PageNumber, movieParameters.PageSize);

            //return  _context.Movies.OrderBy(m => m.Name)
            //   .Skip((movieParameters.PageNumber - 1) * movieParameters.PageSize)
            //   .Take(movieParameters.PageSize)
            //   .ToListAsync();            
        }

        public async Task<Movie> Update(Movie movie)
        {
            _context.Entry(movie).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return movie;
        }

        public void Delete(Movie movie)
        {
            _context.Remove(movie);
        }

        public bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }

        public List<Movie> MoviesByTheaterId(int theaterId)
        {
            var sql = (from m in _context.Movies
                     where m.TheaterId == theaterId
                     select new Movie
                     {
                         Id = m.Id,
                         Name = m.Name,
                         Genre = m.Genre,
                         Duration = m.Duration,
                         ReleaseDate = m.ReleaseDate,
                         TheaterId = m.TheaterId
                     }).ToList();

            return sql;
        }

        public List<Movie> MoviesByShowtimeTheaterId(int theaterId)  // Proper way to filter // TODO: Move this to ShowtimeService
        {
            var sql = (from mo in _context.Movies 
                       join st in _context.Showtimes on mo.Id equals st.MovieId
                       where st.TheaterId == theaterId
                       select mo).ToList();

            return sql;
        }

    }
}
