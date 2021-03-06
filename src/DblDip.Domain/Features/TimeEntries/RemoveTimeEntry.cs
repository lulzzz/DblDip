using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace DblDip.Domain.Features.TimeEntries
{
    public class RemoveTimeEntry
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit>
        {
            public Guid TimeEntryId { get; init; }
        }

        public class Response
        {
            public TimeEntryDto TimeEntry { get; init; }
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

                var timeEntry = await _context.FindAsync<TimeEntry>(request.TimeEntryId);

                timeEntry.Remove(_dateTime.UtcNow);

                _context.Store(timeEntry);

                await _context.SaveChangesAsync(cancellationToken);

                return new();
            }
        }
    }
}
