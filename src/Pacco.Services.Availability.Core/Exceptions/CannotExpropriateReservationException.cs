using System;

namespace Pacco.Services.Availability.Core.Exceptions
{
    public class CannotExpropriateReservationException : DomainException
    {
        public override string Code => "cannot_expropriate_reservation";

        public Guid ResourceId { get; }

        public CannotExpropriateReservationException(Guid resourceId, DateTime date) :
            base($"Cannot expropriate resource: {resourceId} reservation at: {date}")
        {
            ResourceId = resourceId;
        }
    }
}
