using FluentValidation;

namespace WebApi1.Application.AuthorOperations.Queries.GetAuthorDetails
{
    public class GetAuthorDetailQueryValidator: AbstractValidator<GetAuthorDetailQuery>
    {
        public GetAuthorDetailQueryValidator()
        {
            RuleFor(x => x.AuthorId).NotEmpty();
            RuleFor(x => x.AuthorId).GreaterThan(0);
        }
    }
}
