using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using ordermanagement.shared.product_authority_api.Validators;
using ordermanagement.shared.product_authority_infrastructure;
using ordermanagement.shared.product_authority_infrastructure.Entities;
using System;
using Xunit;

namespace ordermanagement.shared.product_authority_api.test.Validators
{
    public class SpidExistsValidator_Should_
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ProductAuthorityDatabaseContext _dbContext;

        public SpidExistsValidator_Should_()
        {
            _mediatorMock = new Mock<IMediator>();
            _dbContext = GetContextWithData();
        }

        private ProductAuthorityDatabaseContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<ProductAuthorityDatabaseContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;

            var context = new ProductAuthorityDatabaseContext(options, _mediatorMock.Object);

            context.Spids.Add(new SpidEntity { Spid = 12345 });

            context.Products.Add(Any.ProductEntity("key", new DateTime(2019, 02, 01)));

            context.SaveChanges();
            return context;
        }

        [Fact]
        public void Be_Valid_If_Spid_Is_Present_In_Db()
        {
            var validator = new SpidExistsValidator(_dbContext);

            Assert.True(validator.IsValid(12345));
        }

        [Fact]
        public void Be_Invalid_If_Spid_Is_Not_Present_In_Db()
        {
            var validator = new SpidExistsValidator(_dbContext);

            Assert.False(validator.IsValid(56789));
        }
    }
}
