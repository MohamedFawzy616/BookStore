using BookStoreTask.DTOs;
using FluentValidation;

namespace BookStoreTask.Validators
{
    public class BookUpdateValidator : AbstractValidator<BookUpdateDto>
    {
        public BookUpdateValidator()
        {
            RuleFor(b => b.Title)
                .NotEmpty().WithMessage("Title is required")
                .MinimumLength(3).WithMessage("The Minimum Lenght is 3 Char")
                .MaximumLength(200).WithMessage("The Maximum Lenght is 200 Char");
        }
    }
}