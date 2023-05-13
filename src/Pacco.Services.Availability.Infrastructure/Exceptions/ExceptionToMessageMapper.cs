using App.Metrics.AspNetCore;
using Convey.MessageBrokers.RabbitMQ;
using Pacco.Services.Availability.Application.Commands;
using Pacco.Services.Availability.Application.Events.Rejected;
using Pacco.Services.Availability.Application.Exceptions;
using Pacco.Services.Availability.Core.Exceptions;
using System;

namespace Pacco.Services.Availability.Infrastructure.Exceptions
{
    internal sealed class ExceptionToMessageMapper : IExceptionToMessageMapper
    {
        public object Map(Exception exception, object message)
        {
            return exception switch
            {
                MissingResourceTagException ex => new AddResourceRejected(Guid.Empty, ex.Message, ex.Code),
                InvalidResourceTagException ex => new AddResourceRejected(Guid.Empty, ex.Message, ex.Code), 
                ResourceAlreadyExistsException ex => new AddResourceRejected(ex.ResourceId, ex.Message, ex.Code),
                ResourceNotFoundException ex => message switch
                {
                    ReserveResource cmd => new ResourceReservedRejected(ex.ResourceId, ex.Message, ex.Code),
                    _ => null
                },
                _ => null
            };
        }
    }
}
