using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Surveys
{
    public class CreateSurvey
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Survey).NotNull();
                RuleFor(request => request.Survey).SetValidator(new SurveyValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public SurveyDto Survey { get; init; }
        }

        public class Response
        {
            public SurveyDto Survey { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var survey = new Survey(request.Survey.Name);

                _context.Store(survey);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Survey = survey.ToDto()
                };
            }
        }
    }
}
