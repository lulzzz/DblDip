using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace DblDip.Domain.Features.Participants
{
    public class RemoveParticipant
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit>
        {
            public Guid ParticipantId { get; init; }
        }

        public class Response
        {
            public ParticipantDto Participant { get; init; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IAppDbContext _context;
            private readonly IDateTime _dateTime;

            public Handler(IAppDbContext context, IDateTime dateTime)
            {
                _context = context;
                _dateTime = dateTime;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {

                var participant = await _context.FindAsync<Participant>(request.ParticipantId);

                participant.Remove(_dateTime.UtcNow);

                _context.Store(participant);

                await _context.SaveChangesAsync(cancellationToken);

                return new();
            }
        }
    }
}
