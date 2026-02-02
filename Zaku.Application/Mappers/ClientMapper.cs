using Zaku.Application.DTOs.Clients;
using Zaku.Domain.Entities;

namespace Zaku.Application.Mappers
{
    internal static class ClientMapper
    {
        public static ClientResponse ToResponse(Client client)
        {
            return new ClientResponse(
                client.Id,
                client.UserId,
                client.CompanyName,
                client.TaxId,
                client.DefaultDeliveryAddress
            );
        }
    }
}
