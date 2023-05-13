﻿using Convey.CQRS.Events;
using Pacco.Services.Availability.Application.Attributes;
using System;

namespace Pacco.Services.Availability.Application.Events.Rejected
{
    [Contract]
    public class ResourceReservedRejected : IRejectedEvent
    {
        public Guid ResourceId { get; set; }
        public string Reason { get; }
        public string Code { get; }

        public ResourceReservedRejected(Guid resourceId, string reason, string code)
        {
            ResourceId = resourceId;
            Reason = reason;
            Code = code;
        }
    }
}
