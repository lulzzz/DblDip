using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace DblDip.Domain.Features.Clients
{
    public class RemoveClient
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit>
        {
            public Guid ClientId { get; init; }
        }

        public class Response
        {
            public ClientDto Client { get; init; }
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
                var client = await _context.FindAsync<Client>(request.ClientId);

                client.Remove(_dateTime.UtcNow);

                _context.Store(client);

                await _context.SaveChangesAsync(cancellationToken);

                return new();
            }
        }
    }
}
