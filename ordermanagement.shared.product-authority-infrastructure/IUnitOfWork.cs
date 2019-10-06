using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<bool> SaveChangesAndPublishEventsAsync(IReadOnlyCollection<INotification> domainEvents, CancellationToken cancellationToken = default);
    }
}