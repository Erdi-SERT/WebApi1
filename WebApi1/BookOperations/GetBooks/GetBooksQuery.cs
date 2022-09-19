using System;
using System.Collections.Generic;
using System.Linq;
using WebApi1.Common;
using WebApi1.DbOperations;

namespace WebApi1.BookOperations
{
    public class GetBooksQuery
    {
        
        private readonly BookStoreDbContext _dbContext;
        public GetBooksQuery(BookStoreDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public List<BooksViewModel> Handle()
        {
            var booklist = _dbContext.Books.OrderBy(x => x.Id).ToList<Book>();
            List<BooksViewModel> vm = new List<BooksViewModel>();
            foreach (var item in booklist)
            {
                vm.Add(new BooksViewModel
                {
                    Title = item.Title,
                    Genre=((GenreEnum)item.GenreId).ToString(),

                    PageCount=item.PageCount,
                    PublishDate=item.PublishDate.Date.ToString("dd/MM/yyyy"),
                });
            }
            return vm;
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
