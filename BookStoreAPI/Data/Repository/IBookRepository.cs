using BookStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Data.Repository
{
    public interface IBookRepository : IRepository<Book>
    {
    }
}
