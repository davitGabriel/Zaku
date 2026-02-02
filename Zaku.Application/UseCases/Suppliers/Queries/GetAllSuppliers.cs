using MediatR;
using Zaku.Application.DTOs.Suppliers;
using Zaku.Application.Mappers;
using Zaku.Domain.Entities;
using Zaku.Domain.Interfaces;

namespace Zaku.Application.UseCases.Suppliers.Queries
{
    public record GetAllSuppliersQuery : IRequest<List<SupplierDto>>;

    internal class GetAllSuppliersHandler : IRequestHandler<GetAllSuppliersQuery, List<SupplierDto>>
    {
        private readonly IRepository<Supplier> _supplierRepository;

        public GetAllSuppliersHandler(IRepository<Supplier> supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<List<SupplierDto>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
        {
            List<Supplier> suppliers = await _supplierRepository.GetAllAsync();
            return suppliers.Select(SupplierMapper.ToResponse).ToList();
        }
    }
}
