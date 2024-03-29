﻿using BookStoreAPI.Data.Entities;
using BookStoreAPI.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Data.Repository
{
    public class BookRepository : IBookRepository
    {
        public readonly BookStoreContext db;
        public BookRepository(BookStoreContext dbContext)
        {
            db = dbContext;
        }
        public async Task CreateAsync(Book entity)
        {
            await db.Books.AddAsync(entity);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Book entity)
        {
            db.Books.Remove(entity);
            await db.SaveChangesAsync();
        }

        public IQueryable<Book> GetAll()
        {
            return db.Books;
        }

        public async Task<List<Book>> GetBooksByFilterAsync(string title, DateTime? releaseDate)
        {
            IQueryable<Book> booksQuery = db.Books;
            if(!string.IsNullOrEmpty(title))
            {
                booksQuery = booksQuery.Where(book=>book.Title.Contains(title));
            }
                
            if(releaseDate != null)
            {
                booksQuery = booksQuery.Where(book=>book.ReleaseDate.Date >= releaseDate);
            }
            return await booksQuery.ToListAsync();
        }

        public async Task<Book> GetByIdAsync(Guid id)
        {
            return await db.Books.FindAsync(id);
        }

        public async Task UpdateAsync(Book entity)
        {
            db.Books.Update(entity);
            await db.SaveChangesAsync();
        }

    }
}
