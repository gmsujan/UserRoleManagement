using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace UserRoleManagement.Access;

public class UserDto : AuditedEntityDto<Guid>
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public bool IsActive { get; set; }

    public List<Guid> RoleIds { get; set; } = new();
    public List<string> RoleNames { get; set; } = new();
}