using System;

namespace DblDip.Core.DomainEvents
{
    public record UserCreated(Guid UserId, string Username, string Password, byte[] Salt, bool PasswordResetRequired);
    public record UserPasswordChanged(string Password);
    public record RoleReferenceAdded(Guid RoleId, string Name);
    public record RoleReferenceRemoved(Guid RoleId, string Name);
    public record UserRefreshTokenAdded(string RefreshToken);
}
