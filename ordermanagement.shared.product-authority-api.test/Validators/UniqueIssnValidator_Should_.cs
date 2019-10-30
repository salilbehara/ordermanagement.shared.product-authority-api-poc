using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using ordermanagement.shared.product_authority_api.Interfaces;
using ordermanagement.shared.product_authority_api.Validators;
using ordermanagement.shared.product_authority_infrastructure;
using ordermanagement.shared.product_authority_infrastructure.Entities;
using System;
using System.Collections.Generic;
using Xunit;

namespace ordermanagement.shared.product_authority_api.test.Validators
{
    public class UniqueIssnValidator_Should_
    {
        private class TestModelWithKey : IProductKey
        {
            public string ProductKey { get; set; }
            public string Issn { get; set; }
        }

        private class TestModelWithoutKey
        {
            public string Issn { get; set; }
        }

        private readonly Mock<IMediator> _mediatorMock;
        private readonly ProductEntity _product;
        private readonly ProductAuthorityDatabaseContext _dbContext;

        public UniqueIssnValidator_Should_()
        {
            _product = Any.ProductEntity("key", new DateTime(2019, 02, 01));
            _mediatorMock = new Mock<IMediator>();
            _dbContext = GetContextWithData();
        }

        private ProductAuthorityDatabaseContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<ProductAuthorityDatabaseContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;

            var context = new ProductAuthorityDatabaseContext(options, _mediatorMock.Object);

            context.Products.Add(_product);

            context.SaveChanges();
            return context;
        }

        [Fact]
        public void Be_Valid_If_Product_Already_Has_Same_Print_Issn()
        {
            var validator = new UniqueIssnValidator(_dbContext);
            var testModel = new TestModelWithKey
            {
                ProductKey = _product.ProductKey,
                Issn = _product.PrintIssn
            };

            Assert.True(validator.IsValid(testModel, m => m.Issn));
        }

        [Fact]
        public void Be_Invalid_If_Different_Product_Already_Has_Same_Print_Issn()
        {
            var validator = new UniqueIssnValidator(_dbContext);
            var testModel = new TestModelWithKey
            {
                ProductKey = "i'm different!",
                Issn = _product.PrintIssn
            };

            Assert.False(validator.IsValid(testModel, m => m.Issn));
        }

        [Fact]
        public void Be_Invalid_If_Any_Product_Already_Has_Same_Print_Issn()
        {
            var validator = new UniqueIssnValidator(_dbContext);
            var testModel = new TestModelWithoutKey
            {
                Issn = _product.PrintIssn
            };

            Assert.False(validator.IsValid(testModel, m => m.Issn));
        }

        [Fact]
        public void Be_Valid_If_No_Product_Has_Print_Issn_With_Key()
        {
            var validator = new UniqueIssnValidator(_dbContext);
            var testModel = new TestModelWithKey
            {
                ProductKey = "i'm different!",
                Issn = _product.PrintIssn + "X"
            };

            Assert.True(validator.IsValid(testModel, m => m.Issn));
        }

        [Fact]
        public void Be_Valid_If_No_Product_Has_Print_Issn_Without_Key()
        {
            var validator = new UniqueIssnValidator(_dbContext);
            var testModel = new TestModelWithoutKey
            {
                Issn = _product.PrintIssn + "X"
            };

            Assert.True(validator.IsValid(testModel, m => m.Issn));
        }

        [Fact]
        public void Be_Valid_If_Product_Already_Has_Same_Online_Issn()
        {
            var validator = new UniqueIssnValidator(_dbContext);
            var testModel = new TestModelWithKey
            {
                ProductKey = _product.ProductKey,
                Issn = _product.OnlineIssn
            };

            Assert.True(validator.IsValid(testModel, m => m.Issn));
        }

        [Fact]
        public void Be_Invalid_If_Different_Product_Already_Has_Same_Online_Issn()
        {
            var validator = new UniqueIssnValidator(_dbContext);
            var testModel = new TestModelWithKey
            {
                ProductKey = "i'm different!",
                Issn = _product.OnlineIssn
            };

            Assert.False(validator.IsValid(testModel, m => m.Issn));
        }
    }
}
