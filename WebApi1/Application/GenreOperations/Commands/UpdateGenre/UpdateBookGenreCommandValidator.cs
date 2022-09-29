using FluentValidation;

namespace WebApi1.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateBookGenreCommandValidator: AbstractValidator<UpdateGenreCommand>
    {
        public UpdateBookGenreCommandValidator()
        {
            RuleFor(command => command.Model.Name).MinimumLength(4).When(x=>x.Model.Name!=string.Empty);
        }
    }
}
