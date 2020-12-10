using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.SocialEvents
{
    public class GetSocialEventById
    {
        public class Request : IRequest<Response> {  
            public Guid SocialEventId { get; set; }        
        }

        public class Response
        {
            public SocialEventDto SocialEvent { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var socialEvent = await _context.FindAsync<SocialEvent>(request.SocialEventId);

                return new Response() { 
                    SocialEvent = socialEvent.ToDto()
                };
            }
        }
    }
}