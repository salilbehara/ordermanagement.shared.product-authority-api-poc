using FluentValidation.Validators;
using ordermanagement.shared.product_authority_infrastructure;
using System.Linq;

namespace ordermanagement.shared.product_authority_api.Validators
{
    public class SpidExistsValidator : PropertyValidator
    {
        private readonly ProductAuthorityDatabaseContext _dbContext;

        public SpidExistsValidator(ProductAuthorityDatabaseContext dbContext)
            : base("{PropertyName} does not exist.")
        {
            _dbContext = dbContext;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var spid = (int)context.PropertyValue;

            return _dbContext.Spids.Any(s => s.Spid == spid);

        }
    }
}