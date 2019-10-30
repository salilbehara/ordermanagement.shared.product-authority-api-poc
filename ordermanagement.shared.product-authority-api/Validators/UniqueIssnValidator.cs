using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using ordermanagement.shared.product_authority_api.Interfaces;
using ordermanagement.shared.product_authority_infrastructure;
using ordermanagement.shared.product_authority_infrastructure.Entities;
using System;
using System.Linq;

namespace ordermanagement.shared.product_authority_api.Validators
{
    public class UniqueIssnValidator : PropertyValidator
    {
        private readonly ProductAuthorityDatabaseContext _dbContext;
        private readonly UniqueIssnCheckType _checkType;

        public UniqueIssnValidator(ProductAuthorityDatabaseContext dbContext, UniqueIssnCheckType checkType)
            : base("{PropertyName} is already in use on a different product.")
        {
            _dbContext = dbContext;
            _checkType = checkType;
        }

        private IQueryable<ProductEntity> FilterByProductKeyIfAvailable(IQueryable<ProductEntity> products, string productKey)
        {
            if (productKey == null)
            {
                return products;
            }

            return products.Where(p => p.ProductKey != productKey);
        }

        private IQueryable<ProductEntity> FilterByIssnAndType(IQueryable<ProductEntity> products, string issn)
        {
            switch (_checkType)
            {
                case UniqueIssnCheckType.Print:
                    return products.Where(p => p.PrintIssn == issn);

                case UniqueIssnCheckType.Online:
                    return products.Where(p => p.OnlineIssn == issn);

                default:
                    throw new ArgumentException("Issn Check Type is invalid.");
            }
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var productKey = (context.Instance as IProductKey)?.ProductKey;
            var issn = context.PropertyValue as string;

            var products = FilterByProductKeyIfAvailable(_dbContext.Products, productKey);

            products = FilterByIssnAndType(products, issn);

            return !products.Any();

        }
    }
}