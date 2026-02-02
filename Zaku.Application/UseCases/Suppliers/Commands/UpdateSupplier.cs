using MediatR;
using Zaku.Application.DTOs.Suppliers;
using Zaku.Application.Mappers;
using Zaku.Domain.Entities;
using Zaku.Domain.Interfaces;

namespace Zaku.Application.UseCases.Suppliers.Commands
{
    public record UpdateSupplierCommand(
        Guid Id,
        string? CompanyName,
        string? TaxId,
        string? ServiceRegions,
        decimal? Rating
    ) : IRequest<SupplierDto>;

    internal class UpdateSupplierHandler : IRequestHandler<UpdateSupplierCommand, SupplierDto>
    {
        private readonly IRepository<Supplier> _supplierRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSupplierHandler(IRepository<Supplier> supplierRepository, IUnitOfWork unitOfWork)
        {
            _supplierRepository = supplierRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SupplierDto> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            Supplier supplier = await _supplierRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"Supplier with ID {request.Id} not found");

            if (request.CompanyName is not null) supplier.CompanyName = request.CompanyName;
            if (request.TaxId is not null) supplier.TaxId = request.TaxId;
            if (request.ServiceRegions is not null) supplier.ServiceRegions = request.ServiceRegions;
            if (request.Rating.HasValue) supplier.Rating = request.Rating.Value;

            await _supplierRepository.UpdateAsync(supplier);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return SupplierMapper.ToResponse(supplier);
        }
    }
}
