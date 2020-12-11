using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class ReferralBuilder
    {
        private Referral _referral;

        public static Referral WithDefaults()
        {
            return new Referral();
        }

        public ReferralBuilder()
        {
            _referral = WithDefaults();
        }

        public Referral Build()
        {
            return _referral;
        }
    }
}
