using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using ordermanagement.shared.product_authority_api.Application.Commands.Products;
using ordermanagement.shared.product_authority_api.Application.Queries.Offerings;
using ordermanagement.shared.product_authority_api.Application.Queries.Products;
using ordermanagement.shared.product_authority_api.Application.Queries.Rates;
using ordermanagement.shared.product_authority_infrastructure;
using ordermanagement.shared.product_authority_infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ordermanagement.shared.product_authority_api.test.Queries.Offerings
{
    public class GetAllDeliveryMethodsQuery_should
    {
        private readonly ProductAuthorityDatabaseContext _context;
        private List<DeliveryMethodEntity> _deliveryMethods;
        private IRequestHandler<GetAllDeliveryMethodsQuery, GetAllDeliveryMethodsQueryDto> _handler;
        private Mock<IMediator> _mediatorMock = new Mock<IMediator>();

        public GetAllDeliveryMethodsQuery_should()
        {
            _deliveryMethods = new List<DeliveryMethodEntity>(new[]
            {
                Any.DeliveryMethodEntity()
            });
            _context = GetContextWithData();
            _handler = new GetAllDeliveryMethodsQueryHandler(_context);

        }

        [Fact]
        public async void Get_Delivery_Methods()
        {
            var query = new GetAllDeliveryMethodsQuery();
            var response = await _handler.Handle(query, default);

            Assert.Equal(_deliveryMethods.First().DeliveryMethodCode, response.DeliveryMethods.First().DeliveryMethodCode);
            Assert.Equal(_deliveryMethods.First().DeliveryMethodName, response.DeliveryMethods.First().DeliveryMethodName);
        }

        private ProductAuthorityDatabaseContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<ProductAuthorityDatabaseContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;
                        
            var context = new ProductAuthorityDatabaseContext(options, _mediatorMock.Object);


            context.DeliveryMethods.AddRange(_deliveryMethods);
            
            context.SaveChanges();
            return context;
        }
    }
}
