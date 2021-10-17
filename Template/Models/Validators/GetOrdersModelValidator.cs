using FluentValidation;
using Template.Models.RequestModels;

namespace Template.Models.Validators
{
    public class GetOrdersModelValidator : AbstractValidator<GetOrdersModel>
    {
        public GetOrdersModelValidator()
        {
            RuleFor(x => x.Skip).GreaterThan(-1);
            RuleFor(x => x.Take).GreaterThan(0);
        }
    }
}
