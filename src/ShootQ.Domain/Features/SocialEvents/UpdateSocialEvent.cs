using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.SocialEvents
{
    public class UpdateSocialEvent
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.SocialEvent).NotNull();
                RuleFor(request => request.SocialEvent).SetValidator(new SocialEventValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public SocialEventDto SocialEvent { get; set; }
        }

        public class Response
        {
            public SocialEventDto SocialEvent { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var socialEvent = await _context.FindAsync<SocialEvent>(request.SocialEvent.SocialEventId);

                //socialEvent.Update();

                _context.Store(socialEvent);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    SocialEvent = socialEvent.ToDto()
                };
            }
        }
    }
}
