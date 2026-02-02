using MediatR;
using Zaku.Application.DTOs.Orders;
using Zaku.Application.Mappers;
using Zaku.Domain.Entities;
using Zaku.Domain.Enums;
using Zaku.Domain.Interfaces;

namespace Zaku.Application.UseCases.Orders.Commands
{
    public record UpdateOrderCommand(
        Guid Id,
        OrderStatus Status,
        DateTime? RequestedDeliveryDate
    ) : IRequest<OrderResponse>;

    internal class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, OrderResponse>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOrderHandler(
            IRepository<Order> orderRepository,
            IRepository<OrderItem> orderItemRepository,
            IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            Order order = await _orderRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"Order with ID {request.Id} not found");

            if (request.Status != default)
                order.Status = request.Status;

            if (request.RequestedDeliveryDate.HasValue)
                order.RequestedDeliveryDate = request.RequestedDeliveryDate.Value;

            if (request.Status == OrderStatus.Delivered)
                order.ActualDeliveryDate = DateTime.UtcNow;

            order.UpdatedAt = DateTime.UtcNow;

            await _orderRepository.UpdateAsync(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            List<OrderItem> items = await _orderItemRepository.GetAllAsync(i => i.OrderId == request.Id);
            return OrderMapper.ToResponse(order, items);
        }
    }
}
