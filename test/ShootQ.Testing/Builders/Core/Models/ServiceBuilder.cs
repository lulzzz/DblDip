using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class ServiceBuilder
    {
        private Service _service;

        public static Service WithDefaults()
        {
            return new Service(default, default, default);
        }

        public ServiceBuilder()
        {
            _service = WithDefaults();
        }

        public Service Build()
        {
            return _service;
        }
    }
}
