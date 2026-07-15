using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserRoleManagement.Access;

public class CreateUpdateRoleDto
{
    [Required]
    [StringLength(64)]
    public string Name { get; set; } = null!;

    [StringLength(256)]
    public string Description { get; set; } = null!;

    public bool IsActive { get; set; } = true;

    public List<Guid> PermissionIds { get; set; } = new();
}