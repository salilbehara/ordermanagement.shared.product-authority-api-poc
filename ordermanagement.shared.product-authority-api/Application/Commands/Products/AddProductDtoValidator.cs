using FluentValidation;
using ordermanagement.shared.product_authority_api.Validators;
using ordermanagement.shared.product_authority_infrastructure;

namespace ordermanagement.shared.product_authority_api.Application.Commands.Products
{
    public class AddProductDtoValidator : AbstractValidator<AddProductDto>
    {
        public AddProductDtoValidator(ProductAuthorityDatabaseContext context)
        {
            //Add Model State validations here. 
            //This will help avoid transmitting invalid commands to command handlers and enforce the fail fast principle.
            //https://fluentvalidation.net/start

            RuleFor(p => p.ProductName).NotEmpty().MaximumLength(128);
            RuleFor(p => p.ProductDisplayName).MaximumLength(128);
            RuleFor(p => p.PublisherId).NotEmpty();
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
            RuleFor(p => p.LegacyIdSpid).IsSpidFormatValid()
                                        .DependentRules(() => RuleFor(p => p.LegacyIdSpid).DoesSpidExist(context));
        }
    }
}
