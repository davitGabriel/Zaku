using Zaku.Domain.Enums;

namespace Zaku.Application.DTOs.Orders
{
    public record CreateOrderRequest
    {
        public Guid ClientId { get; init; }
        public Guid SupplierId { get; init; }
        public OrderType OrderType { get; init; }
        public DateTime RequestedDeliveryDate { get; init; }
        public List<CreateOrderItemRequest> Items { get; init; } = new();
    }

    public record CreateOrderItemRequest
    {
        public Guid ProductId { get; init; }
        public int Quantity { get; init; }
        public decimal UnitPrice { get; init; }
    }

    public record UpdateOrderRequest
    {
        public OrderStatus Status { get; init; }
        public DateTime? RequestedDeliveryDate { get; init; }
    }

    public record OrderResponse
    {
        public Guid Id { get; init; }
        public string? OrderNumber { get; init; }
        public Guid ClientId { get; init; }
        public Guid SupplierId { get; init; }
        public OrderType OrderType { get; init; }
        public OrderStatus Status { get; init; }
        public decimal TotalAmount { get; init; }
        public DateTime RequestedDeliveryDate { get; init; }
        public DateTime? ActualDeliveryDate { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
        public List<OrderItemResponse> Items { get; init; } = new();
    }

    public record OrderItemResponse
    {
        public Guid Id { get; init; }
        public Guid ProductId { get; init; }
        public int Quantity { get; init; }
        public decimal UnitPrice { get; init; }
        public decimal TotalPrice { get; init; }
    }
}
