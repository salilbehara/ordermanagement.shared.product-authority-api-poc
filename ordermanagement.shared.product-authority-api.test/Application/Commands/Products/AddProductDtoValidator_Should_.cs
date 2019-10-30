using FluentValidation.TestHelper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using ordermanagement.shared.product_authority_api.Application.Commands.Products;
using ordermanagement.shared.product_authority_infrastructure;
using ordermanagement.shared.product_authority_infrastructure.Entities;
using System;
using Xunit;

namespace ordermanagement.shared.product_authority_api.test.Application.Commands.Products
{
    public class AddProductDtoValidator_Should_
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ProductAuthorityDatabaseContext _dbContext;
        private readonly ProductEntity _product;
        private readonly AddProductDto _instance;
        private readonly AddProductDtoValidator _validator;

        public AddProductDtoValidator_Should_()
        {
            _instance = new AddProductDto
            {
                ProductName = Any.String(),
                ProductDisplayName = Any.String(),
                PublisherId = Any.Long(),
                PrintIssn = "00000019",
                OnlineIssn = "00000027",
                ProductTypeCode = Any.String(4),
                ProductStatusCode = Any.String(4),
                PublisherProductCode = Any.String(),
                LegacyIdSpid = 12345
            };

            _mediatorMock = new Mock<IMediator>();
            _product = Any.ProductEntity("key", new DateTime(2019, 02, 01));
            _dbContext = GetContextWithData();
            _validator = new AddProductDtoValidator(_dbContext);
        }

        private ProductAuthorityDatabaseContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<ProductAuthorityDatabaseContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;
            var context = new ProductAuthorityDatabaseContext(options, _mediatorMock.Object);

            context.Spids.Add(new SpidEntity { Spid = 12345 });
            context.Products.Add(_product);

            context.SaveChanges();

            return context;
        }

        [Fact]
        public void Pass_Validation()
        {
            Assert.True(_validator.TestValidate(_instance).IsValid);
        }

        [Fact]
        public void Fail_Because_ProductName_Is_Empty()
        {
            _instance.ProductName = "";

            _validator.ShouldHaveValidationErrorFor(p => p.ProductName, _instance);
        }

        [Fact]
        public void Fail_Because_ProductName_Is_Too_Long()
        {
            _instance.ProductName = Any.String(129);

            _validator.ShouldHaveValidationErrorFor(p => p.ProductName, _instance);
        }

        [Fact]
        public void Fail_Because_ProductDisplayName_Is_Too_Long()
        {
            _instance.ProductDisplayName = Any.String(129);

            _validator.ShouldHaveValidationErrorFor(p => p.ProductDisplayName, _instance);
        }

        [Fact]
        public void Fail_Because_PublisherID_Is_Empty()
        {
            _instance.PublisherId = 0;

            _validator.ShouldHaveValidationErrorFor(p => p.PublisherId, _instance);
        }

        [Fact]
        public void Fail_Because_PrintIssn_Is_Same_As_OnlineIssn()
        {
            _instance.PrintIssn = _instance.OnlineIssn;

            _validator.ShouldHaveValidationErrorFor(p => p.PrintIssn, _instance);
        }

        [Fact]
        public void Fail_Because_PrintIssn_Is_Invalid()
        {
            _instance.PrintIssn = "0000000X";

            _validator.ShouldHaveValidationErrorFor(p => p.PrintIssn, _instance);
        }

        [Fact]
        public void Fail_Because_PrintIssn_Is_Not_Unique()
        {
            _instance.PrintIssn = _product.PrintIssn;

            _validator.ShouldHaveValidationErrorFor(p => p.PrintIssn, _instance);
        }

        [Fact]
        public void Fail_Because_OnlineIssn_Is_Same_As_OnlineIssn()
        {
            _instance.OnlineIssn = _instance.PrintIssn;

            _validator.ShouldHaveValidationErrorFor(p => p.OnlineIssn, _instance);
        }

        [Fact]
        public void Fail_Because_OnlineIssn_Is_Invalid()
        {
            _instance.OnlineIssn = "0000000X";

            _validator.ShouldHaveValidationErrorFor(p => p.OnlineIssn, _instance);
        }

        [Fact]
        public void Fail_Because_OnlineIssn_Is_Not_Unique()
        {
            _instance.OnlineIssn = _product.OnlineIssn;

            _validator.ShouldHaveValidationErrorFor(p => p.OnlineIssn, _instance);
        }

        [Fact]
        public void Fail_Because_ProductTypeCode_Is_Too_Long()
        {
            _instance.ProductTypeCode = Any.String(5);

            _validator.ShouldHaveValidationErrorFor(p => p.ProductTypeCode, _instance);
        }

        [Fact]
        public void Fail_Because_ProductStatusCode_Is_Too_Long()
        {
            _instance.ProductStatusCode = Any.String(5);

            _validator.ShouldHaveValidationErrorFor(p => p.ProductStatusCode, _instance);
        }

        [Fact]
        public void Fail_Because_PublisherProductCode_Is_Too_Long()
        {
            _instance.PublisherProductCode = Any.String(33);

            _validator.ShouldHaveValidationErrorFor(p => p.PublisherProductCode, _instance);
        }

        [Fact]
        public void Fail_Because_Spid_Format_Is_Invalid()
        {
            _instance.LegacyIdSpid = -1;

            _validator.ShouldHaveValidationErrorFor(p => p.LegacyIdSpid, _instance);
        }

        [Fact]
        public void Fail_Because_Spid_Does_Not_Exist()
        {
            _instance.LegacyIdSpid = 5555;

            _validator.ShouldHaveValidationErrorFor(p => p.LegacyIdSpid, _instance);
        }
    }
}
