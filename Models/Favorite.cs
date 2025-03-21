using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookFinderAPI.Models
{
    public class Favorite
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }

        // Navigation property
        [ForeignKey("BookId")]
        public Book Book { get; set; }
    }
}
