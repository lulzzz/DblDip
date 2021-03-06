using BuildingBlocks.Abstractions;
using FluentValidation;
using MediatR;
using DblDip.Core.Models;
using DblDip.Core.ValueObjects;
using System;
using System.Threading;
using System.Threading.Tasks;
using static DblDip.Core.Constants.Rates;
using DblDip.Domain.IntegrationEvents;
using DblDip.Domain.Features.WeddingQuotes;

namespace DblDip.Domain.Features.Quotes
{
    public class CreateWeddingQuote
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Response>
        {
            public Guid WeddingId { get; init; }
            public string Email { get; init; }
        }

        public class Response
        {
            public WeddingQuoteDto Quote { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            private readonly IDateTime _dateTime;
            private readonly IMediator _mediator;

            public Handler(IAppDbContext context, IDateTime dateTime, IMediator mediator)
            {
                _context = context;
                _dateTime = dateTime;
                _mediator = mediator;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var wedding = await _context.FindAsync<Wedding>(request.WeddingId);

                var rate = await _context.FindAsync<Rate>(PhotographyRate);

                var quote = new WeddingQuote((Email)request.Email, wedding, rate);

                _context.Store(quote);

                await _context.SaveChangesAsync(default);

                await _mediator.Publish(new QuoteCreated(quote));

                return new Response()
                {
                    Quote = quote.ToDto()
                };
            }
        }
    }
}
