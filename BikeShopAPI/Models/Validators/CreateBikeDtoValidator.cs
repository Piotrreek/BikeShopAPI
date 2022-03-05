using FluentValidation;

namespace BikeShopAPI.Models.Validators
{
    public class CreateBikeDtoValidator : AbstractValidator<CreateBikeDto>
    {
        public CreateBikeDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .MaximumLength(15);
            RuleFor(b => b.Brand)
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .MaximumLength(15);
            RuleFor(b => b.Price)
                .NotEmpty().WithMessage("{PropertyName} can not be empty");
            RuleFor(b => b.Count)
                .NotEmpty().WithMessage("{PropertyName} can not be empty");
        }
    }
}
