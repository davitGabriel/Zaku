namespace Zaku.Application.DTOs.Products
{
    public record CreateProductRequest(
        Guid SupplierId,
        string SKU,
        string Name,
        string? Description,
        decimal UnitPrice,
        string Unit,
        bool IsActive = true
    );

    public record UpdateProductRequest(
        string? SKU,
        string? Name,
        string? Description,
        decimal? UnitPrice,
        string? Unit,
        bool? IsActive
    );

    public record ProductResponse(
        Guid Id,
        Guid SupplierId,
        string? SKU,
        string? Name,
        string? Description,
        decimal UnitPrice,
        string? Unit,
        bool IsActive
    );
}
