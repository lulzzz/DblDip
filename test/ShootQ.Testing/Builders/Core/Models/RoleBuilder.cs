using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class RoleBuilder
    {
        private Role _role;

        public static Role WithDefaults()
        {
            return new Role("Test");
        }

        public RoleBuilder()
        {
            _role = WithDefaults();
        }

        public Role Build()
        {
            return _role;
        }
    }
}
