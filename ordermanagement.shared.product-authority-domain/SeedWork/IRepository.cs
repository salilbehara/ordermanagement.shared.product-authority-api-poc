using System;
using System.Collections.Generic;
using System.Text;

namespace ordermanagement.shared.product_authority_domain.SeedWork
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
