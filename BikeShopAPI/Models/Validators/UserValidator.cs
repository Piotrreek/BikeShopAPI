using BikeShopAPI.Entities;
using FluentValidation;

namespace BikeShopAPI.Models.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.UserName)
                .NotEmpty();
            RuleFor(u => u.Password)
                .NotEmpty();
            RuleFor(u => u.EMailAddress)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
