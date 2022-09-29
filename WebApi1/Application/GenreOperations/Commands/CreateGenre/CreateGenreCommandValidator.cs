using FluentValidation;

namespace WebApi1.Application.GenreOperations.Commands
{
    public class CreateGenreCommandValidator: AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(command=>command.Model.Name).NotEmpty();
            RuleFor(command=>command.Model.Name).MinimumLength(4);
            
        }
    }
}
