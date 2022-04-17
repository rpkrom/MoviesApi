using Microsoft.EntityFrameworkCore;
using MoviesApi.Helpers;
using MoviesApi.Entity.Models;
using MoviesApi.Entity.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Services
{
    public class TheaterService : ITheaterService
    {
        private readonly MoviesDbContext _context;

        public TheaterService(MoviesDbContext context)
        {
            _context = context;
        }

        public Task Save()
        {
           return _context.SaveChangesAsync();
        }

        public void Add(Theater theater)
        {
            _context.Add(theater);
        }

        public async Task<Theater> Find(int id)
        {
             return await _context.Theaters.FindAsync(id);
        }
                
        public List<Theater> FindAll()
        {
            return _context.Theaters.OrderBy(t => t.Name).ToList();
        }
               
        public PagedList<Theater> FindAll(MovieParameters movieParameters)
        {
            return PagedList<Theater>.ToPagedList(FindAll(), movieParameters.PageNumber, movieParameters.PageSize);

             //return  _context.Movies.OrderBy(m => m.Name)
             //   .Skip((movieParameters.PageNumber - 1) * movieParameters.PageSize)
             //   .Take(movieParameters.PageSize)
             //   .ToListAsync();            
        }

        public async Task<Theater> Update(Theater theater)
        {
            _context.Entry(theater).State = EntityState.Modified;

            await _context.SaveChangesAsync(); 

            return theater;
        }

        public void Delete(Theater theater)
        {
            _context.Remove(theater);
        }

        public bool MovieExists(int id)
        {
            return _context.Theaters.Any(e => e.Id == id);
        }
                
    }
}
