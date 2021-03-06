using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.StudioPortraits
{
    public class UpdateStudioPortrait
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.StudioPortrait).NotNull();
                RuleFor(request => request.StudioPortrait).SetValidator(new StudioPortraitValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public StudioPortraitDto StudioPortrait { get; init; }
        }

        public class Response
        {
            public StudioPortraitDto StudioPortrait { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var studioPortrait = await _context.FindAsync<StudioPortrait>(request.StudioPortrait.StudioPortraitId);

                studioPortrait.Update();

                _context.Store(studioPortrait);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    StudioPortrait = studioPortrait.ToDto()
                };
            }
        }
    }
}
