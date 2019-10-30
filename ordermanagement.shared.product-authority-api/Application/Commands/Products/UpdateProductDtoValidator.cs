using FluentValidation;
using ordermanagement.shared.product_authority_api.Application.Extensions;
using ordermanagement.shared.product_authority_api.Validators;
using ordermanagement.shared.product_authority_infrastructure;
using System;
using System.Linq;

namespace ordermanagement.shared.product_authority_api.Application.Commands.Products
{
    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        private readonly ProductAuthorityDatabaseContext _context;

        public UpdateProductDtoValidator(ProductAuthorityDatabaseContext context)
        {
            _context = context;

            //Add Model State validations here. 
            //This will help avoid transmitting invalid commands to command handlers and enforce the fail fast principle.
            ////https://fluentvalidation.net/start

            RuleFor(p => p.ProductKey).NotEmpty().MaximumLength(16);
            RuleFor(p => p.EffectiveStartDate).NotEmpty();
            RuleFor(p => p.ProductName).NotEmpty().MaximumLength(128);
            RuleFor(p => p.ProductDisplayName).MaximumLength(128);
            RuleFor(p => p.PrintIssn).NotEqual(p => p.OnlineIssn)
                                     .WithMessage("Print and Online ISSN must be different.")
                                     .IsValidIssn()
                                     .DependentRules(() => RuleFor(p => p.PrintIssn).IsUniqueIssn(context));
            RuleFor(p => p.OnlineIssn).NotEqual(p => p.PrintIssn)
                                      .WithMessage("Print and Online ISSN must be different.")
                                      .IsValidIssn()
                                      .DependentRules(() => RuleFor(p => p.OnlineIssn).IsUniqueIssn(context));
            RuleFor(p => p.ProductTypeCode).MaximumLength(4);
            RuleFor(p => p.ProductStatusCode).MaximumLength(4);
            RuleFor(p => p.PublisherProductCode).MaximumLength(32);

            RuleFor(p => p.EffectiveStartDate).Must(IsProductValidForDateRangeAsync)
                .WithMessage($"No product found for the specified 'Product Key' and 'Effective Start date'.");
        }

        private bool IsProductValidForDateRangeAsync(UpdateProductDto request, DateTime effectiveStartDate)
        {
            var productId = request.ProductKey.DecodeKeyToId();

            return _context.Products.Any(p => p.ProductId == productId &&
                                         p.EffectiveStartDate <= effectiveStartDate &&
                                         p.EffectiveEndDate > effectiveStartDate);
        }
    }
}
