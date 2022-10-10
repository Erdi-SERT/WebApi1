using AutoMapper;
using FluentValidation;
using System;
using WebApi1.Application.AuthorOperations.Command.CreateBookCommand;

namespace WebApi1.Application.AuthorOperations.Command.CreateBook
{
    public class CreateAuthorValidator : AbstractValidator<CreteAuthorCommand>
    {
        public CreateAuthorValidator()
        {
            RuleFor(command=>command.Model.Name).NotEmpty();
            RuleFor(command=>command.Model.Name).MinimumLength(4);
            RuleFor(command=>command.Model.Surname).MinimumLength(4);
            RuleFor(command=>command.Model.Surname).MinimumLength(4);
            RuleFor(command => command.Model.BirthDate).GreaterThan(new DateTime(1990, 01, 01));

        }

    }
}

    
