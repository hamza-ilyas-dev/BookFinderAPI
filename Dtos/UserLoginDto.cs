using System.ComponentModel.DataAnnotations;

namespace BookFinderAPI.Dtos
{
    public class UserLoginDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
