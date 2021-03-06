using DblDip.Core.Models;

namespace DblDip.Testing.Builders.Core.Models
{
    public class EquipmentBuilder
    {
        private Equipment _equipment;

        public static Equipment WithDefaults()
        {
            return new Equipment(default, default, default);
        }

        public EquipmentBuilder()
        {
            _equipment = WithDefaults();
        }

        public Equipment Build()
        {
            return _equipment;
        }
    }
}
