using MediatR;
using Zaku.Domain.Entities;
using Zaku.Domain.Interfaces;

namespace Zaku.Application.UseCases.Orders.Commands
{
    public record DeleteOrderCommand(Guid Id) : IRequest;

    internal class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOrderHandler(IRepository<Order> orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            Order order = await _orderRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"Order with ID {request.Id} not found");

            await _orderRepository.DeleteAsync(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
