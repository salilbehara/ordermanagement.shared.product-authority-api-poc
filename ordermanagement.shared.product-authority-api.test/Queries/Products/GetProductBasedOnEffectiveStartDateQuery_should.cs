using Microsoft.EntityFrameworkCore;
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
        private GetProductBasedOnEffectiveStartDateQueryHandler _handler;
        

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
            var response = await _handler.Execute(query);

            Assert.Equal(_products.First().ProductName, response.ProductName);
        }

        private ProductAuthorityDatabaseContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<ProductAuthorityDatabaseContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;
                        
            var context = new ProductAuthorityDatabaseContext(options);

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
