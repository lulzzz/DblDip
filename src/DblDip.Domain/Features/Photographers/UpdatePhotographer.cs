using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Photographers
{
    public class UpdatePhotographer
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Photographer).NotNull();
                RuleFor(request => request.Photographer).SetValidator(new PhotographerValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public PhotographerDto Photographer { get; init; }
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

                var photographer = await _context.FindAsync<Photographer>(request.Photographer.PhotographerId);

                photographer.Update();

                _context.Store(photographer);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Photographer = photographer.ToDto()
                };
            }
        }
    }
}
