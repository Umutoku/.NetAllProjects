using FluentValidation;

namespace Library.Models.FluentValidators
{
    public class AuthorValidator:AbstractValidator<Author>
    {
        public AuthorValidator()
        {
            When(x => x.FirstName == "Deneme", () =>
            {
                RuleFor(x => x.LastName).Must(y => y == "Test").WithMessage("İsim deneme ise soyadı test olmalı");
            }
            );
        }
    }
}
