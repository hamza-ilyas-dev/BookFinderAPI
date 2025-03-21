using BookFinderAPI.Models;

namespace BookFinderAPI.Interfaces
{
    public interface IBookRepository
    {
         Task<List<Book>> GetBookAsync(string search);
         Task<Book?> GetBookIdAsync(int id);
    }
}
