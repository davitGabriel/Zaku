using MediatR;
using Zaku.Application.DTOs.Orders;
using Zaku.Application.Mappers;
using Zaku.Domain.Entities;
using Zaku.Domain.Enums;
using Zaku.Domain.Interfaces;

namespace Zaku.Application.UseCases.Orders.Commands
{
    public record CreateOrderCommand(
        Guid ClientId,
        Guid SupplierId,
        OrderType OrderType,
        DateTime RequestedDeliveryDate,
        List<CreateOrderItemRequest> Items
    ) : IRequest<OrderResponse>;

    internal class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderResponse>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderHandler(
            IRepository<Order> orderRepository,
            IRepository<OrderItem> orderItemRepository,
            IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            Order order = new()
            {
                Id = Guid.NewGuid(),
                OrderNumber = $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}",
                ClientId = request.ClientId,
                SupplierId = request.SupplierId,
                OrderType = request.OrderType,
                Status = OrderStatus.Pending,
                RequestedDeliveryDate = request.RequestedDeliveryDate,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            List<OrderItem> orderItems = new();
            decimal totalAmount = 0;

            foreach (CreateOrderItemRequest itemRequest in request.Items)
            {
                decimal totalPrice = itemRequest.Quantity * itemRequest.UnitPrice;
                totalAmount += totalPrice;

                orderItems.Add(new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    ProductID = itemRequest.ProductId,
                    Quantity = itemRequest.Quantity,
                    UnitPrice = itemRequest.UnitPrice,
                    TotalPrice = totalPrice
                });
            }

            order.TotalAmount = totalAmount;

            await _orderRepository.AddAsync(order);
            foreach (OrderItem item in orderItems)
            {
                await _orderItemRepository.AddAsync(item);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return OrderMapper.ToResponse(order, orderItems);
        }
    }
}
