using BookStoreTask.DTOs;
using FluentValidation;

namespace BookStoreTask.Validators
{
    public class BookCreateValidator : AbstractValidator<BookCreateDto>
    {
        public BookCreateValidator()
        {
            RuleFor(b => b.Title)
                .NotEmpty().WithMessage("Title is required")
                .MinimumLength(3).WithMessage("The Minimum Lenght is 5 Char")
                .MaximumLength(200).WithMessage("The Maximum Lenght is 200 Char");
        }
    }
}