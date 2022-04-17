using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesApi.Entity.Models
{
    [Table("Theater")]
    public class Theater
    {
        public Theater() { }

        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Location { get; set; }

    }
}
