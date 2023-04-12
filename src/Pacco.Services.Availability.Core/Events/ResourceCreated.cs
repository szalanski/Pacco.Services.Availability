using Pacco.Services.Availability.Core.Entities;

namespace Pacco.Services.Availability.Core.Events
{
    public class ResourceCreated : IDomainEvent
    {
        public Resource Resource { get; set; }

        public ResourceCreated(Resource resource) => Resource = resource;
    }
}
