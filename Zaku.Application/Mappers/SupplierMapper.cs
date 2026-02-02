using Zaku.Application.DTOs.Suppliers;
using Zaku.Domain.Entities;

namespace Zaku.Application.Mappers
{
    internal static class SupplierMapper
    {
        public static SupplierDto ToResponse(Supplier supplier)
        {
            return new SupplierDto(
                supplier.Id,
                supplier.UserId,
                supplier.CompanyName,
                supplier.TaxId,
                supplier.ServiceRegions,
                supplier.Rating
            );
        }
    }
}
