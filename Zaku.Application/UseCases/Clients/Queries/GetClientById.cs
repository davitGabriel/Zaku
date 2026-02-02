using MediatR;
using Zaku.Application.DTOs.Clients;
using Zaku.Application.Mappers;
using Zaku.Domain.Entities;
using Zaku.Domain.Interfaces;

namespace Zaku.Application.UseCases.Clients.Queries
{
    public record GetClientByIdQuery(Guid Id) : IRequest<ClientResponse>;

    internal class GetClientByIdHandler : IRequestHandler<GetClientByIdQuery, ClientResponse>
    {
        private readonly IRepository<Client> _clientRepository;

        public GetClientByIdHandler(IRepository<Client> clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<ClientResponse> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            Client client = await _clientRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"Client with ID {request.Id} not found");

            return ClientMapper.ToResponse(client);
        }
    }
}
