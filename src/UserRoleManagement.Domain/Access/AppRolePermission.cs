using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace UserRoleManagement.Access;
public class AppRolePermission : Entity
{
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }
    public AppRole Role { get; set; }
    public AppPermission Permission { get; set; }
    private AppRolePermission() { }
    public AppRolePermission(Guid roleId, Guid permissionId)
    {
        RoleId = roleId;
        PermissionId = permissionId;
    }

    public override object[] GetKeys() => new object[] { RoleId, PermissionId };
}