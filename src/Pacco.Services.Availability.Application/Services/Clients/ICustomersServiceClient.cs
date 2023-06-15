using Pacco.Services.Availability.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pacco.Services.Availability.Application.Services.Clients
{
    public interface ICustomersServiceClient
    {
        Task<CustomerStateDto> GetStateAsync(Guid customerId);
    }
}
