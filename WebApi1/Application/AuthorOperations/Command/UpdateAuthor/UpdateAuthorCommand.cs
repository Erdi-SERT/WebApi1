using System;
using System.Linq;
using WebApi1.DbOperations;

namespace WebApi1.Application.AuthorOperations.Command.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public int AuthorId { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public UpdateAuthorModel Model { get; set; }

        public UpdateAuthorCommand(BookStoreDbContext dbContext)
        {
            _dbContext= dbContext;          
        }


        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.AuthorId == AuthorId);
            if (author is null)
            {
                throw new InvalidOperationException("Güncellenecek yazar bulunamadı");
            }

            author.Name = Model.Name = default ? Model.Name : author.Name;

            author.Surname = Model.Surname = default ? Model.Surname : author.Surname;

            author.BirthDate = Model.Birthdate = default ? Model.Birthdate : author.BirthDate;
            _dbContext.SaveChanges();
        }
    }

    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
