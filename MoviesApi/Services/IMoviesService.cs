using MoviesApi.Helpers;
using MoviesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Services
{
    public interface IMoviesService
    {
        public void Add(Movie movie);

        public Task Save();

        public Task<Movie> Find(int id);

        public List<Movie> FindAll();

        public PagedList<Movie> FindAll(MovieParameters movieParameters);

        public Task<Movie> Update(Movie movie);
        
        public void Delete(Movie movie);

        public bool MovieExists(int id);

        public List<Movie> MoviesByTheaterId(int theaterId);

        public List<Movie> MoviesByShowtimeTheaterId(int theaterId);

    }
}
