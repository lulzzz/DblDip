using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Photographers
{
    public class GetPhotographerById
    {
        public class Request : IRequest<Response>
        {
            public Guid PhotographerId { get; init; }
        }

        public class Response
        {
            public PhotographerDto Photographer { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var photographer = await _context.FindAsync<Photographer>(request.PhotographerId);

                return new Response()
                {
                    Photographer = photographer.ToDto()
                };
            }
        }
    }
}
