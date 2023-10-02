using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReceivablesShowcase.Application.Receivables;

namespace ReceivablesShowcase.Controllers
{
    [ApiController]
    [Route("receivables")]
    public class ReceivablesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReceivablesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("healthcheck")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetHealth()
        {
            return Ok("Healthy!");
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddReceivable(AddReceivableCommandEntry[] request)
        {
            var result = await _mediator.Send(new AddReceivableCommand { Items = request.ToList() });
            return Ok(result);
        }

        [HttpGet]
        [Route("statistics")]
        [ProducesResponseType(typeof(ReceivableStatisticsResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReceivablesStats()
        {
            var result = await _mediator.Send(new GetReceivableStatisticsQuery());
            return Ok(result);
        }
    }
}