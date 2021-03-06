using DblDip.Domain.Features.Accounts;

namespace DblDip.Testing.Builders.Domain.Dtos
{
    public class AccountDtoBuilder
    {
        private AccountDto _accountDto;

        public static AccountDto WithDefaults()
        {
            return new AccountDto(default, default, default, default, default);
        }

        public AccountDtoBuilder()
        {
            _accountDto = WithDefaults();
        }

        public AccountDto Build()
        {
            return _accountDto;
        }
    }
}
