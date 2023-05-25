using Convey.CQRS.Events;
using Pacco.Services.Availability.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pacco.Services.Availability.Application.Services
{
    public interface IEventMapper
    {
        IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events);
        IEvent Map(IDomainEvent @event);
    }
}
