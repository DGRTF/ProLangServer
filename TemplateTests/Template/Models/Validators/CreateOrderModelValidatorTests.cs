using FluentValidation.TestHelper;
using Template.Models.RequestModels;
using Template.Models.Validators;
using Xunit;

namespace TemplateTests.Template.Models.Validators
{
    public class CreateOrderModelValidatorTests
    {
        private CreateOrderModelValidator _validator;

        public CreateOrderModelValidatorTests()
        {
            _validator = new CreateOrderModelValidator();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("              ")]
        public void ShouldHaveValidationErrorForCustomer(string customer)
        {
            var model = ValidCreateOrderModel();
            model.Customer = customer;

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(nameof(model.Customer));
        }

        [Fact]
        public void ShouldNotHaveValidationErrorForCustomer()
        {
            var model = ValidCreateOrderModel();

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(nameof(model.Customer));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-0.345)]
        public void ShouldHaveValidationErrorForCosts(double costs)
        {
            var model = ValidCreateOrderModel();
            model.Costs = costs;

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(nameof(model.Costs));
        }

        [Fact]
        public void ShouldNotHaveValidationErrorForCosts()
        {
            var model = ValidCreateOrderModel();

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(nameof(model.Costs));
        }

        private CreateOrderModel ValidCreateOrderModel() =>
            new CreateOrderModel
            {
                Customer = "Customer",
                Ddescription = "Ddescription",
                Costs = 2,
            };
    }
}
