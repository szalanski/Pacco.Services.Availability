using Convey.CQRS.Commands;
using Convey.MessageBrokers;
using Convey.MessageBrokers.Outbox;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace Pacco.Services.Availability.Infrastructure.Decorators
{
    internal sealed class OutboxCommandHandlerDecorator<T> : ICommandHandler<T> where T : class, ICommand
    {
        private readonly ICommandHandler<T> _handler;
        private readonly IMessageOutbox _outbox;
        private readonly bool _enabled;
        private string _messageId;

        public OutboxCommandHandlerDecorator(
            ICommandHandler<T> handler,
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
        public Task HandleAsync(T command)
        {
            return _enabled
                ? _outbox.HandleAsync(_messageId, () => _handler.HandleAsync(command))
                : _handler.HandleAsync(command);
        }
    }
}
