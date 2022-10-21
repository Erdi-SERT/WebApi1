using System;
using System.Collections.Generic;
using System.Text;
using WebApi1.DbOperations;
using WebApi1.Entities;

namespace WebApi.UnitTests1.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(new Book
            {
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
    }
}
