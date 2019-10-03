using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_api.Application.Common
{
    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        Task Execute(TCommand command);
    }
}
