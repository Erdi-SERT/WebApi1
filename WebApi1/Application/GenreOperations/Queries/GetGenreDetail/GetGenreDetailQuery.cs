using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi1.DbOperations;

namespace WebApi1.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {

        public int GenreId { get; set; }
        private readonly IBookStoreDbContext _context;

        private readonly IMapper _mapper;
        public GetGenreDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            var genres = _context.Genres.SingleOrDefault(x => x.IsActive == true && x.Id==GenreId);
            GenreDetailViewModel returnObj = _mapper.Map<GenreDetailViewModel>(genres);
            if (returnObj is null)
            {
                throw new InvalidOperationException("Kitap türü bulunamadı");
            }
            return returnObj;
        }
    }

    public class GenreDetailViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
