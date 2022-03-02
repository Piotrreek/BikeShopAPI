using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace BikeShopAPI.Models.Validators
{
    public class CreateBikeShopDtoValidator : AbstractValidator<CreateBikeShopDto>
    {
        public CreateBikeShopDtoValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("{PropertyName} should be not empty.")
                .MaximumLength(20).WithMessage("Maximum length of {PropertyName} is 20!");
            RuleFor(s => s.City)
                .NotEmpty().WithMessage("{PropertyName} should be not empty.")
                .MaximumLength(15).WithMessage("Maximum length of {PropertyName} is 15!"); ;
            RuleFor(s => s.Street)
                .NotEmpty().WithMessage("{PropertyName} should be not empty.")
                .MaximumLength(20).WithMessage("Maximum length of {PropertyName} is 20!"); ;
            RuleFor(s => s.PostalCode)
                .NotEmpty().WithMessage("{PropertyName} should be not empty.");
            RuleFor(s => s.ContactEmail)
                .NotEmpty().WithMessage("{PropertyName} should be not empty.")
                .EmailAddress().WithMessage("This is not correct e-mail address");
            RuleFor(s => s.ContactNumber)
                .NotEmpty().WithMessage("{PropertyName} should be not empty.")
                .Must(isValidNumber).WithMessage("This is not correct number");
        }

        private bool isValidNumber(string number)
        {
            return number.All(char.IsNumber);
        }
    }
}
