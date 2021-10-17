using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Models.RequestModels;
using Template.Models.Validators;
using Xunit;

namespace TemplateTests.Template.Models.Validators
{
    public class FindOrdersModelValidatorTests
    {
        private FindOrdersModelValidator _validator;

        public FindOrdersModelValidatorTests()
        {
            _validator = new FindOrdersModelValidator();
        }

        [Fact]
        public void ShouldHaveValidationErrorForSkip()
        {
            var model = ValidFindOrdersModel();
            model.Skip = -1;

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(nameof(model.Skip));
        }

        [Fact]
        public void ShouldNotHaveValidationErrorForSkip()
        {
            var model = ValidFindOrdersModel();

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(nameof(model.Skip));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void ShouldHaveValidationErrorForTake(int take)
        {
            var model = ValidFindOrdersModel();
            model.Take = take;

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(nameof(model.Take));
        }

        [Fact]
        public void ShouldNotHaveValidationErrorForTake()
        {
            var model = ValidFindOrdersModel();

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(nameof(model.Take));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("              ")]
        public void ShouldHaveValidationErrorForCustomer(string customer)
        {
            var model = ValidFindOrdersModel();
            model.Customer = customer;

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(nameof(model.Customer));
        }

        [Fact]
        public void ShouldNotHaveValidationErrorForCustomer()
        {
            var model = ValidFindOrdersModel();

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(nameof(model.Customer));
        }

        private FindOrdersModel ValidFindOrdersModel() =>
            new FindOrdersModel
            {
                Skip = 0,
                Take = 1,
                Customer = "Customer",
            };
    }
}
