using BuildingBlocks.Abstractions;
using MediatR;
using ShootQ.Core;
using ShootQ.Core.Models;
using ShootQ.Domain.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Threading;
using static ShootQ.Core.Models.User;

namespace ShootQ.Domain.Sagas
{
    public class ProfileCreatedSaga : INotificationHandler<ProfileCreated>
    {
        private readonly IAppDbContext _context;

        public ProfileCreatedSaga(IAppDbContext context)
            => _context = context;

        public async System.Threading.Tasks.Task Handle(ProfileCreated notification, CancellationToken cancellationToken)
        {
            var profile = notification.Profile;

            var user = new User(profile.Email, "default");

            var role = profile switch
            {
                Client => await _context.FindAsync<Role>(Constants.Roles.Client),
                Photographer => await _context.FindAsync<Role>(Constants.Roles.Photographer),
                ProjectManager => await _context.FindAsync<Role>(Constants.Roles.ProjectManager),
                SystemAdministrator => await _context.FindAsync<Role>(Constants.Roles.SystemAdministrator),
                _ => throw new NotImplementedException()
            };

            user.AddRole(role.RoleId, role.Name);

            var account = new Account(new List<Guid> { profile.ProfileId }, profile.ProfileId, profile.Name, user.UserId);

            _context.Store(user);

            _context.Store(account);

        }
    }
}
