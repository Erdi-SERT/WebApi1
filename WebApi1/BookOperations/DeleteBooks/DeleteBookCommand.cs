using System;
using System.Linq;
using WebApi1.DbOperations;

namespace WebApi1.BookOperations.DeleteBooks
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext _dbContext;

        public int BookId { get; set; }

        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
            {
                throw new InvalidOperationException("Sİlinecke kitap bulunamadı");
            }
            else
            {
                _dbContext.Books.Remove(book);
                _dbContext.SaveChanges();
                
            }
        }

        
    }
}
