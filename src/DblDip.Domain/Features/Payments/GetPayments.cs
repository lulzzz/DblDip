using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Payments
{
    public class GetPayments
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public List<PaymentDto> Payments { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new Response()
                {
                    Payments = _context.Set<Payment>().Select(x => x.ToDto()).ToList()
                };
            }
        }
    }
}
