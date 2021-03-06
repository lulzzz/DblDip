using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Engagements
{
    public class CreateEngagement
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Engagement).NotNull();
                RuleFor(request => request.Engagement).SetValidator(new EngagementValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public EngagementDto Engagement { get; init; }
        }

        public class Response
        {
            public EngagementDto Engagement { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var engagement = new Engagement();

                _context.Store(engagement);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Engagement = engagement.ToDto()
                };
            }
        }
    }
}
