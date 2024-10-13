using AutoMapper;
using LibraryBE.DTO;
using LibraryBE.Interfaces;
using LibraryBE.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        public IActionResult GetBooks()
        {
            var books = _mapper.Map<List<BookDto>>(_bookRepository.GetAllBooks());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(books);
        }

        [HttpGet("{Bookid}")]
        [ProducesResponseType(200, Type = typeof(Book))]
        [ProducesResponseType(400)]
        public IActionResult GetBook(int Bookid)
        {
            if (!_bookRepository.BookExists(Bookid))
            {
                return NotFound();
            }
            var book = _mapper.Map<BookDto>(_bookRepository.GetBook(Bookid));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(book);
        }






    }
}
