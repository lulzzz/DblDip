using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class BrandBuilder
    {
        private Brand _brand;

        public static Brand WithDefaults()
        {
            return new Brand();
        }

        public BrandBuilder()
        {
            _brand = WithDefaults();
        }

        public Brand Build()
        {
            return _brand;
        }
    }
}
