using BikeShopAPI.Entities;
using FluentValidation;

namespace BikeShopAPI.Models.Validators
{
    public class BikeValidator : AbstractValidator<Bike>
    {
        public BikeValidator()
        {
            RuleFor(b => b.Brand)
                .NotEmpty()
                .MaximumLength(15);
            RuleFor(b => b.Name)
                .NotEmpty()
                .MaximumLength(15);
            RuleFor(b => b.Price)
                .NotEmpty();
            RuleFor(b => b.Count)
                .NotEmpty();
        }
    }
}
