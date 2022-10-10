using AutoMapper;
using WebApi1.BookOperations;
using WebApi1.BookOperations.GetBooks;
using static WebApi1.BookOperations.CreateBookCommand.CreateBookCommand;
using WebApi1.Entities;
using WebApi1.Application.GenreOperations.Queries.GetGenres;
using WebApi1.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi1.Application.AuthorOperations.Queries;
using static WebApi1.Application.AuthorOperations.Command.CreateBookCommand.CreteAuthorCommand;

namespace WebApi1.Common
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailVmModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Book,BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<Author, AuthorViewModel>();
            CreateMap<Author, AuthorDetailViewModel>();
            CreateMap<CreateAuthorModel, Author>();
            
        }
    }
}
