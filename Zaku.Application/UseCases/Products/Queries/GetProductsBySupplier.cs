using MediatR;
using Zaku.Application.DTOs.Products;
using Zaku.Application.Mappers;
using Zaku.Domain.Entities;
using Zaku.Domain.Interfaces;

namespace Zaku.Application.UseCases.Products.Queries
{
    public record GetProductsBySupplierQuery(Guid SupplierId) : IRequest<List<ProductResponse>>;

    internal class GetProductsBySupplierHandler : IRequestHandler<GetProductsBySupplierQuery, List<ProductResponse>>
    {
        private readonly IRepository<Product> _productRepository;

        public GetProductsBySupplierHandler(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductResponse>> Handle(GetProductsBySupplierQuery request, CancellationToken cancellationToken)
        {
            List<Product> products = await _productRepository.GetAllAsync(p => p.SupplierId == request.SupplierId);
            return products.Select(ProductMapper.ToResponse).ToList();
        }
    }
}
