using System.Linq;
using System;
using WebApi1.DbOperations;

namespace WebApi1.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreModel Model { get; set; }
        private readonly BookStoreDbContext _context;
        public UpdateGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
            if (genre == null)
            {
                throw new InvalidOperationException("Update edilecek genre bulunamadı");
            }
            if (_context.Genres.Any(x=>x.Name.ToLower()==Model.Name.ToLower()&&x.Id!=GenreId))
            {
                throw new InvalidOperationException("Aynı isim ile bir kitap türü mevcut");
            }
            genre.Name = Model.Name.Trim() == default ? Model.Name : genre.Name;
            genre.IsActive=Model.IsActive;
            _context.SaveChanges();
        }


    }



    public class UpdateGenreModel
    {
        public string Name { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
