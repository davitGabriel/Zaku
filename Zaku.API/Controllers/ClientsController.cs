using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zaku.Application.DTOs.Clients;
using Zaku.Application.UseCases.Clients.Commands;
using Zaku.Application.UseCases.Clients.Queries;

namespace Zaku.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClientResponse>>> GetAll()
        {
            List<ClientResponse> clients = await _mediator.Send(new GetAllClientsQuery());
            return Ok(clients);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ClientResponse>> GetById(Guid id)
        {
            ClientResponse client = await _mediator.Send(new GetClientByIdQuery(id));
            return Ok(client);
        }

        [HttpPost]
        public async Task<ActionResult<ClientResponse>> Create([FromBody] CreateClientRequest request)
        {
            CreateClientCommand command = new(
                request.UserId,
                request.CompanyName,
                request.TaxId,
                request.DefaultDeliveryAddress);

            ClientResponse client = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ClientResponse>> Update(Guid id, [FromBody] UpdateClientRequest request)
        {
            UpdateClientCommand command = new(
                id,
                request.CompanyName,
                request.TaxId,
                request.DefaultDeliveryAddress);

            ClientResponse client = await _mediator.Send(command);
            return Ok(client);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteClientCommand(id));
            return NoContent();
        }
    }
}
