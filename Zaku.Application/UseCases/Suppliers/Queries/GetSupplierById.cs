using MediatR;
using Zaku.Application.DTOs.Suppliers;
using Zaku.Application.Mappers;
using Zaku.Domain.Entities;
using Zaku.Domain.Interfaces;

namespace Zaku.Application.UseCases.Suppliers.Queries
{
    public record GetSupplierByIdQuery(Guid Id) : IRequest<SupplierDto>;

    internal class GetSupplierByIdHandler : IRequestHandler<GetSupplierByIdQuery, SupplierDto>
    {
        private readonly IRepository<Supplier> _supplierRepository;

        public GetSupplierByIdHandler(IRepository<Supplier> supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<SupplierDto> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            Supplier supplier = await _supplierRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"Supplier with ID {request.Id} not found");

            return SupplierMapper.ToResponse(supplier);
        }
    }
}
