using FluentValidation;

namespace WebApi1.Application.AuthorOperations.Command.DeleteAuthor
{
    public class DeleteAuthorCommandValidator: AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator()
        {
            RuleFor(command=>command.AuthorId).NotEmpty().GreaterThan(0);
        }
    }
}
