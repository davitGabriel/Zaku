using MediatR;
using Zaku.Application.DTOs.Clients;
using Zaku.Application.Mappers;
using Zaku.Domain.Entities;
using Zaku.Domain.Interfaces;

namespace Zaku.Application.UseCases.Clients.Commands
{
    public record CreateClientCommand(
        Guid UserId,
        string CompanyName,
        string? TaxId,
        string? DefaultDeliveryAddress
    ) : IRequest<ClientResponse>;

    internal class CreateClientHandler : IRequestHandler<CreateClientCommand, ClientResponse>
    {
        private readonly IRepository<Client> _clientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateClientHandler(IRepository<Client> clientRepository, IUnitOfWork unitOfWork)
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ClientResponse> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            Client client = new()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                CompanyName = request.CompanyName,
                TaxId = request.TaxId,
                DefaultDeliveryAddress = request.DefaultDeliveryAddress
            };

            await _clientRepository.AddAsync(client);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ClientMapper.ToResponse(client);
        }
    }
}
