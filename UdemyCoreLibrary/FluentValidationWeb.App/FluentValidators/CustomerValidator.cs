using FluentValidation;
using FluentValidationWeb.App.Models;
using System;

namespace FluentValidationWeb.App.FluentValidators
{
    public class CustomerValidator:AbstractValidator<Customer>
    {
        public string NotEmptyMessage { get; } = "{PropertyName} alanı boş olamaz";
        public CustomerValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(NotEmptyMessage);
            RuleFor(x => x.EMail).NotEmpty().WithMessage(NotEmptyMessage).EmailAddress().WithMessage("Email alanı doğru formatta değil");
            RuleFor(x => x.Age).NotEmpty().WithMessage(NotEmptyMessage).InclusiveBetween(18, 60).WithMessage("Age aralığı 18 & 60 arası olmalı");
            RuleFor(x => x.BirthDay).NotEmpty().WithMessage(NotEmptyMessage).Must(x=>
            {
                return DateTime.Now.AddYears(-18)>=x;
            }
            ).WithMessage("Yaşınız 18den büyük olmalıdır");

            RuleFor(x => x.Gender).IsInEnum().WithMessage("{PropertyName} alanı yanlış");


            RuleForEach(x=>x.Address).SetValidator(new AddressValidator());
        }
    }
}
