using MediatR;
using Zaku.Application.DTOs.Products;
using Zaku.Application.Mappers;
using Zaku.Domain.Entities;
using Zaku.Domain.Interfaces;

namespace Zaku.Application.UseCases.Products.Commands
{
    public record UpdateProductCommand(
        Guid Id,
        string? SKU,
        string? Name,
        string? Description,
        decimal? UnitPrice,
        string? Unit,
        bool? IsActive
    ) : IRequest<ProductResponse>;

    internal class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ProductResponse>
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductHandler(IRepository<Product> productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = await _productRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"Product with ID {request.Id} not found");

            if (request.SKU is not null) product.SKU = request.SKU;
            if (request.Name is not null) product.Name = request.Name;
            if (request.Description is not null) product.Description = request.Description;
            if (request.UnitPrice.HasValue) product.UnitPrice = request.UnitPrice.Value;
            if (request.Unit is not null) product.Unit = request.Unit;
            if (request.IsActive.HasValue) product.IsActive = request.IsActive.Value;

            await _productRepository.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ProductMapper.ToResponse(product);
        }
    }
}
