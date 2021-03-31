using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using libraryapp.Models;
using libraryVueApp.Data;
using libraryVueApp.Dtos;
using libraryVueApp.Dtos.BookDtos;
using libraryVueApp.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace libraryVueApp.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/books")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IBookOrderRepository _bookOrderRepository;
        private readonly IUserRepository _userRepository;

        public BookController(IBookRepository bookRepository, IMapper mapper, IBookOrderRepository bookOrderRepository, IUserRepository userRepository)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _bookOrderRepository = bookOrderRepository;
            _userRepository = userRepository;
        }

        //// GET api/books
        //[HttpGet]
        //public ActionResult<IEnumerable<Book>> GetAllBooks()
        //{
        //    return Ok(_bookRepository.GetBooks());
        //}
        [HttpGet]
        public ActionResult<IEnumerable<BookViewModel>> GetAllBooksExtended([FromHeader]int userId)
        {
            List<Book> books = _bookRepository.GetBooks().ToList();
            List<BookOrder> bookOrders = _bookOrderRepository.GetBookOrders().ToList();

            BookViewModelBuilder bookViewModelBuilder = new BookViewModelBuilder(books, bookOrders, userId);
            IEnumerable<BookViewModel> booksViewModels = bookViewModelBuilder.Build();

            return Ok(booksViewModels);
        }
        [HttpGet("{bookId}/queue")]
        public ActionResult<IEnumerable<BookOrderViewModel>> GetQueueDetails(int bookId)
        {
            List<BookOrder> bookOrders = _bookOrderRepository.GetBookOrders(bookId, true);

            IEnumerable<BookOrderViewModel> bookOrderViewModels = bookOrders
                .Select(bo => new BookOrderViewModel
                {
                    Login = bo.User.Login,
                    Firstname = bo.User.Firstname,
                    Lastname = bo.User.Lastname,
                    UserId = bo.User.Id,
                    Status = bo.OrderStatus,
                    BookOrderId = bo.Id
                })
                .OrderBy(vm => (int)vm.Status);                

            return Ok(bookOrderViewModels);
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
        [HttpPut("{bookId}/order")]
        public ActionResult<RequestBookResult> RequestBook(int bookId, [FromHeader]int userId)
        {
            Book book = _bookRepository.GetBook(bookId);
            if (book == null)
                return NotFound("Book does not exist");
            User user = _userRepository.GetUserById(userId);
            if(user == null)
                return NotFound("User that requested book does not exist"); //very unlikely

            RequestBookResult result = _bookOrderRepository.RequestBook(book, user);

            if (_bookOrderRepository.SaveChanges())
                return Ok(result);

            return StatusCode(500);
        }

        [HttpPut("{bookId}/return")]
        public ActionResult<RequestBookResult> ReturnBook(int bookId, [FromBody]int returningUserId)
        {
            BookOrder bookOrder = _bookOrderRepository.GetLatestBookOrder(bookId, returningUserId);
            if (bookOrder == null || bookOrder.OrderStatus != OrderStatus.Borrowed)
                return NotFound("Failed to return the book. This user didn't have this book borrowed.");

            ReturnBookResult result = _bookOrderRepository.ReturnBook(bookOrder);
            if (_bookOrderRepository.SaveChanges())
                return Ok(result);

            return StatusCode(500);
        }

        [HttpPut("{bookId}/dispose/{bookOrderId}")]
        public ActionResult<RequestBookResult> DisposeBook(int bookId, int bookOrderId)
        {
            IEnumerable<BookOrder> bookOrders = _bookOrderRepository.GetBookOrders(bookId);

            if(bookOrders.Any(bo => bo.OrderStatus == OrderStatus.Borrowed))
                return NotFound("Failed to dispose a book. Book is already borrowed");


            BookOrder bookOrder = bookOrders.Single(bo => bo.Id == bookOrderId);

            UserLimitCheckResult checkResult = _bookOrderRepository.HasReachedLimitOfBooksBorrowed(bookOrder.UserId);
            if (checkResult.Success == false)
                return NotFound(checkResult.Message);

            DisposeBookResult result = _bookOrderRepository.DisposeBook(bookOrder);
            if (_bookOrderRepository.SaveChanges())
                return Ok(result);

            return StatusCode(500);
        }        
    }

 
}