using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace UserRoleManagement.Access;
public class AppUserRole : Entity
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }

    public AppUser User { get; set; }
    public AppRole Role { get; set; }

    private AppUserRole() { }

    public AppUserRole(Guid userId, Guid roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }

    // Composite key: the pair (UserId, RoleId) uniquely identifies this row.
    public override object[] GetKeys() => new object[] { UserId, RoleId };
}
