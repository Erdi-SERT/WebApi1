using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi1.Application.AuthorOperations.Command.CreateBook;
using WebApi1.Application.AuthorOperations.Command.CreateBookCommand;
using WebApi1.Application.AuthorOperations.Command.DeleteAuthor;
using WebApi1.Application.AuthorOperations.Command.UpdateAuthor;
using WebApi1.Application.AuthorOperations.Queries;
using WebApi1.Application.AuthorOperations.Queries.GetAuthorDetails;
using WebApi1.DbOperations;
using static WebApi1.Application.AuthorOperations.Command.CreateBookCommand.CreteAuthorCommand;

namespace WebApi1.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class AuthorController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public AuthorController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //Bütün yazarları getirir
        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        } 

        //Belirli Yazarı Getir
        [HttpGet("{id}")]
        public IActionResult GetAuthor(int id)
        {
            AuthorDetailViewModel result=new AuthorDetailViewModel();
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = id;
            GetAuthorDetailQueryValidator validations = new GetAuthorDetailQueryValidator();
            validations.ValidateAndThrow(query);
            result = query.Handle(id);
            return Ok(result);

        }

        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor)
        {
            CreteAuthorCommand command = new CreteAuthorCommand(_context, _mapper);
            command.Model = newAuthor;
            CreateAuthorValidator validator = new CreateAuthorValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteAuthor(int id)
        {
            
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = id;
            DeleteAuthorCommandValidator validationRules = new DeleteAuthorCommandValidator();
            validationRules.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateBook(int id, [FromBody] UpdateAuthorModel updatedBook)
        {
            UpdateAuthorCommand  command = new UpdateAuthorCommand(_context);
            command.AuthorId = id;
            command.Model = updatedBook;
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }


    }
}
