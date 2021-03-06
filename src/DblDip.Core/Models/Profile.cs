using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public class Profile : AggregateRoot
    {
        protected override void When(dynamic @event)
        {
            if (@event is AvatarChanged || @event is ProfileCreated || @event is ProfileRemoved)
            {
                When(@event);
            }
        }

        public Profile(string name, Email email, System.Type type)
        {
            Apply(new ProfileCreated(Guid.NewGuid(),name,email, type.Name, type.AssemblyQualifiedName));
        }

        public void ChangeAvatar(Guid avatarDigitalAssetId)
        {
            Apply(new AvatarChanged(avatarDigitalAssetId));
        }

        public void When(ProfileCreated profileCreated)
        {
            ProfileId = profileCreated.ProfileId;
            Name = profileCreated.Name;
            Email = profileCreated.Email;
            Type = profileCreated.Type;
            DotNetType = profileCreated.DotNetType;
        }

        public void When(AvatarChanged avatarChanged)
        {
            AvatarDigitalAssetId = avatarChanged.AvatarDigitalAssetId;
        }

        public void When(ProfileRemoved profileRemoved)
        {
            Deleted = profileRemoved.Deleted;
        }

        protected override void EnsureValidState()
        {

        }

        public void Remove(DateTime deleted)
        {
            Apply(new ProfileRemoved(deleted));
        }

        public Guid ProfileId { get; private set; }
        public Guid? DefaultDashboardId { get; private set; }
        public Guid AccountId { get; private set; }
        public Email Email { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public Guid AvatarDigitalAssetId { get; private set; }
        public string Name { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public string Type { get; private set; }
        public string DotNetType { get; private set; }
        public DateTime? Deleted { get; protected set; }
    }
}
