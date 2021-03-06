using DblDip.Domain.Features.ShotLists;

namespace DblDip.Testing.Builders.Domain.Dtos
{
    public class ShotListDtoBuilder
    {
        private ShotListDto _shotListDto;

        public static ShotListDto WithDefaults()
        {
            return new ShotListDto();
        }

        public ShotListDtoBuilder()
        {
            _shotListDto = WithDefaults();
        }

        public ShotListDto Build()
        {
            return _shotListDto;
        }
    }
}
