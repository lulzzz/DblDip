using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Equipment
{
    public class CreateEquipment
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Equipment).NotNull();
                RuleFor(request => request.Equipment).SetValidator(new EquipmentValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public EquipmentDto Equipment { get; init; }
        }

        public class Response
        {
            public EquipmentDto Equipment { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var equipment = new DblDip.Core.Models.Equipment(request.Equipment.Name, request.Equipment.Price, request.Equipment.Description);

                _context.Store(equipment);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Equipment = equipment.ToDto()
                };
            }
        }
    }
}
