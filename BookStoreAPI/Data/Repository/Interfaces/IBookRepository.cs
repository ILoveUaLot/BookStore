using BookStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Data.Repository.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
    }
}
