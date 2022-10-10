using System;
using System.Linq;
using WebApi1.DbOperations;

namespace WebApi1.Application.AuthorOperations.Command.DeleteAuthor
{
    
    public class DeleteAuthorCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int AuthorId { get; set; }

        public DeleteAuthorCommand(BookStoreDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.AuthorId == AuthorId);
            if (author is null)
            {
                throw new InvalidOperationException("Silinecek yazar bulunamadı");
            }
            else
            {
                _dbContext.Authors.Remove(author);
                _dbContext.SaveChanges();
            }
        }
    }
}
