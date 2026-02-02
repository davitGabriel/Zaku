using MediatR;
using Zaku.Domain.Entities;
using Zaku.Domain.Interfaces;

namespace Zaku.Application.UseCases.Clients.Commands
{
    public record DeleteClientCommand(Guid Id) : IRequest;

    internal class DeleteClientHandler : IRequestHandler<DeleteClientCommand>
    {
        private readonly IRepository<Client> _clientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteClientHandler(IRepository<Client> clientRepository, IUnitOfWork unitOfWork)
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            Client client = await _clientRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"Client with ID {request.Id} not found");

            await _clientRepository.DeleteAsync(client);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
