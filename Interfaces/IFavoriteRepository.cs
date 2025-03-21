using BookFinderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookFinderAPI.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<Favorite?> GetFavoriteAsync(int userId, int bookId);
        Task<List<Favorite>> GetFavoritesByUserIdAsync(int userId);
        Task<bool> AddFavoriteAsync(int userId, int bookId); 
        Task<bool> DeleteFavoriteAsync(int userId, int id);
    }
}
