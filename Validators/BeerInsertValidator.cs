using FluentValidation;

namespace MyApp.Namespace
{
    public class BeerInsertValidator : AbstractValidator<BeerInsertDto>
    {
        public BeerInsertValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("The name is required");
            RuleFor(x => x.Name).Length(3, 20).WithMessage("The length of the name must be between 3 and 20 characters");
            RuleFor(x => x.BrandID).NotNull().WithMessage("The brand is mandatory");
            RuleFor(x => x.BrandID).GreaterThan(0).WithMessage("Id must be greater than zero");
            RuleFor(x => x.Alcohol).GreaterThan(0).WithMessage("{PropertyName} must be greater than zero");
            
        }
    }
}