using System.ComponentModel.DataAnnotations;
namespace BookFinderAPI.Dtos
  
{
    // DTOs for Register and Login
    public class UserRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
   
}
