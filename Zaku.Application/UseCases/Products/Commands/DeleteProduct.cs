using MediatR;
using Zaku.Domain.Entities;
using Zaku.Domain.Interfaces;

namespace Zaku.Application.UseCases.Products.Commands
{
    public record DeleteProductCommand(Guid Id) : IRequest;

    internal class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductHandler(IRepository<Product> productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Product product = await _productRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"Product with ID {request.Id} not found");

            await _productRepository.DeleteAsync(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
