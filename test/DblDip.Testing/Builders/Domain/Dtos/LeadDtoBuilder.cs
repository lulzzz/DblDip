using DblDip.Domain.Features.Leads;

namespace DblDip.Testing.Builders.Domain.Dtos
{
    public class LeadDtoBuilder
    {
        private LeadDto _leadDto;

        public static LeadDto WithDefaults()
        {
            return new LeadDto();
        }

        public LeadDtoBuilder()
        {
            _leadDto = new LeadDto();
        }

        public LeadDto Build()
        {
            return _leadDto;
        }
    }
}
