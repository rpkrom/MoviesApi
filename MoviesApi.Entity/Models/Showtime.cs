namespace MoviesApi.Entity.Models
{
    public class Showtime
    {
        public int Id { get; set; } 
        public int TheaterId { get; set; }
        public int MovieId { get; set; }
    }
}
