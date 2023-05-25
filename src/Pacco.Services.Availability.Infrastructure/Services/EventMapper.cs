using Convey.CQRS.Events;
using Pacco.Services.Availability.Application.Events;
using Pacco.Services.Availability.Application.Services;
using Pacco.Services.Availability.Core.Events;
using System.Collections.Generic;
using System.Linq;

namespace Pacco.Services.Availability.Infrastructure.Services
{
    internal sealed class EventMapper : IEventMapper
    {
        public IEvent Map(IDomainEvent @event)
        {
            return @event switch
            {
                ResourceCreated e => new ResourceAdded(e.Resource.Id),
                ReservationCancelled e => new ResourceReservationCancelled(e.Resource.Id, e.Reservation.DateTime),
                ReservationAdded e => new ResourceResrved(e.Resource.Id, e.Reservation.DateTime),
                _ => null
            };
        }

        public IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events)
        => events.Select(Map);
    }
}
