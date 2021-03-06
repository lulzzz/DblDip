using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Points
{
    public class UpdatePoint
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Point).NotNull();
                RuleFor(request => request.Point).SetValidator(new PointValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public PointDto Point { get; init; }
        }

        public class Response
        {
            public PointDto Point { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var point = await _context.FindAsync<Point>(request.Point.PointId);

                point.Update();

                _context.Store(point);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Point = point.ToDto()
                };
            }
        }
    }
}
