using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Feedbacks
{
    public class GetFeedbackById
    {
        public class Request : IRequest<Response>
        {
            public Guid FeedbackId { get; init; }
        }

        public class Response
        {
            public FeedbackDto Feedback { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var feedback = await _context.FindAsync<Feedback>(request.FeedbackId);

                return new Response()
                {
                    Feedback = feedback.ToDto()
                };
            }
        }
    }
}
