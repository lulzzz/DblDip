using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShootQ.Domain.Features.Weddings;
using System.Net;
using System.Threading.Tasks;

namespace ShootQ.Api.Controllers
{
    [ApiController]
    [Route("api/weddings")]
    public class WeddingsController
    {
        private readonly IMediator _mediator;

        public WeddingsController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost(Name = "CreateWeddingRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateWedding.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateWedding.Response>> Create([FromBody]CreateWedding.Request request)
            => await _mediator.Send(request);

        [Authorize]
        [HttpPost("{weddingId}/quote", Name = "QuoteWeddingRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(QuoteWedding.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<QuoteWedding.Response>> Quote([FromRoute] QuoteWedding.Request request)
            => await _mediator.Send(request);
        
    }
}
