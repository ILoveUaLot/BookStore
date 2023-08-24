using BookStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions opts) : base(opts)
        {

        }

        DbSet<Book> Books { get; set; }
        DbSet<Order> Orders { get; set; }
    }
}
