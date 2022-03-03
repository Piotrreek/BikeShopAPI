using FluentValidation;

namespace BikeShopAPI.Models.Validators
{
    public class UpdateBikeShopDtoValidator : AbstractValidator<UpdateBikeShopDto>
    {
        public UpdateBikeShopDtoValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("{PropertyName} should be not empty.")
                .MaximumLength(20).WithMessage("Maximum length of {PropertyName} is 20!");
            RuleFor(s => s.ContactEmail)
                .NotEmpty().WithMessage("{PropertyName} should be not empty.")
                .EmailAddress().WithMessage("This is not correct e-mail address");
            RuleFor(s => s.ContactNumber)
                .NotEmpty().WithMessage("{PropertyName} should be not empty.")
                .Must(IsValidNumber).WithMessage("This is not correct number");

        }
        private static bool IsValidNumber(string? number)
        {
            return number != null && number.All(char.IsNumber);
        }
    }
}
