using MediatR;
using Zaku.Application.DTOs.Products;
using Zaku.Application.Mappers;
using Zaku.Domain.Entities;
using Zaku.Domain.Interfaces;

namespace Zaku.Application.UseCases.Products.Queries
{
    public record GetProductByIdQuery(Guid Id) : IRequest<ProductResponse>;

    internal class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
    {
        private readonly IRepository<Product> _productRepository;

        public GetProductByIdHandler(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Product product = await _productRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"Product with ID {request.Id} not found");

            return ProductMapper.ToResponse(product);
        }
    }
}
