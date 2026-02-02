using Zaku.Application.DTOs.Products;
using Zaku.Domain.Entities;

namespace Zaku.Application.Mappers
{
    internal static class ProductMapper
    {
        public static ProductResponse ToResponse(Product product)
        {
            return new ProductResponse(
                product.Id,
                product.SupplierId,
                product.SKU,
                product.Name,
                product.Description,
                product.UnitPrice,
                product.Unit,
                product.IsActive
            );
        }
    }
}
