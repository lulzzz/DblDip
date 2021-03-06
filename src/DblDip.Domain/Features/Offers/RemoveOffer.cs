using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace DblDip.Domain.Features.Offers
{
    public class RemoveOffer
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit>
        {
            public Guid OfferId { get; init; }
        }

        public class Response
        {
            public OfferDto Offer { get; init; }
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

                var offer = await _context.FindAsync<Offer>(request.OfferId);

                offer.Remove(_dateTime.UtcNow);

                _context.Store(offer);

                await _context.SaveChangesAsync(cancellationToken);

                return new();
            }
        }
    }
}
