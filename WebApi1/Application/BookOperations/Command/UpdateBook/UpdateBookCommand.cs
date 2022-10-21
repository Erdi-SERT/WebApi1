using System;
using System.Collections.Generic;
using System.Linq;
using WebApi1.Common;
using WebApi1.DbOperations;
namespace WebApi1.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly IBookStoreDbContext _dbContext;

        public UpdateBookModel Model { get; set; }
        public UpdateBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int BookId { get; set; }
        public void handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);

            if (book is null)
            {
                throw new InvalidOperationException("Güncellenecek kitap bulunamadı");
            }
            
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;           

            book.Title = Model.Title != default ? Model.Title : book.Title;
            _dbContext.SaveChanges();
        }

        public class UpdateBookModel
        {
            public string Title { get; set; }

            public int GenreId { get; set; }
        }

    }
}
