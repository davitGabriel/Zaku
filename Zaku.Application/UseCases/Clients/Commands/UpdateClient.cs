using MediatR;
using Zaku.Application.DTOs.Clients;
using Zaku.Application.Mappers;
using Zaku.Domain.Entities;
using Zaku.Domain.Interfaces;

namespace Zaku.Application.UseCases.Clients.Commands
{
    public record UpdateClientCommand(
        Guid Id,
        string? CompanyName,
        string? TaxId,
        string? DefaultDeliveryAddress
    ) : IRequest<ClientResponse>;

    internal class UpdateClientHandler : IRequestHandler<UpdateClientCommand, ClientResponse>
    {
        private readonly IRepository<Client> _clientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateClientHandler(IRepository<Client> clientRepository, IUnitOfWork unitOfWork)
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ClientResponse> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            Client client = await _clientRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"Client with ID {request.Id} not found");

            if (request.CompanyName is not null) client.CompanyName = request.CompanyName;
            if (request.TaxId is not null) client.TaxId = request.TaxId;
            if (request.DefaultDeliveryAddress is not null) client.DefaultDeliveryAddress = request.DefaultDeliveryAddress;

            await _clientRepository.UpdateAsync(client);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ClientMapper.ToResponse(client);
        }
    }
}
