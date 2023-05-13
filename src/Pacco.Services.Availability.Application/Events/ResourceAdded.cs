using Convey.CQRS.Events;
using Pacco.Services.Availability.Application.Attributes;
using System;
using System.Diagnostics.Contracts;

namespace Pacco.Services.Availability.Application.Events
{
    [Contract]
    public class ResourceAdded : IEvent
    {
        public Guid ResourceId { get; }

        public ResourceAdded(Guid resourceId)
        {
            ResourceId = resourceId;
        }
    }
}
