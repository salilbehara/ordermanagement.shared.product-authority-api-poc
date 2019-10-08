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
    public class GetAllRateTypesQuery_should
    {
        private readonly ProductAuthorityDatabaseContext _context;
        private List<RateTypeEntity> _rateTypes;
        private IRequestHandler<GetAllRateTypesQuery, GetAllRateTypesQueryDto> _handler;
        private Mock<IMediator> _mediatorMock = new Mock<IMediator>();

        public GetAllRateTypesQuery_should()
        {
            _rateTypes = new List<RateTypeEntity>(new[]
            {
                Any.RateTypeEntity()
            });
            _context = GetContextWithData();
            _handler = new GetAllRateTypesQueryHandler(_context);

        }

        [Fact]
        public async void Get_Rate_Types()
        {
            var query = new GetAllRateTypesQuery();
            var response = await _handler.Handle(query, default);

            Assert.Equal(_rateTypes.First().RateTypeCode, response.RateTypes.First().RateTypeCode);
            Assert.Equal(_rateTypes.First().RateTypeName, response.RateTypes.First().RateTypeName);
        }

        private ProductAuthorityDatabaseContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<ProductAuthorityDatabaseContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;
                        
            var context = new ProductAuthorityDatabaseContext(options, _mediatorMock.Object);


            context.RateTypes.AddRange(_rateTypes);
            
            context.SaveChanges();
            return context;
        }
    }
}
