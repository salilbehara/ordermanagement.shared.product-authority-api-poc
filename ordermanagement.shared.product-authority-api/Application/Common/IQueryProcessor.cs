using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_api.Application.Common
{
    public interface IQueryProcessor
    {
        Task<TResult> Process<TResult>(IQuery<TResult> query);
    }
}
