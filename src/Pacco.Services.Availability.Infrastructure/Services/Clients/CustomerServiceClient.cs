using Convey.HTTP;
using Pacco.Services.Availability.Application.DTO;
using Pacco.Services.Availability.Application.Services.Clients;
using System;
using System.Threading.Tasks;

namespace Pacco.Services.Availability.Infrastructure.Services.Clients
{
    internal sealed class CustomerServiceClient : ICustomersServiceClient
    {
        private readonly IHttpClient _httpClient;
        private readonly string _url;

        public CustomerServiceClient(IHttpClient httpClient, HttpClientOptions options)
        {
            _httpClient = httpClient;
            _url = options.Services["customers"];
        }

        public Task<CustomerStateDto> GetStateAsync(Guid customerId)
        {
            return _httpClient.GetAsync<CustomerStateDto>($"{_url}/customers/{customerId}/state");
        }
    }
}
