using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi1.Common;
using WebApi1.DbOperations;

namespace WebApi1.BookOperations.GetBooks
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId { get; set; }
        public GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }

        public BookDetailVmModel Handle()
        {
            var book = _dbContext.Books.Include(x=>x.Genre).Where(x => x.Id == BookId).SingleOrDefault();
            if (book == null)
            {
                throw new InvalidOperationException("Kitap bulunamadı");
            }
            BookDetailVmModel vm=_mapper.Map<BookDetailVmModel>(book);


            //BookDetailVmModel vm=new BookDetailVmModel();
            //{
            //    vm.Title = book.Title;
            //    vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
            //    vm.Genre = ((GenreEnum)book.GenreId).ToString();
            //    vm.PageCount=book.PageCount;

            //}
            return vm;
        }
    }

    public class BookDetailVmModel
    {
        public string Title { get; set; }

        public string Genre { get; set; }

        public int PageCount { get; set; }

        public string PublishDate { get; set; }
    }
}
