using MediatR;
using Zaku.Application.DTOs.Clients;
using Zaku.Application.Mappers;
using Zaku.Domain.Entities;
using Zaku.Domain.Interfaces;

namespace Zaku.Application.UseCases.Clients.Queries
{
    public record GetAllClientsQuery : IRequest<List<ClientResponse>>;

    internal class GetAllClientsHandler : IRequestHandler<GetAllClientsQuery, List<ClientResponse>>
    {
        private readonly IRepository<Client> _clientRepository;

        public GetAllClientsHandler(IRepository<Client> clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<List<ClientResponse>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            List<Client> clients = await _clientRepository.GetAllAsync();
            return clients.Select(ClientMapper.ToResponse).ToList();
        }
    }
}
