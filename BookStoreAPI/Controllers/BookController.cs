using AutoMapper;
using BookStoreAPI.Data.Entities;
using BookStoreAPI.Data.Repository.Interfaces;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        IMapper _mapper;
        IBookRepository _bookRepo;
        public BookController(IMapper mapper, IBookRepository bookRepository)
        {
            _mapper = mapper;
            _bookRepo = bookRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(Guid id)
        {
            Book bookEntity = await _bookRepo.GetByIdAsync(id);
            if (bookEntity == null) return NotFound();

            BookModel bookModel = _mapper.Map<BookModel>(bookEntity);
            return Ok(bookModel);
        }

        [HttpGet("{title}/{releaseDate}")]
        public async Task<IActionResult> GetBookByFilter(string title, DateTime releaseDate)
        {
            List<Book> booksEntities = await _bookRepo.GetBooksByFilterAsync(title, releaseDate);
            if (booksEntities == null) return NotFound();
            List<BookModel> booksList = _mapper.Map<List<BookModel>>(booksEntities);
            return Ok(booksList);
        }
    }
}
