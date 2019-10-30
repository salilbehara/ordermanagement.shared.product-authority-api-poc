using ordermanagement.shared.product_authority_api.Validators;
using Xunit;

namespace ordermanagement.shared.product_authority_api.test.Validators
{
    public class IssnFormatValidator_Should_
    {
        [Theory,
         InlineData("00000000", true),
         InlineData("1000002X", true),
         InlineData("10000011", true),
         InlineData("00000001", false),
         InlineData("10000020", false),
         InlineData("abcd-efgh", false),
         InlineData("", false),
         InlineData(null, false),
         InlineData("0000-0000-0000-0000", false)]
        public void Return_Valid_Or_Invalid_As_Expected(string issn, bool shouldBeValid)
        {
            var validator = new IssnFormatValidator();

            Assert.Equal(shouldBeValid, validator.IsValid(issn));
        }
    }
}
