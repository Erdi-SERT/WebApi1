using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi1.BookOperations;
using WebApi1.BookOperations.CreateBookCommand;
using WebApi1.BookOperations.DeleteBooks;
using WebApi1.BookOperations.GetBookDetail;
using WebApi1.BookOperations.GetBooks;
using WebApi1.BookOperations.UpdateBook;
using WebApi1.DbOperations;
using static WebApi1.BookOperations.CreateBookCommand.CreateBookCommand;
using static WebApi1.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi1.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        //Nuget ten EntityFrameworkCore ve  EntityFrameworkCore.InMemory nuget ten ekledik 
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            BookDetailVmModel result = new BookDetailVmModel();
            //try
            //{
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);

            query.BookId = id;
            GetBookCommandValidator validator = new GetBookCommandValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();
            return Ok(result);


        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = id;
            command.Model = updatedBook;
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.handle();           
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteBook(int id)
        {            
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;
            DeleteBookCommandValidator validationRules = new DeleteBookCommandValidator();
            validationRules.ValidateAndThrow(command);
            command.Handle();          
            return Ok();
        }
    }
}

