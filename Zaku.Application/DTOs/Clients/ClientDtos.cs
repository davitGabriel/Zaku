namespace Zaku.Application.DTOs.Clients
{
    public record CreateClientRequest(
        Guid UserId,
        string CompanyName,
        string? TaxId,
        string? DefaultDeliveryAddress
    );

    public record UpdateClientRequest(
        string? CompanyName,
        string? TaxId,
        string? DefaultDeliveryAddress
    );

    public record ClientResponse(
        Guid Id,
        Guid UserId,
        string? CompanyName,
        string? TaxId,
        string? DefaultDeliveryAddress
    );
}
