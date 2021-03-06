using DblDip.Core.Models;
using DblDip.Domain.Features.Venues;

namespace DblDip.Testing.Builders.Domain.Dtos
{
    public class VenueDtoBuilder
    {
        private VenueDto _venueDto;

        public static VenueDto WithDefaults()
        {
            return new VenueDto();
        }

        public VenueDtoBuilder()
        {
            _venueDto = WithDefaults();
        }

        public VenueDto Build()
        {
            return _venueDto;
        }
    }
}
