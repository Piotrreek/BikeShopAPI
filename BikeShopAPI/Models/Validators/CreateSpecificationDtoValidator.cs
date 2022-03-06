using FluentValidation;

namespace BikeShopAPI.Models.Validators
{
    public class CreateSpecificationDtoValidator : AbstractValidator<CreateSpecificationDto>
    {
        public CreateSpecificationDtoValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("{PropertyName} can't be empty");
            RuleFor(s => s.Brand)
                .NotEmpty().WithMessage("{PropertyName} can't be empty");
        }
    }
}
