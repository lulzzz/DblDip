using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Boards
{
    public class GetBoardById
    {
        public class Request : IRequest<Response>
        {
            public Guid BoardId { get; init; }
        }

        public class Response
        {
            public BoardDto Board { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var board = await _context.FindAsync<Board>(request.BoardId);

                return new Response()
                {
                    Board = board.ToDto()
                };
            }
        }
    }
}
