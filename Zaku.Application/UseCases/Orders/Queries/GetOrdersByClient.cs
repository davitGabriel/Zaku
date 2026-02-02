using MediatR;
using Zaku.Application.DTOs.Orders;
using Zaku.Application.Mappers;
using Zaku.Domain.Entities;
using Zaku.Domain.Interfaces;

namespace Zaku.Application.UseCases.Orders.Queries
{
    public record GetOrdersByClientQuery(Guid ClientId) : IRequest<List<OrderResponse>>;

    internal class GetOrdersByClientHandler : IRequestHandler<GetOrdersByClientQuery, List<OrderResponse>>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;

        public GetOrdersByClientHandler(
            IRepository<Order> orderRepository,
            IRepository<OrderItem> orderItemRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
        }

        public async Task<List<OrderResponse>> Handle(GetOrdersByClientQuery request, CancellationToken cancellationToken)
        {
            List<Order> orders = await _orderRepository.GetAllAsync(o => o.ClientId == request.ClientId);
            List<OrderResponse> responses = new();

            foreach (Order order in orders)
            {
                List<OrderItem> items = await _orderItemRepository.GetAllAsync(i => i.OrderId == order.Id);
                responses.Add(OrderMapper.ToResponse(order, items));
            }

            return responses;
        }
    }
}
