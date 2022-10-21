using System;
using System.Collections.Generic;
using System.Text;
using WebApi1.DbOperations;
using WebApi1.Entities;

namespace WebApi.UnitTests1.TestSetup
{
    public static class Genres
    {
        public static void AddGenres(this BookStoreDbContext context)
        {
            context.AddRange(new Genre
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
        }
    }
}
