using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi1.Common;
using WebApi1.DbOperations;
using WebApi1.Entities;

namespace WebApi1.BookOperations
{
    public class GetBooksQuery
    {
           
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBooksQuery(BookStoreDbContext dbContext,IMapper mapper )
        {
            _dbContext = dbContext;
            _mapper = mapper;   
        }

        public List<BooksViewModel> Handle()
        {
            var booklist = _dbContext.Books.Include(x=>x.Genre).OrderBy(x => x.Id).ToList<Book>();

            List<BooksViewModel> result = _mapper.Map<List<BooksViewModel>>(booklist);
            //List<BooksViewModel> vm = new List<BooksViewModel>();
            //foreach (var item in booklist)
            //{
            //    vm.Add(new BooksViewModel
            //    {
            //        Title = item.Title,
            //        Genre=((GenreEnum)item.GenreId).ToString(),

            //        PageCount=item.PageCount,
            //        PublishDate=item.PublishDate.Date.ToString("dd/MM/yyyy"),
            //    });
            //}
            return result;
        }
    }

    public class BooksViewModel
    {
        public string  Title { get; set; }

        public int PageCount{ get; set; }

        public string  PublishDate{ get; set; }

        public string Genre { get; set; }

    }
}
