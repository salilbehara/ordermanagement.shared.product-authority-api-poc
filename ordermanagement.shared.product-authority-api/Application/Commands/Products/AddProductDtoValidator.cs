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
            RuleFor(p => p.PrintIssn).IsValidIssn().IsUniquePrintIssn(context);
            RuleFor(p => p.OnlineIssn).IsValidIssn().IsUniqueOnlineIssn(context);
            RuleFor(p => p.ProductTypeCode).MaximumLength(4);
            RuleFor(p => p.ProductStatusCode).MaximumLength(4);
            RuleFor(p => p.PublisherProductCode).MaximumLength(32);
        }
    }
}
