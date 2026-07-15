using System;
using System.Collections.Generic;

namespace UserRoleManagement.Access;

public class LoginResultDto
{
    public bool Success { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = null!;
    public List<string> PermissionCodes { get; set; } = new();
}