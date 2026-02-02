using MediatR;
using Zaku.Application.DTOs.Orders;
using Zaku.Application.Mappers;
using Zaku.Domain.Entities;
using Zaku.Domain.Interfaces;

namespace Zaku.Application.UseCases.Orders.Queries
{
    public record GetAllOrdersQuery : IRequest<List<OrderResponse>>;

    internal class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, List<OrderResponse>>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;

        public GetAllOrdersHandler(
            IRepository<Order> orderRepository,
            IRepository<OrderItem> orderItemRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
        }

        public async Task<List<OrderResponse>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            List<Order> orders = await _orderRepository.GetAllAsync();
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
