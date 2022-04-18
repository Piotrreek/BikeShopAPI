using BikeShopAPI.Entities;
using FluentValidation;

namespace BikeShopAPI.Models.Validators
{
    public class SpecificationValidator : AbstractValidator<Specification>
    {
        public SpecificationValidator()
        {
            RuleFor(s => s.Brand)
                .NotEmpty();
            RuleFor(s => s.Name)
                .NotEmpty();
        }
    }
}
