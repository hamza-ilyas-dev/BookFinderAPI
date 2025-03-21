using BookFinderAPI.Data;
using BookFinderAPI.Interfaces;
using BookFinderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookFinderAPI.Repository
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly AppDbContext _context;

        public FavoriteRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get a specific favorite by UserId and BookId
        public async Task<Favorite?> GetFavoriteAsync(int userId, int bookId)
        {
            return await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.BookId == bookId);
        }

        // Get all favorite books for a user
        public async Task<List<Favorite>> GetFavoritesByUserIdAsync(int userId)
        {
            return await _context.Favorites
                .Where(f => f.UserId == userId)
                .Include(f => f.Book)  
                .ToListAsync();
        }

        // Add a book to favorites
        public async Task<bool> AddFavoriteAsync(int userId, int bookId)
        { 
            var bookExists = await _context.Books.AnyAsync(b => b.Id == bookId);
            if (!bookExists)
            {
                return false;  
            }

            var favorite = new Favorite { UserId = userId, BookId = bookId };
            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();

            return true;  
        }
         
        // Remove a book from favorites
        public async Task<bool> DeleteFavoriteAsync(int userId, int id)
        {
            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.Id == id && f.UserId == userId);

            if (favorite == null)
                return false;

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
