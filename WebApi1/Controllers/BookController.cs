using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi1.DbOperations;

namespace WebApi1.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        //Nuget ten EntityFrameworkCore ve  EntityFrameworkCore.InMemory nuget ten ekledik 
        private readonly BookStoreDbContext _context;


        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //public List<Book> GetBooks()
        //{

        //    var booklist = _context.Books.OrderBy(x => x.Id).ToList<Book>();
        //    return booklist;
        //}


        [HttpGet("{id}")]
        public Book GetByID(int id)
        {

            var book = _context.Books.Where(x => x.Id == id).SingleOrDefault();
            return book;
        }
        //[HttpGet]
        //public Book Get([FromQuery]string id)
        //{
        //    var book= BookList.Where(x=>x.Id==Convert.ToInt32(id)).SingleOrDefault();
        //    return book;
        //}
        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = _context.Books.SingleOrDefault(x => x.Title == newBook.Title);

            if (book.Id>0 )
            {
                return BadRequest();
            }
            else
            {
                _context.Books.Add(newBook);
                _context.SaveChanges();
            }
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);

            if (book is null)
            {
                return BadRequest();
            }
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;

            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;

            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;

            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
            {
                return BadRequest();
            }
            else
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
                return Ok();
            }

        }
    }
}

