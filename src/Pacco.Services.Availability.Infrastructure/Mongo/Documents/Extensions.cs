using Pacco.Services.Availability.Application.DTO;
using Pacco.Services.Availability.Core.Entities;
using Pacco.Services.Availability.Core.ValueObjects;
using System;
using System.Linq;

namespace Pacco.Services.Availability.Infrastructure.Mongo.Documents
{
    internal static class Extensions
    {
        public static Resource AsEntity(this ResourceDocument document)
        => new Resource(
                document.Id,
                document.Tags,
                document.Reservations.Select(d => new Reservation(d.TimeStamp.AsDateTime(), d.Priority)),
                document.Version);

        public static ResourceDocument AsDocument(this Resource resource)
           => new ResourceDocument
           {
               Id = resource.Id,
               Tags = resource.Tags,
               Version = resource.Version,
               Reservations = resource.Reservations.Select(r => new ReservationDocument
               {
                   Priority = r.Priority,
                   TimeStamp = r.DateTime.AsDaysSinceEpoch()
               })
           };

        public static int AsDaysSinceEpoch(this DateTime dateTime)
            => (dateTime - new DateTime()).Days;

        public static DateTime AsDateTime(this int daysSinceEpoch)
            => new DateTime().AddDays(daysSinceEpoch);

        public static ResourceDto AsDto(this ResourceDocument document)
        => new ResourceDto
        {
            Id = document.Id,
            Tags = document.Tags ?? Enumerable.Empty<string>(),
            Reservations = document.Reservations?.Select(r => new ReservationDto
            {
                DateTime = r.TimeStamp.AsDateTime(),
                Priority = r.Priority
            }) ?? Enumerable.Empty<ReservationDto>()
        };
    }
}
