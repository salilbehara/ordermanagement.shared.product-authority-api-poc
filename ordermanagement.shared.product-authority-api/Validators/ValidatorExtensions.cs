using FluentValidation;
using ordermanagement.shared.product_authority_infrastructure;

namespace ordermanagement.shared.product_authority_api.Validators
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> IsValidIssn<T>(this IRuleBuilder<T, string> ruleBuilder) =>
            ruleBuilder.SetValidator(new IssnFormatValidator());

        public static IRuleBuilderOptions<T, string> IsUniquePrintIssn<T>(this IRuleBuilder<T, string> ruleBuilder, ProductAuthorityDatabaseContext dbContext) =>
            ruleBuilder.SetValidator(new UniqueIssnValidator(dbContext, UniqueIssnCheckType.Print));

        public static IRuleBuilderOptions<T, string> IsUniqueOnlineIssn<T>(this IRuleBuilder<T, string> ruleBuilder, ProductAuthorityDatabaseContext dbContext) =>
            ruleBuilder.SetValidator(new UniqueIssnValidator(dbContext, UniqueIssnCheckType.Online));
    }
}
