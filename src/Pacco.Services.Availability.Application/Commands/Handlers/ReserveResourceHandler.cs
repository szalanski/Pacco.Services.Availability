using Convey.CQRS.Commands;
using Pacco.Services.Availability.Application.Exceptions;
using Pacco.Services.Availability.Application.Services;
using Pacco.Services.Availability.Application.Services.Clients;
using Pacco.Services.Availability.Core.Exceptions;
using Pacco.Services.Availability.Core.Repositories;
using Pacco.Services.Availability.Core.ValueObjects;
using System.Threading.Tasks;

namespace Pacco.Services.Availability.Application.Commands.Commands
{
    public class ReserveResourceHandler : ICommandHandler<ReserveResource>
    {
        private readonly IResourcesRepository _repository;
        private readonly ICustomersServiceClient _customersServiceClient;
        private readonly IEventProcessor _eventProcessor;

        public ReserveResourceHandler(
            IResourcesRepository repository,
            ICustomersServiceClient customersServiceClient,
            IEventProcessor eventProcessor)
        {
            _repository = repository;
            _customersServiceClient = customersServiceClient;
            _eventProcessor = eventProcessor;
        }

        public async Task HandleAsync(ReserveResource command)
        {
            var resource = await _repository.GetAsync(command.ResourceId);
            if (resource == null)
            {
                throw new ResourceNotFoundException(resource.Id);
            }

            var customerState = await _customersServiceClient.GetStateAsync(command.CustomerId);
            if (customerState is null)
            {
                throw new CustomerNotFoundException(command.CustomerId);
            }

            if (!customerState.IsValid)
            {
                throw new InvalidCustomerStateException(command.CustomerId, customerState.State);
            }

            var reservation = new Reservation(command.DateTime, command.Priority);
            resource.AddReservation(reservation);

            await _repository.UpdateAsync(resource);
            await _eventProcessor.ProcessAsync(resource.Events);
        }
    }
}
