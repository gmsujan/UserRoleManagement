using System;
using System.Collections.Generic;

namespace UserRoleManagement.Access;

public class AssignRolesDto
{
    public List<Guid> RoleIds { get; set; } = new();
}