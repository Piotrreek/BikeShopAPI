using BikeShopAPI.Entities;
using FluentValidation;

namespace BikeShopAPI.Models.Validators
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(a => a.City)
                .NotEmpty()
                .MaximumLength(15);
            RuleFor(a => a.Street)
                .NotEmpty()
                .MaximumLength(15);
        }
    }
}
