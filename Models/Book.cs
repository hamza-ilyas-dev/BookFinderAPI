using System.ComponentModel.DataAnnotations;

namespace BookFinderAPI.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        public string CoverImageUrl { get; set; }
    }
}
