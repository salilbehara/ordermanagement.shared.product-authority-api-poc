using System;
using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_api.Application.Common
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly IServiceProvider _provider;

        public CommandProcessor(IServiceProvider provider)
        {
            _provider = provider;
        }

        public Task Process(ICommand command)
        {
            var handlerType = typeof(ICommandHandler<>)
                .MakeGenericType(command.GetType());

            dynamic handler = _provider.GetService(handlerType);

            return handler.Execute((dynamic)command);
        }
    }
}
