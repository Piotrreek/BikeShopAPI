using System.Data;
using BikeShopAPI.Entities;
using FluentValidation;

namespace BikeShopAPI.Models.Validators
{
    public class BagValidator : AbstractValidator<Bag>
    {
        public BagValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty();
            RuleFor(b => b.Price)
                .NotEmpty();
            RuleFor(b => b.Count)
                .NotEmpty();
        }
    }
}
