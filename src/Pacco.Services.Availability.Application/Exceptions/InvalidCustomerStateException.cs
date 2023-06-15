using System;

namespace Pacco.Services.Availability.Application.Exceptions
{
    public class InvalidCustomerStateException : AppException
    {
        public override string Code => "invalid_customer_state_exception:";

        public Guid CustomerId { get; }
        public string State { get; }

        public InvalidCustomerStateException(Guid customerId, string state) : base($"Customer with id: {customerId} has invalid state: {state}")
        {
            CustomerId = customerId;
            State = state;
        }
    }
}
