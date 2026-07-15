using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace UserRoleManagement.Access;
public class AppRole : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; set; }        
    public string Description { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<AppRolePermission> RolePermissions { get; set; } = new List<AppRolePermission>();
    public ICollection<AppUserRole> UserRoles { get; set; } = new List<AppUserRole>();

    private AppRole() { }

    public AppRole(Guid id, string name, string description) : base(id)
    {
        Name = name;
        Description = description;
    }
}