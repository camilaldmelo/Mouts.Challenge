using Mouts.Challenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouts.Challenge.Application.Sales.Interfaces
{
    public interface IEventPublisher
    {
        Task PublishAsync<T>(
            T @event,
            CancellationToken cancellationToken = default);
    }
}
