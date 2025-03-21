using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookFinderAPI.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookFinderAPI.Controllers
{
    [Route("api/favorites")]
    [ApiController]
    [Authorize] // Requires JWT authentication
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoriteController(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        // Get User ID from JWT Token
        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        // Add a Book to Favorites
        [HttpPost]
        public async Task<IActionResult> AddToFavorites([FromBody] int bookId)
        {
            int userId = GetUserId();
            var added = await _favoriteRepository.AddFavoriteAsync(userId, bookId);

            if (!added)
                return BadRequest("Book is already in favorites.");

            return Ok("Book added to favorites.");
        }
         
        [HttpGet]
        public async Task<IActionResult> GetFavorites()
        {
            int userId = GetUserId();
            var favorites = await _favoriteRepository.GetFavoritesByUserIdAsync(userId);

            return Ok(favorites);
        }
         
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFromFavorites(int id)
        {
            int userId = GetUserId();
            var removed = await _favoriteRepository.DeleteFavoriteAsync(userId, id);

            if (!removed)
                return NotFound("Favorite not found.");

            return Ok("Book removed from favorites.");
        }
    }
}
