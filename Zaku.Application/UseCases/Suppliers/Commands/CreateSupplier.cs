using MediatR;
using Zaku.Application.DTOs.Suppliers;
using Zaku.Application.Mappers;
using Zaku.Domain.Entities;
using Zaku.Domain.Interfaces;

namespace Zaku.Application.UseCases.Suppliers.Commands
{
    public record CreateSupplierCommand(
        Guid UserId,
        string CompanyName,
        string? TaxId,
        string? ServiceRegions
    ) : IRequest<SupplierDto>;

    internal class CreateSupplierHandler : IRequestHandler<CreateSupplierCommand, SupplierDto>
    {
        private readonly IRepository<Supplier> _supplierRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSupplierHandler(IRepository<Supplier> supplierRepository, IUnitOfWork unitOfWork)
        {
            _supplierRepository = supplierRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SupplierDto> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            Supplier supplier = new()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                CompanyName = request.CompanyName,
                TaxId = request.TaxId,
                ServiceRegions = request.ServiceRegions,
                Rating = 0
            };

            await _supplierRepository.AddAsync(supplier);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return SupplierMapper.ToResponse(supplier);
        }
    }
}
