using Microsoft.EntityFrameworkCore;
using MoviesApi.Entity.Models;

namespace MoviesApi.Entity.DataContext
{
    public class MoviesDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Theater> Theaters { get; set; }
        public DbSet<Showtime> Showtimes { get; set; }

        public MoviesDbContext() { }

        public MoviesDbContext(DbContextOptions<MoviesDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().ToTable("Movie");
            modelBuilder.Entity<Theater>().ToTable("Theater");
            modelBuilder.Entity<Showtime>().ToTable("Showtime");
        }
    }
}
