using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi1.Application.GenreOperations.Commands;
using WebApi1.Application.GenreOperations.Commands.DeleteGenre;
using WebApi1.Application.GenreOperations.Commands.UpdateGenre;
using WebApi1.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi1.Application.GenreOperations.Queries.GetGenres;
using WebApi1.DbOperations;
using static WebApi1.BookOperations.CreateBookCommand.CreateBookCommand;
using static WebApi1.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi1.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreController(IBookStoreDbContext context, IMapper mapper)
        {
            _context= context;
            _mapper= mapper;    
        }


        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_context,_mapper);
            var obj = query.Handle();
            return Ok(obj);
        }
        [HttpGet("id")]
        public IActionResult GetGenreDetail(int id)
        {
            GetGenreDetailQuery query=new GetGenreDetailQuery(_context,_mapper);
            query.GenreId=id;
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            validator.ValidateAndThrow(query);

            var obj=query.Handle();
            return Ok(obj);
        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context,_mapper);
            command.Model= newGenre;
            CreateGenreCommandValidator validations=new CreateGenreCommandValidator();
            validations.ValidateAndThrow(command);
            command.Handle(newGenre);
            return Ok();
        }


        [HttpPut("id")]
        public IActionResult UpdateGenre(int id,[FromBody] UpdateGenreModel model)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.Model= model;
            command.GenreId = id;
            UpdateBookGenreCommandValidator validator = new UpdateBookGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand command=new DeleteGenreCommand(_context);
            command.GenreId = id;
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }



    }
}
