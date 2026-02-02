namespace Zaku.Application.DTOs.Suppliers
{
    public record CreateSupplierRequest(
        Guid UserId,
        string CompanyName,
        string? TaxId,
        string? ServiceRegions
    );

    public record UpdateSupplierRequest(
        string? CompanyName,
        string? TaxId,
        string? ServiceRegions,
        decimal? Rating
    );

    public record SupplierDto(
        Guid Id,
        Guid UserId,
        string? CompanyName,
        string? TaxId,
        string? ServiceRegions,
        decimal Rating
    );
}
