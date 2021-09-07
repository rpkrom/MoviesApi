using Microsoft.EntityFrameworkCore;
using MoviesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            _context.Movies.Add(movie);
        }

        public async Task<Movie> Find(int id)
        {
             return await _context.Movies.FindAsync(id);
        }
        
        public Task<List<Movie>> FindAll()
        {
             return  _context.Movies.ToListAsync();            
        }

        public async Task<Movie> Update(Movie movie)
        {
            _context.Entry(movie).State = EntityState.Modified;

            await Save();

            return movie;
        }

        public void Delete(Movie movie)
        {
            _context.Movies.Remove(movie);
        }

        public bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
