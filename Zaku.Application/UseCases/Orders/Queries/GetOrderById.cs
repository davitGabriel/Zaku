using MediatR;
using Zaku.Application.DTOs.Orders;
using Zaku.Application.Mappers;
using Zaku.Domain.Entities;
using Zaku.Domain.Interfaces;

namespace Zaku.Application.UseCases.Orders.Queries
{
    public record GetOrderByIdQuery(Guid Id) : IRequest<OrderResponse>;

    internal class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderResponse>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;

        public GetOrderByIdHandler(
            IRepository<Order> orderRepository,
            IRepository<OrderItem> orderItemRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
        }

        public async Task<OrderResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            Order order = await _orderRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"Order with ID {request.Id} not found");

            List<OrderItem> items = await _orderItemRepository.GetAllAsync(i => i.OrderId == request.Id);
            return OrderMapper.ToResponse(order, items);
        }
    }
}
