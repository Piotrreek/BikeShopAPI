using System.Data;
using FluentValidation;

namespace BikeShopAPI.Models.Validators
{
    public class BuyNowDtoValidator : AbstractValidator<BuyNowDto>
    {
        public BuyNowDtoValidator()
        {
            RuleFor(p => p.EMail)
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .EmailAddress().WithMessage("Insert correct E-Mail address");
            RuleFor(p => p.City)
                .NotEmpty().WithMessage("{PropertyName} can not be empty");
            RuleFor(p => p.Street)
                .NotEmpty().WithMessage("{PropertyName} can not be empty");
            RuleFor(p => p.HouseNumber)
                .NotEmpty().WithMessage("{PropertyName} can not be empty");
            RuleFor(p => p.PhoneNumber)
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .Must(IsValidNumber).WithMessage("This is not correct number");
        }
        private static bool IsValidNumber(string? number)
        {
            return number != null && number.All(char.IsNumber);
        }
    }
}
