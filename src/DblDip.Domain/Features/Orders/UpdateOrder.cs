using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Orders
{
    public class UpdateOrder
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Order).NotNull();
                RuleFor(request => request.Order).SetValidator(new OrderValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public OrderDto Order { get; init; }
        }

        public class Response
        {
            public OrderDto Order { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var order = await _context.FindAsync<Order>(request.Order.OrderId);

                order.Update();

                _context.Store(order);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Order = order.ToDto()
                };
            }
        }
    }
}
