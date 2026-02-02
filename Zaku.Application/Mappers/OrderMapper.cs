using Zaku.Application.DTOs.Orders;
using Zaku.Domain.Entities;

namespace Zaku.Application.Mappers
{
    internal static class OrderMapper
    {
        public static OrderResponse ToResponse(Order order, List<OrderItem> items)
        {
            return new OrderResponse
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                ClientId = order.ClientId,
                SupplierId = order.SupplierId,
                OrderType = order.OrderType,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                RequestedDeliveryDate = order.RequestedDeliveryDate,
                ActualDeliveryDate = order.ActualDeliveryDate,
                CreatedAt = order.CreatedAt,
                UpdatedAt = order.UpdatedAt,
                Items = items.Select(i => new OrderItemResponse
                {
                    Id = i.Id,
                    ProductId = i.ProductID,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    TotalPrice = i.TotalPrice
                }).ToList()
            };
        }
    }
}
