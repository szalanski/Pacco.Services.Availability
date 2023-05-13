using Convey.CQRS.Events;
using Pacco.Services.Availability.Application.Attributes;
using System;

namespace Pacco.Services.Availability.Application.Events
{
    [Contract]
    public class ResourceResrved : IEvent
    {
        public Guid ResourceId { get; }
        public DateTime DateTime { get; }
        public ResourceResrved(Guid resourceId, DateTime dateTime)
        {
            ResourceId = resourceId;
            DateTime = dateTime;
        }
    }
}
