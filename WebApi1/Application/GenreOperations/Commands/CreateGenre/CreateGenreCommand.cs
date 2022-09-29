using AutoMapper;
using System.Linq;
using WebApi1.DbOperations;
using System;


namespace WebApi1.Application.GenreOperations.Commands
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model { get; set; }

        private readonly BookStoreDbContext _context;

        private readonly IMapper _mapper;


        public CreateGenreCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle(CreateGenreModel model)
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Name == Model.Name);
            if (genre != null)
            {
                throw new InvalidOperationException(" Eklemek istediğiniz kitap türü zaten mevcut");
            }
            genre = new Entities.Genre();
            genre.Name=model.Name;

            _context.Genres.Add(genre);
            _context.SaveChanges();
        }
    }

    public class CreateGenreModel
    {
        public string Name { get; set; }
    }
}
