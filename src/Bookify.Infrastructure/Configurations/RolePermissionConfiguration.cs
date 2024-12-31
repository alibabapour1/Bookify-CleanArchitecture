using Bookify.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookify.Infrastructure.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolesPermissions>
{
    public void Configure(EntityTypeBuilder<RolesPermissions> builder)
    {
        builder.ToTable("roles_permissions");
        builder.HasKey(x => new { x.RoleId, x.PermissionId });
        builder.HasData(new RolesPermissions
        {
            PermissionId = Permissions.UserRead.Id,
            RoleId =Role.Registered.Id
        });
    }
}