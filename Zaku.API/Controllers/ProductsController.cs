using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zaku.Application.DTOs.Products;
using Zaku.Application.UseCases.Products.Commands;
using Zaku.Application.UseCases.Products.Queries;

namespace Zaku.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductResponse>>> GetAll()
        {
            List<ProductResponse> products = await _mediator.Send(new GetAllProductsQuery());
            return Ok(products);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductResponse>> GetById(Guid id)
        {
            ProductResponse product = await _mediator.Send(new GetProductByIdQuery(id));
            return Ok(product);
        }

        [HttpGet("supplier/{supplierId:guid}")]
        public async Task<ActionResult<List<ProductResponse>>> GetBySupplierId(Guid supplierId)
        {
            List<ProductResponse> products = await _mediator.Send(new GetProductsBySupplierQuery(supplierId));
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponse>> Create([FromBody] CreateProductRequest request)
        {
            CreateProductCommand command = new(
                request.SupplierId,
                request.SKU,
                request.Name,
                request.Description,
                request.UnitPrice,
                request.Unit,
                request.IsActive);

            ProductResponse product = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProductResponse>> Update(Guid id, [FromBody] UpdateProductRequest request)
        {
            UpdateProductCommand command = new(
                id,
                request.SKU,
                request.Name,
                request.Description,
                request.UnitPrice,
                request.Unit,
                request.IsActive);

            ProductResponse product = await _mediator.Send(command);
            return Ok(product);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteProductCommand(id));
            return NoContent();
        }
    }
}
