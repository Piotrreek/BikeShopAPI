using BikeShopAPI.Entities;
using FluentValidation;

namespace BikeShopAPI.Models.Validators
{
    public class BikeShopValidator : AbstractValidator<BikeShop>
    {
        public BikeShopValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty()
                .MaximumLength(20);
        }
    }
}
