using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Photographers
{
    public class GetPhotographers
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public List<PhotographerDto> Photographers { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new Response()
                {
                    Photographers = _context.Set<Photographer>().Select(x => x.ToDto()).ToList()
                };
            }
        }
    }
}
