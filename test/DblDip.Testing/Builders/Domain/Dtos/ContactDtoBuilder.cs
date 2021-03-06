using DblDip.Domain.Features.Contacts;

namespace DblDip.Testing.Builders.Domain.Dtos
{
    public class ContactDtoBuilder
    {
        private ContactDto _contactDto;

        public static ContactDto WithDefaults()
        {
            return new ContactDto();
        }

        public ContactDtoBuilder()
        {
            _contactDto = new ContactDto();
        }

        public ContactDto Build()
        {
            return _contactDto;
        }
    }
}
