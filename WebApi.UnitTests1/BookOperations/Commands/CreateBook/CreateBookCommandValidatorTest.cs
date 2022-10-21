using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.UnitTests1.TestSetup;
using WebApi1.BookOperations.CreateBookCommand;
using Xunit;
using static WebApi1.BookOperations.CreateBookCommand.CreateBookCommand;

namespace WebApi.UnitTests1.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTest: IClassFixture<CommonTestFixture>
    {
        //Bİrden fazla test çalıştırmak için kullanılır
        [Theory]
        [InlineData("Lord Of The",0,0)]
        [InlineData("Lord Of The",0,1)]
        [InlineData("",0,0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnsErrors(string title,int pageCount,int genreID)
        {
            //arrenge
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model = new CreateBookModel
            {
                Title = "",
                PageCount = 0,
                Publisdate = DateTime.Now,
                GenreId = 0
            };

            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result=validator.Validate(command);
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        //Bir test içinde yalnızca 1 durum test edilmeli
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel
            {
                Title = "Lord Of The Rings",
                PageCount = 100,
                Publisdate = DateTime.Now.Date,
                GenreId = 1
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result=validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel
            {
                Title = "Lord Of The Rings",
                PageCount = 100,
                Publisdate = DateTime.Now.Date.AddYears(-2),
                GenreId = 1
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Equals(0);
        }



    }
}
