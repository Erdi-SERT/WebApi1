using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Linq;
using WebApi1.DbOperations;
using System;
using WebApi1.TokenOperations;
using WebApi1.TokenOperations.Models;

namespace WebApi1.Application.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {
        public CreateTokenModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public CreateTokenCommand(IBookStoreDbContext dbContext, IMapper mapper, IConfiguration config)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _config = config;
        }

        public Token Handle()
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);
            if (user != null)
            {
                //Token yarat
                TokenHandler tokenHandler = new TokenHandler(_config);
                Token token = tokenHandler.CreateAccessToken(user);
                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _dbContext.SaveChanges();
                return token;



            }
            else
            {
                throw new InvalidOperationException("Kullanıcı adı veya  şifre hatalı");
            }
        }

        public class CreateTokenModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
