﻿using Convey.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pacco.Services.Availability.Application.Events
{
    public class ResourceReservationCancelled : IEvent
    {
        public Guid ResourceId { get; }
        public DateTime DateTime { get; }

        public ResourceReservationCancelled(Guid resourceId, DateTime dateTime)
        {
            ResourceId = resourceId;
            DateTime = dateTime;
        }
    }
}
