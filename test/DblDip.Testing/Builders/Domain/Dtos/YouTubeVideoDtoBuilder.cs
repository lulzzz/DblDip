using DblDip.Domain.Features.YouTubeVideos;

namespace DblDip.Testing.Builders.Domain.Dtos
{
    public class YouTubeVideoDtoBuilder
    {
        private YouTubeVideoDto _youTubeVideoDto;

        public static YouTubeVideoDto WithDefaults()
        {
            return new YouTubeVideoDto();
        }

        public YouTubeVideoDtoBuilder()
        {
            _youTubeVideoDto = WithDefaults();
        }

        public YouTubeVideoDto Build()
        {
            return _youTubeVideoDto;
        }
    }
}
