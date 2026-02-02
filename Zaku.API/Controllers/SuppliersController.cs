using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zaku.Application.DTOs.Suppliers;
using Zaku.Application.UseCases.Suppliers.Commands;
using Zaku.Application.UseCases.Suppliers.Queries;

namespace Zaku.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SuppliersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SuppliersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<SupplierDto>>> GetAll()
        {
            List<SupplierDto> suppliers = await _mediator.Send(new GetAllSuppliersQuery());
            return Ok(suppliers);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SupplierDto>> GetById(Guid id)
        {
            SupplierDto supplier = await _mediator.Send(new GetSupplierByIdQuery(id));
            return Ok(supplier);
        }

        [HttpPost]
        public async Task<ActionResult<SupplierDto>> Create([FromBody] CreateSupplierRequest request)
        {
            CreateSupplierCommand command = new(
                request.UserId,
                request.CompanyName,
                request.TaxId,
                request.ServiceRegions);

            SupplierDto supplier = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = supplier.Id }, supplier);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<SupplierDto>> Update(Guid id, [FromBody] UpdateSupplierRequest request)
        {
            UpdateSupplierCommand command = new(
                id,
                request.CompanyName,
                request.TaxId,
                request.ServiceRegions,
                request.Rating);

            SupplierDto supplier = await _mediator.Send(command);
            return Ok(supplier);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteSupplierCommand(id));
            return NoContent();
        }
    }
}
