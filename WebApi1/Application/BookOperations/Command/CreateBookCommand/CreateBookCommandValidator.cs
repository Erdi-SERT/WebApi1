using FluentValidation;
using System;

namespace WebApi1.BookOperations.CreateBookCommand
{
    public class CreateBookCommandValidator:AbstractValidator<CreateBookCommand>
    {
        
        public CreateBookCommandValidator()
        {
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.PageCount).GreaterThan(0);
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.Publisdate.Date).NotEmpty().LessThan(DateTime.Now.Date);

        }
    }
}
