using System;
using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_api.Application.Common
{
    public class QueryProcessor : IQueryProcessor
    {
        private readonly IServiceProvider _provider;

        public QueryProcessor(IServiceProvider provider)
        {
            _provider = provider;
        }

        Task<TResult> IQueryProcessor.Process<TResult>(IQuery<TResult> query)
        {
            var handlerType = typeof(IQueryHandler<,>)
                .MakeGenericType(query.GetType(), typeof(TResult));

            dynamic handler = _provider.GetService(handlerType);

            return handler.Execute((dynamic)query);
        }
    }
}
