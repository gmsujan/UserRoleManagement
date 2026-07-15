using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace UserRoleManagement.Access;

public class RoleDto : AuditedEntityDto<Guid>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsActive { get; set; }

    public List<Guid> PermissionIds { get; set; } = new();
}