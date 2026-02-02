using MediatR;
using Zaku.Domain.Entities;
using Zaku.Domain.Interfaces;

namespace Zaku.Application.UseCases.Suppliers.Commands
{
    public record DeleteSupplierCommand(Guid Id) : IRequest;

    internal class DeleteSupplierHandler : IRequestHandler<DeleteSupplierCommand>
    {
        private readonly IRepository<Supplier> _supplierRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSupplierHandler(IRepository<Supplier> supplierRepository, IUnitOfWork unitOfWork)
        {
            _supplierRepository = supplierRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            Supplier supplier = await _supplierRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"Supplier with ID {request.Id} not found");

            await _supplierRepository.DeleteAsync(supplier);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
