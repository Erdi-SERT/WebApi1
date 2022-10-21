using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi1.DbOperations;
using WebApi1.Entities;

namespace WebApi1.Application.AuthorOperations.Queries
{
    public class GetAuthorsQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetAuthorsQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<AuthorViewModel> Handle()
        {
            var authorList = _dbContext.Authors.OrderBy(x=>x.AuthorId).ToList<Author>();
            List<AuthorViewModel> result=_mapper.Map<List<AuthorViewModel>>(authorList);
            return result;
        }
    }

    public class AuthorViewModel
    {
        public string Name { get; set; }
        public string SurName { get; set; }

        public DateTime Birthdate { get; set; }
    }
}
