using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using DblDip.Core.Enums;
using System;
using System.Collections.Generic;

namespace DblDip.Core.Models
{
    public class Role : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        public Role(string name)
        {
            Apply(new RoleCreated(Guid.NewGuid(), name));
        }

        public Role(Guid roleId, string name)
        {
            Apply(new RoleCreated(roleId, name));
        }
        public void When(RoleCreated roleCreated)
        {
            RoleId = roleCreated.RoleId;
            Name = roleCreated.Name;
            Privileges = new List<Privilege>();
        }

        public void When(RoleRemoved roleRemoved)
        {
            Deleted = roleRemoved.Removed;
        }

        public void When(PrivilegesUpdated privilegesUpdated)
        {
            Privileges = privilegesUpdated.Privileges;
        }

        protected override void EnsureValidState()
        {

        }

        public void Remove(DateTime removed)
        {
            Apply(new RoleRemoved(removed));
        }

        public void UpdatePrivileges(ICollection<Privilege> privileges)
        {
            Apply(new PrivilegesUpdated(privileges));
        }

        public Guid RoleId { get; private set; }
        public string Name { get; private set; }
        public DateTime? Deleted { get; private set; }
        public ICollection<Privilege> Privileges { get; private set; }

        public record Privilege(AccessRight AccessRight, string Aggregate);

    }

}
