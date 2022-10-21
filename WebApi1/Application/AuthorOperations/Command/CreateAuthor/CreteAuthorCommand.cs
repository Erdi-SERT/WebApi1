using AutoMapper;
using System;
using System.Linq;
using WebApi1.DbOperations;
using WebApi1.Entities;

namespace WebApi1.Application.AuthorOperations.Command.CreateBookCommand
{
    public class CreteAuthorCommand
    {
        public CreateAuthorModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreteAuthorCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Name== Model.Name && x.Surname==Model.Surname && x.BirthDate==Model.BirthDate);

            if (author != null)
            {
                throw new InvalidOperationException("Yazar zaten mevcut");
            }

            author = _mapper.Map<Author>(Model);
            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();
        }
        public class CreateAuthorModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime BirthDate { get; set; }
        }
    }
}

