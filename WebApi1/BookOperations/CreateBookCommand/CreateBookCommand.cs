﻿using AutoMapper;
using System;
using System.Linq;
using WebApi1.DbOperations;

namespace WebApi1.BookOperations.CreateBookCommand
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateBookCommand(BookStoreDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);

            if (book != null)
            {
                throw new InvalidOperationException("Kitap zaten mevcut");
            }

            book = _mapper.Map<Book>(Model);
            //book = new Book();
            //book.Title = Model.Title;
            //book.PublishDate = Model.Publisdate;
            //book.PageCount = Model.PageCount;
            //book.GenreId = Model.GenreId;


            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();

        }

        public class CreateBookModel
        {
            public string Title { get; set; }

            public int GenreId { get; set; }

            public int PageCount { get; set; }

            public DateTime Publisdate { get; set; }

        }

    }


}
