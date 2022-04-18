using BikeShopAPI.Entities;
using FluentValidation;

namespace BikeShopAPI.Models.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty();
            RuleFor(p => p.Price)
                .NotEmpty();
            RuleFor(p => p.Count)
                .NotEmpty();
            RuleFor(p => p.Brand)
                .NotEmpty();
        }
    }
}
