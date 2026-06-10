using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mouts.Challenge.Application.Sales.Commands.CancelSale;
using Mouts.Challenge.Application.Sales.Commands.CreateSale;
using Mouts.Challenge.Application.Sales.Commands.UpdateSale;
using Mouts.Challenge.Application.Sales.Queries.GetSaleById;
using Mouts.Challenge.Application.Sales.Queries.GetSales;

namespace Mouts.Challenge.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {

        private readonly IMediator _mediator;
        public SalesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
        [FromBody] CreateSaleCommand command,
        CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = response.Id },
                response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(
            int id,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetSaleByIdQuery(id),
                cancellationToken);

            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetSalesQuery(),
                cancellationToken);

            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] UpdateSaleCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest("Route id is different from body id.");

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Cancel(
            int id,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(
                new CancelSaleCommand(id),
                cancellationToken);

            return NoContent();
        }

        [HttpPatch("{saleId:int}/items/{itemId:int}/cancel")]
        public async Task<IActionResult> CancelItem(
            int saleId,
            int itemId,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(
                new CancelSaleItemCommand(saleId, itemId),
                cancellationToken);

            return NoContent();
        }

    }
}
