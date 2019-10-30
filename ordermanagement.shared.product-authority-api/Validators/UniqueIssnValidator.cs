using FluentValidation.Validators;
using ordermanagement.shared.product_authority_api.Interfaces;
using ordermanagement.shared.product_authority_infrastructure;
using ordermanagement.shared.product_authority_infrastructure.Entities;
using System.Linq;

namespace ordermanagement.shared.product_authority_api.Validators
{
    public class UniqueIssnValidator : PropertyValidator
    {
        private readonly ProductAuthorityDatabaseContext _dbContext;

        public UniqueIssnValidator(ProductAuthorityDatabaseContext dbContext)
            : base("{PropertyName} is already in use on a different product.")
        {
            _dbContext = dbContext;
        }

        private IQueryable<ProductEntity> FilterByProductKeyIfAvailable(IQueryable<ProductEntity> products, string productKey)
        {
            if (productKey == null)
            {
                return products;
            }

            return products.Where(p => p.ProductKey != productKey);
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var productKey = (context.Instance as IProductKey)?.ProductKey;
            var issn = context.PropertyValue as string;

            var products = FilterByProductKeyIfAvailable(_dbContext.Products, productKey)
                                .Where(p => (p.PrintIssn == issn) || (p.OnlineIssn == issn));

            return !products.Any();

        }
    }
}