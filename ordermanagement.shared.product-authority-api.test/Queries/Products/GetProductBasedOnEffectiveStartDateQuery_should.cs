using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using ordermanagement.shared.product_authority_api.Application.Commands.Products;
using ordermanagement.shared.product_authority_api.Application.Queries.Products;
using ordermanagement.shared.product_authority_infrastructure;
using ordermanagement.shared.product_authority_infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ordermanagement.shared.product_authority_api.test.Queries.Products
{
    public class GetProductBasedOnEffectiveStartDateQuery_should
    {
        private readonly ProductAuthorityDatabaseContext _context;
        private List<ProductEntity> _products;
        private IRequestHandler<GetProductBasedOnEffectiveStartDateQuery, GetProductBasedOnEffectiveStartDateQueryDto> _handler;
        private Mock<IMediator> _mediatorMock = new Mock<IMediator>();

        public GetProductBasedOnEffectiveStartDateQuery_should()
        {
            _products = new List<ProductEntity>(new[]
            {
                Any.ProductEntity("key",new DateTime(2019, 01, 01),new DateTime(2019, 02, 01)),
                Any.ProductEntity("key",new DateTime(2019, 02, 01))
            });

            _context = GetContextWithData();
            _handler = new GetProductBasedOnEffectiveStartDateQueryHandler(_context);


        }

        [Fact]
        public async void Get_Expired_Product_By_Effective_Date()
        {
            var effectiveDate = new DateTime(2019, 01, 15);
            var query = new GetProductBasedOnEffectiveStartDateQuery("key", effectiveDate);
            var response = await _handler.Handle(query, default);

            Assert.Equal(_products.First().ProductName, response.ProductName);
        }

        private ProductAuthorityDatabaseContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<ProductAuthorityDatabaseContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;
                        
            var context = new ProductAuthorityDatabaseContext(options, _mediatorMock.Object);

            context.Products.AddRange(_products);
            
            context.SaveChanges();
            return context;
        }
    }
}
