using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using LibraryBE.Controllers;
using LibraryBE.DTO;
using LibraryBE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBE.Tests.Controller
{
    public class BookControllerTest
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public BookControllerTest()
        {
            _bookRepository = A.Fake<IBookRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void BookController_GetBooks_ReturnOK()
        {
            //Arrange 
            var books = A.Fake<ICollection<BookDto>>();
            var bookList = A.Fake<List<BookDto>>();
            A.CallTo(() => _mapper.Map<List<BookDto>>(books)).Returns(bookList);
            var controller = new BookController(_bookRepository, _mapper);
            //Act
            var result = controller.GetBooks();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Microsoft.AspNetCore.Mvc.OkObjectResult));
        }

        [Fact]
        public void BookController_GetBook_ReturnOK()
        {
            // Arrange
            int bookId = 1;
            var book = A.Fake<BookDto>();
            A.CallTo(() => _bookRepository.BookExists(bookId)).Returns(true);
            A.CallTo(() => _mapper.Map<BookDto>(_bookRepository.GetBook(bookId))).Returns(book);
            var controller = new BookController(_bookRepository, _mapper);

            // Act
            var result = controller.GetBook(bookId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Microsoft.AspNetCore.Mvc.OkObjectResult));
        }

        [Fact]
        public void BookController_GetBook_ReturnNotFound()
        {
            // Arrange
            int bookId = 1;
            A.CallTo(() => _bookRepository.BookExists(bookId)).Returns(false);
            var controller = new BookController(_bookRepository, _mapper);

            // Act
            var result = controller.GetBook(bookId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Microsoft.AspNetCore.Mvc.NotFoundResult));
        }
    }
}
