using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using ordermanagement.shared.product_authority_api.Application.Commands.Products;
using ordermanagement.shared.product_authority_infrastructure;
using ordermanagement.shared.product_authority_infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ordermanagement.shared.product_authority_api.test.Commands.Products
{
    public class UpdateProductCommandHandler_should
    {
        private readonly ProductAuthorityDatabaseContext _context;
        private readonly IRequestHandler<UpdateProductCommand> _handler;
        private UpdateProductCommand _command;
        private Mock<IMediator> _mediatorMock = new Mock<IMediator>();
        private List<ProductEntity> _products;

        public UpdateProductCommandHandler_should()
        {
            _products = new List<ProductEntity>(new[]
                        {
                Any.ProductEntity("key",new DateTime(2019, 02, 01))
            });

            _context = GetContextWithData();
            _handler = new UpdateProductCommandHandler(_context);
            _command = new UpdateProductCommand(_products.First().ProductKey, DateTime.Now, Any.String(), Any.String(), Any.String(), Any.String(), Any.String(), Any.String(), Any.String(), Any.Int());

        }

        [Fact]
        public async Task Set_Expiration_Date_On_Current_Effecive_Product()
        {
            await _handler.Handle(_command, default);

            //verify existing record was updated
            var existingProduct = _products.First();
            Assert.Single(_context.Products, p =>
            {
                return
                    p.EffectiveStartDate == existingProduct.EffectiveStartDate &&
                    p.EffectiveEndDate == _command.EffectiveStartDate;
            });
        }

        [Fact]
        public async Task Add_New_Product_Entity()
        {
            await _handler.Handle(_command, default);

            //verify new record was added
            Assert.Single(_context.Products, p =>
            {
                return
                    p.EffectiveStartDate == _command.EffectiveStartDate &&
                    p.EffectiveEndDate == new DateTime(9999, 12, 31) &&
                    p.LegacyIdSpid == _command.LegacyIdSpid &&
                    p.OnlineIssn == _command.OnlineIssn &&
                    p.PrintIssn == _command.PrintIssn &&
                    p.ProductDisplayName == _command.ProductDisplayName &&
                    p.ProductName == _command.ProductName &&
                    p.ProductStatusCode == _command.ProductStatusCode &&
                    p.ProductTypeCode == _command.ProductTypeCode &&
                    p.LegacyIdSpid == _command.LegacyIdSpid &&
                    p.PublisherProductCode == _command.PublisherProductCode;
            });
        }

        private ProductAuthorityDatabaseContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<ProductAuthorityDatabaseContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;

            var context = new ProductAuthorityDatabaseContext(options, _mediatorMock.Object);

            context.ProductStatuses.AddRange(new[]
            {
                new ProductStatusEntity {ProductStatusCode = "A", ProductStatusName = "StatusA"}
            });

            context.ProductTypes.AddRange(_products.Select(x => x.ProductType));
            context.ProductStatuses.AddRange(_products.Select(x => x.ProductStatus));
            context.Products.AddRange(_products);

            context.SaveChanges();
            return context;
        }

    }
}
