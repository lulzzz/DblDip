using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace ShootQ.Domain.Features.Services
{
    public class RemoveService
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit> {  
            public Guid ServiceId { get; set; }
        }

        public class Response
        {
            public ServiceDto Service { get; set; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken) {

                var service = await _context.FindAsync<Service>(request.ServiceId);

                service.Remove(default);

                _context.Store(service);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit()
                {

                };
            }
        }
    }
}
