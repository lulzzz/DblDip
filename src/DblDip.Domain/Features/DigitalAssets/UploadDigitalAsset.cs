using BuildingBlocks.Abstractions;
using BuildingBlocks.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using DblDip.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.DigitalAssets
{
    public class UploadDigitalAsset
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public List<Guid> DigitalAssetIds { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; init; }
            public IHttpContextAccessor _httpContextAccessor { get; init; }
            public Handler(IAppDbContext context, IHttpContextAccessor httpContextAccessor)
            {
                _context = context;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var httpContext = _httpContextAccessor.HttpContext;
                var defaultFormOptions = new FormOptions();
                var digitalAssets = new List<DigitalAsset>();

                if (!MultipartRequestHelper.IsMultipartContentType(httpContext.Request.ContentType))
                    throw new Exception($"Expected a multipart request, but got {httpContext.Request.ContentType}");

                var mediaTypeHeaderValue = MediaTypeHeaderValue.Parse(httpContext.Request.ContentType);

                var boundary = MultipartRequestHelper.GetBoundary(
                    mediaTypeHeaderValue,
                    defaultFormOptions.MultipartBoundaryLengthLimit);

                var reader = new MultipartReader(boundary, httpContext.Request.Body);

                var section = await reader.ReadNextSectionAsync();

                while (section != null)
                {
                    var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out ContentDispositionHeaderValue contentDisposition);

                    if (hasContentDispositionHeader)
                    {
                        if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                        {
                            using (var targetStream = new MemoryStream())
                            {
                                await section.Body.CopyToAsync(targetStream, cancellationToken);
                                var name = $"{contentDisposition.FileName}".Trim(new char[] { '"' }).Replace("&", "and");
                                var bytes = StreamHelper.ReadToEnd(targetStream);
                                var contentType = section.ContentType;

                                var digitalAsset = new DigitalAsset(name, bytes, contentType);

                                _context.Store(digitalAsset);

                                digitalAssets.Add(digitalAsset);
                            }
                        }
                    }

                    section = await reader.ReadNextSectionAsync(cancellationToken);
                }

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    DigitalAssetIds = digitalAssets.Select(x => x.DigitalAssetId).ToList()
                };
            }
        }
    }
}
