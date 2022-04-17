using MoviesApi.Helpers;
using MoviesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Services
{
    public interface ITheaterService
    {
        public void Add(Theater theater);

        public Task Save();

        public Task<Theater> Find(int id);

        public List<Theater> FindAll();

        public PagedList<Theater> FindAll(MovieParameters movieParameters);

        public Task<Theater> Update(Theater theater);

        public void Delete(Theater theater);

        public bool MovieExists(int id);

    }
}
