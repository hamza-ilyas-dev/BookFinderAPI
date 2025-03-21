using BookFinderAPI.Data;
using BookFinderAPI.Models;
using BookFinderAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookFinderAPI.Repository
{
    public class BookRepository : IBookRepository
    { 
        private readonly AppDbContext _context;
        public BookRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Book>> GetBookAsync(string search)
        {
            return await _context.Books
                .Where(b => b.Title.Contains(search) || b.Author.Contains(search))
                .ToListAsync();
        }
        public async Task<Book?> GetBookIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);  
        }
    }
}
