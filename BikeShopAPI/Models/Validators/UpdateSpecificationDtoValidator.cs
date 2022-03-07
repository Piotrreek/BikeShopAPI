using FluentValidation;

namespace BikeShopAPI.Models.Validators
{
    public class UpdateSpecificationDtoValidator : AbstractValidator<UpdateSpecificationDto>
    {
        public UpdateSpecificationDtoValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("{PropertyName} can't be empty");
            RuleFor(s => s.Brand)
                .NotEmpty().WithMessage("{PropertyName} can't be empty");
        }
    }
}
