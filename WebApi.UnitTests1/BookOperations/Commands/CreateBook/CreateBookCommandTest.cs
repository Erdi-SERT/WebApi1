using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.UnitTests1.TestSetup;
using WebApi1.DbOperations;
using WebApi1.Entities;
using Xunit;
using WebApi1.BookOperations.CreateBookCommand;
using static WebApi1.BookOperations.CreateBookCommand.CreateBookCommand;
using FluentAssertions;
using System.Linq;

namespace WebApi.UnitTests1.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        private readonly IMapper _mapper;

        public CreateBookCommandTest(CommonTestFixture   testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;   
        }

        //Aslında bir test olduğu belli eder 
        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            var book = new Book()
            {
                Title = "WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",
                PageCount = 100,
                PublishDate = new System.DateTime(1990, 01, 10),
                GenreId = 1
            };

            _context.Books.Add(book);
            _context.SaveChanges();
            WebApi1.BookOperations.CreateBookCommand.CreateBookCommand command = new WebApi1.BookOperations.CreateBookCommand.CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel() { Title = "WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn" };

            //act && assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut");                

        }


        [Fact]
        public void WhenValidInputAreGiven_Book_ShouldBeCreated()
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            CreateBookModel model=new CreateBookModel()
            {
                Title="The Lord Of The Rings",
                PageCount=100,
                GenreId=1,
                Publisdate=DateTime.Now.AddYears(-2)
            };
            
            command.Model = model;
            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var book = _context.Books.SingleOrDefault(x => x.Title == model.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.GenreId.Should().Be(model.GenreId);
            book.PublishDate.Should().Be(model.Publisdate);
            book.Title.Should().Be(model.Title);



        }
    }
}
