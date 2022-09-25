using FluentValidation;
using WebApi1.BookOperations.GetBooks;

namespace WebApi1.BookOperations.GetBookDetail
{
    public class GetBookCommandValidator: AbstractValidator<GetBookDetailQuery>
    {
        
        public GetBookCommandValidator()
        {
            RuleFor(command=>command.BookId).NotEmpty();
            RuleFor(command=>command.BookId).GreaterThan(0);
            
        }
    }
}
