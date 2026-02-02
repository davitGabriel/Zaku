using MediatR;
using Zaku.Application.DTOs.Products;
using Zaku.Application.Mappers;
using Zaku.Domain.Entities;
using Zaku.Domain.Interfaces;

namespace Zaku.Application.UseCases.Products.Commands
{
    public record CreateProductCommand(
        Guid SupplierId,
        string SKU,
        string Name,
        string? Description,
        decimal UnitPrice,
        string Unit,
        bool IsActive = true
    ) : IRequest<ProductResponse>;

    internal class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductHandler(IRepository<Product> productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = new()
            {
                Id = Guid.NewGuid(),
                SupplierId = request.SupplierId,
                SKU = request.SKU,
                Name = request.Name,
                Description = request.Description,
                UnitPrice = request.UnitPrice,
                Unit = request.Unit,
                IsActive = request.IsActive
            };

            await _productRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ProductMapper.ToResponse(product);
        }
    }
}
