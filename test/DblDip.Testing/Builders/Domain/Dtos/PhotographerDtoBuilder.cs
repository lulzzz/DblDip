using DblDip.Domain.Features.Photographers;

namespace DblDip.Testing.Builders.Domain.Dtos
{
    public class PhotographerDtoBuilder
    {
        private PhotographerDto _photographerDto;

        public static PhotographerDto WithDefaults()
        {
            return new PhotographerDto();
        }

        public PhotographerDtoBuilder()
        {
            _photographerDto = new PhotographerDto();
        }

        public PhotographerDto Build()
        {
            return _photographerDto;
        }
    }
}
