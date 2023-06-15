﻿using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Convey.MessageBrokers.Outbox;
using Pacco.Services.Availability.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pacco.Services.Availability.Infrastructure.Services
{
    internal sealed class MessageBroker : IMessageBroker
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IMessageOutbox _outbox;
        private readonly OutboxOptions _outboxOptions;
        private readonly IMessagePropertiesAccessor _propertiesAccessor;
        private readonly ICorrelationContextAccessor _correlationContextAccessor;

        public MessageBroker(
            IBusPublisher busPublisher,
            IMessageOutbox outbox,
            OutboxOptions outboxOptions,
            IMessagePropertiesAccessor propertiesAccessor,
            ICorrelationContextAccessor correlationContextAccessor)
        {
            _busPublisher = busPublisher;
            _outbox = outbox;
            _outboxOptions = outboxOptions;
            _propertiesAccessor = propertiesAccessor;
            _correlationContextAccessor = correlationContextAccessor;
        }

        public Task PublishAsync(params IEvent[] events)
        => PublishAsync(events.AsEnumerable());

        public async Task PublishAsync(IEnumerable<IEvent> events)
        {
            if (events is null)
            {
                return;
            }

            var messageProperties = _propertiesAccessor.MessageProperties;
            var originatedMessageId = messageProperties?.MessageId;
            var correlationId = messageProperties?.CorrelationId;
            var correlationContext = _correlationContextAccessor.CorrelationContext;

            foreach (var @event in events)
            {
                if (@event is null)
                    continue;

                var messageId = Guid.NewGuid().ToString("N");
                if (_outboxOptions.Enabled)
                {
                    await _outbox.SendAsync(@event, originatedMessageId, messageId, correlationId, messageContext: correlationContext);
                    continue;
                }
                await _busPublisher.PublishAsync(@event, messageId, correlationId, messageContext: correlationContext);
            }
        }
    }
}
