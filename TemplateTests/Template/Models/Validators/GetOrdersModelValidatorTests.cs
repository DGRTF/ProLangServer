using FluentValidation.TestHelper;
using Template.Models.RequestModels;
using Template.Models.Validators;
using Xunit;

namespace TemplateTests.Template.Models.Validators
{
    public class GetOrdersModelValidatorTests
    {
        private GetOrdersModelValidator _validator;

        public GetOrdersModelValidatorTests()
        {
            _validator = new GetOrdersModelValidator();
        }

        [Fact]
        public void ShouldHaveValidationErrorForSkip()
        {
            var model = ValidGetOrdersModel();
            model.Skip = -1;

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(nameof(model.Skip));
        }

        [Fact]
        public void ShouldNotHaveValidationErrorForSkip()
        {
            var model = ValidGetOrdersModel();

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(nameof(model.Skip));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void ShouldHaveValidationErrorForTake(int take)
        {
            var model = ValidGetOrdersModel();
            model.Take = take;

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(nameof(model.Take));
        }

        [Fact]
        public void ShouldNotHaveValidationErrorForTake()
        {
            var model = ValidGetOrdersModel();

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(nameof(model.Take));
        }

        private GetOrdersModel ValidGetOrdersModel() =>
            new GetOrdersModel
            {
                Skip = 0,
                Take = 1,
            };
    }
}
