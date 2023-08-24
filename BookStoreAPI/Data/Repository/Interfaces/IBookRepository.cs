using BookStoreAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Data.Repository.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
    }
}
