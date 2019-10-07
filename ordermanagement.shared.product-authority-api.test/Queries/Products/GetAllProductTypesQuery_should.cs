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
    public class GetAllProductTypesQuery_should
    {
        private readonly ProductAuthorityDatabaseContext _context;
        private List<ProductTypeEntity> _productTypes;
        private IRequestHandler<GetAllProductTypesQuery, GetAllProductTypesQueryDto> _handler;
        private Mock<IMediator> _mediatorMock = new Mock<IMediator>();

        public GetAllProductTypesQuery_should()
        {
            _productTypes = new List<ProductTypeEntity>(new[]
            {
                Any.ProductTypeEntity()
            });
            _context = GetContextWithData();
            _handler = new GetAllProductTypesQueryHandler(_context);

        }

        [Fact]
        public async void Get_Product_Types()
        {
            var query = new GetAllProductTypesQuery();
            var response = await _handler.Handle(query, default);

            Assert.Equal(_productTypes.First().ProductTypeCode, response.ProductTypes.First().ProductTypeCode);
            Assert.Equal(_productTypes.First().ProductTypeName, response.ProductTypes.First().ProductTypeName);
        }

        private ProductAuthorityDatabaseContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<ProductAuthorityDatabaseContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;
                        
            var context = new ProductAuthorityDatabaseContext(options, _mediatorMock.Object);


            context.ProductTypes.AddRange(_productTypes);
            
            context.SaveChanges();
            return context;
        }
    }
}
