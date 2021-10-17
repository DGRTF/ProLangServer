using FluentValidation;
using Template.Models.RequestModels;

namespace Template.Models.Validators
{
    public class FindOrdersModelValidator : AbstractValidator<FindOrdersModel>
    {
        public FindOrdersModelValidator()
        {
            Include(new GetOrdersModelValidator());
            RuleFor(x => x.Customer).NotEmpty();
        }
    }
}
