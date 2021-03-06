using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Stories
{
    public class GetStoryById
    {
        public class Request : IRequest<Response> {  
            public Guid StoryId { get; set; }        
        }

        public class Response
        {
            public StoryDto Story { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var story = await _context.FindAsync<Story>(request.StoryId);

                return new Response() { 
                    Story = story.ToDto()
                };
            }
        }
    }
}
