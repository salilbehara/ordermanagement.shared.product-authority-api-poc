using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_api.Application.Events
{
    public class ProductChangedPublishToSnsTopicEventHandler : INotificationHandler<ProductChangedEvent>
    {
        public Task Handle(ProductChangedEvent notification, CancellationToken cancellationToken)
        {
            //Publish Messages to SNS Topic
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(notification);
            Console.WriteLine(jsonString);

            return Task.CompletedTask;
        }
    }
}
