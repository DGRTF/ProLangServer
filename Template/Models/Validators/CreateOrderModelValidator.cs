using FluentValidation;
using Template.Models.RequestModels;

namespace Template.Models.Validators
{
    public class CreateOrderModelValidator : AbstractValidator<CreateOrderModel>
    {
        public CreateOrderModelValidator()
        {
            RuleFor(x => x.Costs).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Customer).NotEmpty();
        }
    }
}
