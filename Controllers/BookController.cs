using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using libraryapp.Models;
using libraryVueApp.Data;
using libraryVueApp.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace libraryVueApp.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            this._mapper = mapper;
        }

        // GET api/books
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            return Ok(_bookRepository.GetBooks());
        }
        // GET api/books/{id}
        [HttpGet("{id}", Name = "GetBookById")]
        public ActionResult<Book> GetBookById(int id)
        {
            return Ok(_bookRepository.GetBook(id));
        }

        //POST api/books
        [HttpPost]
        public ActionResult<Book> AddBook([FromBody]CreateBookDto createBookDto)
        {
            Book book = _mapper.Map<Book>(createBookDto);
            _bookRepository.AddBook(book);
            _bookRepository.SaveChanges();
            return CreatedAtRoute(nameof(GetBookById), new { book.Id }, book);
        }

        //PUT api/books
        [HttpPut("{Id}")]
        public ActionResult<Book> UpdateBook(int id, Book book)
        {
            _bookRepository.Update(book);
            _bookRepository.SaveChanges();
            return NoContent();
        }


        //DELETE api/books
        [HttpDelete("{id}")]
        public ActionResult<Book> DeleteBookById(int id)
        {
            Book book = _bookRepository.GetBook(id);
            if (book == null)
                return NotFound();

            _bookRepository.Delete(book);
            _bookRepository.SaveChanges();
            return NoContent();
        }
    }
}