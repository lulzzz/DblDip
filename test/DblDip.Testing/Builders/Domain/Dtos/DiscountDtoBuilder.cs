using DblDip.Core.Models;
using DblDip.Domain.Features.Discounts;

namespace DblDip.Testing.Builders.Domain.Dtos
{
    public class DiscountDtoBuilder
    {
        private DiscountDto _discountDto;

        public static DiscountDto WithDefaults()
        {
            return new DiscountDto();
        }

        public DiscountDtoBuilder()
        {
            _discountDto = WithDefaults();
        }

        public DiscountDto Build()
        {
            return _discountDto;
        }
    }
}
