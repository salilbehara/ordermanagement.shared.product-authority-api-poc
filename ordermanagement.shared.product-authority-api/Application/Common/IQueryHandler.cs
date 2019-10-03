using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_api.Application.Common
{
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<TResult> Execute(TQuery query);
    }
}
