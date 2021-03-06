using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using System;

namespace DblDip.Core.Models
{
    public class Engagement : Service
    {
        protected override void When(dynamic @event) => When(@event);

        public Engagement()
        {

        }

        public void When(EngagementUpdated engagementUpdated)
        {

        }

        protected override void EnsureValidState()
        {

        }

        public void Update()
        {
            Apply(new EngagementUpdated());
        }

        public Guid EngagementId { get; private set; }
    }
}
