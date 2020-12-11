using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Questionnaires
{
    public class GetQuestionnaireById
    {
        public class Request : IRequest<Response> {  
            public Guid QuestionnaireId { get; set; }        
        }

        public class Response
        {
            public QuestionnaireDto Questionnaire { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var questionnaire = await _context.FindAsync<Questionnaire>(request.QuestionnaireId);

                return new Response() { 
                    Questionnaire = questionnaire.ToDto()
                };
            }
        }
    }
}
