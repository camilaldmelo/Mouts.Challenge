using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mouts.Challenge.Application.Sales.Interfaces;
using Mouts.Challenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Mouts.Challenge.Infrastructure.Services
{
    public class EventPublisher : IEventPublisher
{
    private readonly ILogger<EventPublisher> _logger;

    public EventPublisher(ILogger<EventPublisher> logger)
    {
        _logger = logger;
    }

    public Task PublishAsync<T>(
        T @event,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Domain event published: {EventName} {@Event}",
            typeof(T).Name,
            @event);

        return Task.CompletedTask;
    }
}
}
