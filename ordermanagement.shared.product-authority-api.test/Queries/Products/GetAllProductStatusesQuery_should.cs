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
    public class GetAllProductStatusesQuery_should
    {
        private readonly ProductAuthorityDatabaseContext _context;
        private List<ProductStatusEntity> _productStatuses;
        private IRequestHandler<GetAllProductStatusesQuery, GetAllProductStatusesQueryDto> _handler;
        private Mock<IMediator> _mediatorMock = new Mock<IMediator>();

        public GetAllProductStatusesQuery_should()
        {
            _productStatuses = new List<ProductStatusEntity>(new[]
            {
                Any.ProductStatusEntity()
            });
            _context = GetContextWithData();
            _handler = new GetAllProductStatusesQueryHandler(_context);

        }

        [Fact]
        public async void Get_Product_Statuses()
        {
            var query = new GetAllProductStatusesQuery();
            var response = await _handler.Handle(query, default);

            Assert.Equal(_productStatuses.First().ProductStatusCode, response.ProductStatuses.First().ProductStatusCode);
            Assert.Equal(_productStatuses.First().ProductStatusName, response.ProductStatuses.First().ProductStatusName);
        }

        private ProductAuthorityDatabaseContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<ProductAuthorityDatabaseContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;
                        
            var context = new ProductAuthorityDatabaseContext(options, _mediatorMock.Object);

            context.ProductStatuses.AddRange(_productStatuses);
            
            context.SaveChanges();
            return context;
        }
    }
}
