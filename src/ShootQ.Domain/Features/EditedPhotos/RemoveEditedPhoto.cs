using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace ShootQ.Domain.Features.EditedPhotos
{
    public class RemoveEditedPhoto
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit> {  
            public Guid EditedPhotoId { get; set; }
        }

        public class Response
        {
            public EditedPhotoDto EditedPhoto { get; set; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken) {

                var editedPhoto = await _context.FindAsync<EditedPhoto>(request.EditedPhotoId);

                //editedPhoto.Remove();

                _context.Store(editedPhoto);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit()
                {

                };
            }
        }
    }
}
