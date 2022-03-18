using BikeShopAPI.Entities;
using FluentValidation;

namespace BikeShopAPI.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(BikeShopDbContext dbContext)
        {
            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("{PropertyName} can not be empty!")
                .MinimumLength(10).WithMessage("Minimum length of {PropertyName} is 10!");
            RuleFor(u => u.EMailAddress)
                .NotEmpty()
                .EmailAddress();
            RuleFor(u => u.ConfirmPassword)
                .Equal(e => e.Password)
                .WithMessage("Password and ConfirmPassword must be the same!");
            RuleFor(u => u.UserName)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var userNameInUse = dbContext.Users.Any(u => u.UserName == value);
                    if (userNameInUse)
                    {
                        context.AddFailure("UserName", "That UserName is taken");
                    }
                });
            RuleFor(u => u.EMailAddress)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var EmailInUse = dbContext.Users.Any(u => u.EMailAddress == value);
                    if (EmailInUse)
                    {
                        context.AddFailure("EMailAddress", "That E-mail is taken");
                    }

                });
            RuleFor(u => u.RoleId)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    if (value == 3)
                    {
                        context.AddFailure("RoleId", "U can not assign yourself this role!");
                    }
                });
        }
    }
}
