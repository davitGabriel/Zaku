using MediatR;
using Zaku.Application.DTOs.Products;
using Zaku.Application.Mappers;
using Zaku.Domain.Entities;
using Zaku.Domain.Interfaces;

namespace Zaku.Application.UseCases.Products.Queries
{
    public record GetAllProductsQuery : IRequest<List<ProductResponse>>;

    internal class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<ProductResponse>>
    {
        private readonly IRepository<Product> _productRepository;

        public GetAllProductsHandler(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            List<Product> products = await _productRepository.GetAllAsync();
            return products.Select(ProductMapper.ToResponse).ToList();
        }
    }
}
