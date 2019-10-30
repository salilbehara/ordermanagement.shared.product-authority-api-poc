using FluentValidation;
using ordermanagement.shared.product_authority_infrastructure;

namespace ordermanagement.shared.product_authority_api.Validators
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> IsValidIssn<T>(this IRuleBuilder<T, string> ruleBuilder) =>
            ruleBuilder.SetValidator(new IssnFormatValidator());

        public static IRuleBuilderOptions<T, string> IsUniqueIssn<T>(this IRuleBuilder<T, string> ruleBuilder, ProductAuthorityDatabaseContext dbContext) =>
            ruleBuilder.SetValidator(new UniqueIssnValidator(dbContext));

        public static IRuleBuilderOptions<T, int> DoesSpidExist<T>(this IRuleBuilder<T, int> ruleBuilder, ProductAuthorityDatabaseContext dbContext) =>
            ruleBuilder.SetValidator(new SpidExistsValidator(dbContext));

        public static IRuleBuilderOptions<T, int> IsSpidFormatValid<T>(this IRuleBuilder<T, int> ruleBuilder) =>
            ruleBuilder.GreaterThanOrEqualTo(0)
                       .LessThanOrEqualTo(999999999)
                       .WithMessage("{PropertyName} must not be more than 9 digits."); 
    }
}
