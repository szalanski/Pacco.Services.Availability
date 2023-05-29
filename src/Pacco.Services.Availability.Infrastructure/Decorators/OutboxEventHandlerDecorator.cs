using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Convey.MessageBrokers.Outbox;
using System;
using System.Threading.Tasks;

namespace Pacco.Services.Availability.Infrastructure.Decorators
{
    internal sealed class OutboxEventHandlerDecorator<T> : IEventHandler<T> where T : class, IEvent
    {
        private readonly IEventHandler<T> _handler;
        private readonly IMessageOutbox _outbox;
        private readonly bool _enabled;
        private string _messageId;

        public OutboxEventHandlerDecorator(
            IEventHandler<T> handler,
            IMessageOutbox outbox,
            OutboxOptions outboxOptions,
            IMessagePropertiesAccessor propertiesAccessor)
        {
            _handler = handler;
            _outbox = outbox;
            _enabled = outboxOptions.Enabled;
            var messageProperties = propertiesAccessor.MessageProperties;
            _messageId = string.IsNullOrWhiteSpace(messageProperties?.MessageId)
                ? Guid.NewGuid().ToString("N")
                : messageProperties.MessageId;

        }
        public Task HandleAsync(T @event)
        {
            return _enabled
                ? _outbox.HandleAsync(_messageId, () => _handler.HandleAsync(@event))
                : _handler.HandleAsync(@event);
        }
    }
}
