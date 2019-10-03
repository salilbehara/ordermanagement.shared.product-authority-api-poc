using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_api.Application.Common
{
    public interface ICommandProcessor
    {
        Task Process(ICommand command);
    }
}
