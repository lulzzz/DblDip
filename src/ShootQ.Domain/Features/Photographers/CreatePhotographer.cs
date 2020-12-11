using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ShootQ.Core.ValueObjects;
using ShootQ.Domain.IntegrationEvents;

namespace ShootQ.Domain.Features.Photographers
{
    public class CreatePhotographer
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Response>
        {
            public string Name { get; set; }
            public Email Email { get; set; }
        }

        public class Response
        {
            public PhotographerDto Photographer { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            private readonly IMediator _mediator;
            public Handler(IAppDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var photographer = new Photographer(request.Name, request.Email);

                _context.Store(photographer);

                await _context.SaveChangesAsync(cancellationToken);

                await _mediator.Publish(new ProfileCreated(photographer));

                return new Response()
                {
                    Photographer = photographer.ToDto()
                };
            }
        }
    }
}
