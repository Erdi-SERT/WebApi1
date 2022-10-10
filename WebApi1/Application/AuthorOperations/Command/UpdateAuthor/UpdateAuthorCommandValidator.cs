using FluentValidation;

namespace WebApi1.Application.AuthorOperations.Command.UpdateAuthor
{
    public class UpdateAuthorCommandValidator: AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.AuthorId).GreaterThan(0);
            RuleFor(command=>command.Model.Name).NotEmpty();
            RuleFor(command=>command.Model.Name).MinimumLength(4);
            RuleFor(command=>command.Model.Surname).MinimumLength(4);
            RuleFor(command=>command.Model.Surname).NotEmpty();
            RuleFor(command=>command.Model.Birthdate).NotEmpty();
            RuleFor(command=>command.Model.Birthdate).GreaterThan(new System.DateTime(1500,01,01));
            
            
        }
    }
}
