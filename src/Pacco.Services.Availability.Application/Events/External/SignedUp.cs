using Convey.CQRS.Events;
using Convey.MessageBrokers;
using System;

namespace Pacco.Services.Availability.Application.Events.External
{
    [Message("identity")]
    public class SignedUp : IEvent
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public SignedUp(Guid userId, string email, string role)
        {
            UserId = userId;
            Email = email;
            Role = role;
        }
    }
}
