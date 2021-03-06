using DblDip.Core.Models;
using DblDip.Domain.Features.Boards;

namespace DblDip.Testing.Builders.Domain.Dtos
{
    public class BoardDtoBuilder
    {
        private BoardDto _boardDto;

        public static BoardDto WithDefaults()
        {
            return new BoardDto();
        }

        public BoardDtoBuilder()
        {
            _boardDto = WithDefaults();
        }

        public BoardDto Build()
        {
            return _boardDto;
        }
    }
}
