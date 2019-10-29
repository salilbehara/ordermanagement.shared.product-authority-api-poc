using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentValidation;
using FluentValidation.TestHelper;
using ordermanagement.shared.product_authority_api.Application.Commands.Products;
using ordermanagement.shared.product_authority_api.Validators;

namespace ordermanagement.shared.product_authority_api.test.Validators
{
    public class IssnValidator_Should_
    {
        private class IssnValidationTester : AbstractValidator<string>
        {
            public string Issn { get; }

            public IssnValidationTester(string issn)
            {
                Issn = issn;

                RuleFor(p=> p).IsValidIssn();
            }
        }

        [Theory,
         InlineData("0000-0000", true),
         InlineData("00000000", true),
         InlineData("1000-002X", true),
         InlineData("1000002X", true),
         InlineData("0000-0001", false),
         InlineData("1000-0020", false),
         InlineData("abcd-efgh", false),
         InlineData("", false),
         InlineData(null, false),
         InlineData("0000-0000-0000-0000", false)]
        public void Return_Valid_Or_Invalid_As_Expected(string issn, bool shouldBeValid)
        {
            var validationTester = new IssnValidationTester(issn);
            var result = validationTester.Validate(issn);

            Assert.Equal(shouldBeValid, result.IsValid);
        }
    }
}
