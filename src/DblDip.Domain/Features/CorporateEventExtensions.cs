using DblDip.Core.Models;
using DblDip.Domain.Features.CorporateEvents;

namespace DblDip.Domain.Features
{
    public static class CorporateEventExtensions
    {
        public static CorporateEventDto ToDto(this CorporateEvent corporateEvent)
        {
            return new CorporateEventDto(corporateEvent.CorporateEventId);
        }
    }
}
