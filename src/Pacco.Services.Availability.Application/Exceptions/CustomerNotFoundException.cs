using System;

namespace Pacco.Services.Availability.Application.Exceptions
{
    public  class CustomerNotFoundException : AppException
    {
        public override string Code => "customer_nof_found";

        public Guid CustomerId { get; }

        public CustomerNotFoundException(Guid customerId) : base($"Customer with id {customerId} was not found.")
        {
            CustomerId = customerId;
        }
    }
}
