﻿using AutoMapper;
using BookStoreAPI.Controllers;
using BookStoreAPI.Data;
using BookStoreAPI.Data.Entities;
using BookStoreAPI.Data.Repository.Interfaces;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApiTests
{
    [TestFixture]
    internal class BookControllerTests
    {
        private IMapper _mapper;
        private Mock<IBookRepository> _mockBookRepo;
        private BookController _controller;

        [SetUp]
        public void Setup()
        {
            _mapper = new MapperConfiguration(cfg=>
            {
                cfg.AddProfile(new AppMappingProfile());
            }).CreateMapper();

            _mockBookRepo = new Mock<IBookRepository>();
            _controller = new BookController(_mapper, _mockBookRepo.Object);
        }

        [Test]
        public async Task GetBook_ExistingId_ReturnsOk()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var mockBook = new Book { id = bookId, Title = "Test Book", ReleaseDate = DateTime.Now };
            _mockBookRepo.Setup(repo => repo.GetByIdAsync(bookId)).ReturnsAsync(mockBook);

            // Act
            var result = await _controller.GetBook(bookId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult?.Value);
            Assert.IsInstanceOf<BookStoreAPI.Models.BookModel>(okResult.Value);
        }

        [Test]
        public async Task GetBook_NonExistentId_ReturnsNotFound()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();
            _mockBookRepo.Setup(repo => repo.GetByIdAsync(nonExistentId)).ReturnsAsync<IBookRepository, Book>((Book)null);

            // Act
            var result = await _controller.GetBook(nonExistentId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
        [Test]
        public async Task GetBookByFilter_ExistingData_ReturnsOk()
        {
            // Arrange
            var title = "Test Book";
            var releaseDate = DateTime.Now;
            var mockBooks = new List<Book>
            {
                new Book { id = Guid.NewGuid(), Title = title, ReleaseDate = releaseDate }
            };
            _mockBookRepo.Setup(repo => repo.GetBooksByFilterAsync(title, releaseDate)).ReturnsAsync(mockBooks);

            // Act
            var result = await _controller.GetBookByFilter(title, releaseDate);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult?.Value);
            Assert.IsInstanceOf<List<BookStoreAPI.Models.BookModel>>(okResult.Value);
        }
        [Test]
        public async Task GetBookByFilter_NoData_ReturnsNotFound()
        {
            // Arrange
            var title = "Nonexistent Book";
            var releaseDate = DateTime.Now;
            _mockBookRepo.Setup(repo => repo.GetBooksByFilterAsync(title, releaseDate))
                .ReturnsAsync<IBookRepository, List<Book>>((List<Book>)null);

            // Act
            var result = await _controller.GetBookByFilter(title, releaseDate);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }


    }
}
