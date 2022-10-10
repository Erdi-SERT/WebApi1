using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WebApi1.Entities;

namespace WebApi1.DbOperations
{
    public class DataGenarator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }
                else
                {
                    context.Genres.AddRange(new Genre
                    {
                        Name = "Personal Growth",

                    }, new Genre
                    {
                        Name = "Science Fiction",

                    },
                    new Genre
                    {
                        Name = "Romance",

                    });

                    context.Authors.AddRange(new Author
                    {
                        Name = "Halil",
                        Surname = "İnancık",
                        BirthDate = new DateTime(1916, 09, 06)
                    },
                    new Author
                    {
                        Name = "İlber",
                        Surname = "Ortaylı",
                        BirthDate = new DateTime(1947, 05, 21)
                    },
                    new Author 
                    {
                        Name="Fuat",
                        Surname="Köprülü",
                        BirthDate=new DateTime(1890,12,4)
                    }
                    );



                    context.Books.AddRange(new Book
                    {
                        //Id = 1,
                        Title = "Lean Startup",
                        GenreId = 1,  //Personal Growth
                        PageCount = 200,
                        PublishDate = new DateTime(2001, 06, 12)
                    },
                    new Book
                    {
                        //Id = 2,
                        Title = "Herland",
                        GenreId = 2,  //Science Fiction
                        PageCount = 250,
                        PublishDate = new DateTime(2010, 05, 23)
                    },
                    new Book
                    {
                        //Id = 3,
                        Title = "Dune",
                        GenreId = 2,  //Science Fiction
                        PageCount = 540,
                        PublishDate = new DateTime(2002, 12, 21)
                    });
                }
                context.SaveChanges();
            }
        }
    }
}
