﻿using FluentValidation;

namespace WebApi1.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidator: AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreCommandValidator()
        {
            RuleFor(command=>command.GenreId).GreaterThan(0);
            RuleFor(command=>command.GenreId).NotEmpty();
        }
    }
}
