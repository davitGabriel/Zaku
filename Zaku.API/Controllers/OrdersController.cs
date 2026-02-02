using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zaku.Application.DTOs.Orders;
using Zaku.Application.UseCases.Orders.Commands;
using Zaku.Application.UseCases.Orders.Queries;

namespace Zaku.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderResponse>>> GetAll()
        {
            List<OrderResponse> orders = await _mediator.Send(new GetAllOrdersQuery());
            return Ok(orders);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderResponse>> GetById(Guid id)
        {
            OrderResponse order = await _mediator.Send(new GetOrderByIdQuery(id));
            return Ok(order);
        }

        [HttpGet("client/{clientId:guid}")]
        public async Task<ActionResult<List<OrderResponse>>> GetByClientId(Guid clientId)
        {
            List<OrderResponse> orders = await _mediator.Send(new GetOrdersByClientQuery(clientId));
            return Ok(orders);
        }

        [HttpGet("supplier/{supplierId:guid}")]
        public async Task<ActionResult<List<OrderResponse>>> GetBySupplierId(Guid supplierId)
        {
            List<OrderResponse> orders = await _mediator.Send(new GetOrdersBySupplierQuery(supplierId));
            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult<OrderResponse>> Create([FromBody] CreateOrderRequest request)
        {
            CreateOrderCommand command = new(
                request.ClientId,
                request.SupplierId,
                request.OrderType,
                request.RequestedDeliveryDate,
                request.Items);

            OrderResponse order = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<OrderResponse>> Update(Guid id, [FromBody] UpdateOrderRequest request)
        {
            UpdateOrderCommand command = new(id, request.Status, request.RequestedDeliveryDate);
            OrderResponse order = await _mediator.Send(command);
            return Ok(order);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteOrderCommand(id));
            return NoContent();
        }
    }
}
