using Microsoft.EntityFrameworkCore;
using BookFinderAPI.Models;

namespace BookFinderAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            base.OnModelCreating(modelBuilder);
        }
    }
}
