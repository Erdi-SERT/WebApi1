using AutoMapper;
using System.Linq;
using WebApi1.DbOperations;
using WebApi1.Entities;
using System;

namespace WebApi1.Application.AuthorOperations.Queries
{
    public class GetAuthorDetailQuery
    {

        public int AuthorId { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetAuthorDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public AuthorDetailViewModel Handle(int id)
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.AuthorId == AuthorId);

            if (author is null)
            {
                throw new InvalidOperationException("İlgili yazar bulunamadı");
            }


            AuthorDetailViewModel result = _mapper.Map<AuthorDetailViewModel>(author);
            return result;
        }
    }

    public class AuthorDetailViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate{ get; set; }
    }
}
