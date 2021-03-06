using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Meetings
{
    public class CreateMeeting
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Meeting).NotNull();
                RuleFor(request => request.Meeting).SetValidator(new MeetingValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public MeetingDto Meeting { get; init; }
        }

        public class Response
        {
            public MeetingDto Meeting { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var meeting = new Meeting();

                _context.Store(meeting);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Meeting = meeting.ToDto()
                };
            }
        }
    }
}
