using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Libraries
{
    public class CreateLibrary
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Library).NotNull();
                RuleFor(request => request.Library).SetValidator(new LibraryValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public LibraryDto Library { get; init; }
        }

        public class Response
        {
            public LibraryDto Library { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var library = new Library();

                _context.Store(library);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Library = library.ToDto()
                };
            }
        }
    }
}
