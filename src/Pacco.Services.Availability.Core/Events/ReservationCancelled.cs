using Pacco.Services.Availability.Core.Entities;
using Pacco.Services.Availability.Core.ValueObjects;

namespace Pacco.Services.Availability.Core.Events
{
    public class ReservationCancelled : IDomainEvent
    {
        public Resource Resource { get; set; }
        public Reservation Reservation { get; set; }
        public ReservationCancelled(Resource resource, Reservation reservation)
        {
            Resource = resource;
            Reservation = reservation;
        }
    }
}
