using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi1.Application.UserOperations.Commands.CreateToken;
using WebApi1.Application.UserOperations.Commands.CreateUser;
using WebApi1.DbOperations;
using WebApi1.TokenOperations.Models;
using static WebApi1.Application.UserOperations.Commands.CreateToken.CreateTokenCommand;
using static WebApi1.Application.UserOperations.Commands.CreateUser.CreateUserCommand;

namespace WebApi1.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class UserController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        readonly IConfiguration _config;
        public UserController(IBookStoreDbContext context, IConfiguration config, IMapper mapper)
        {
            _context = context;
            _config = config;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserModel newUser) 
        {
            CreateUserCommand command =new CreateUserCommand(_context, _mapper);
            command.Model= newUser;
            command.Handle();
            return Ok();
        }

        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command = new CreateTokenCommand(_context,_mapper,_config);
            command.Model= login;
            var token = command.Handle();
            return token;
        }
    }
}
