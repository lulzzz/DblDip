using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Posts
{
    public class CreatePost
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Post).NotNull();
                RuleFor(request => request.Post).SetValidator(new PostValidator());
            }
        }

        public record Request(PostDto Post) : IRequest<Response>;

        public class Response
        {
            public PostDto Post { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var post = new Post(request.Post.AuthorId, request.Post.Title);

                _context.Store(post);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Post = post.ToDto()
                };
            }
        }
    }
}
