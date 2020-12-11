using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Engagements
{
    public class GetEngagements
    {
        public class Request : IRequest<Response> {  }

        public class Response
        {
            public List<EngagementDto> Engagements { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
			    return new Response() { 
                    Engagements = _context.Set<Engagement>().Select(x => x.ToDto()).ToList()
                };
            }
        }
    }
}
