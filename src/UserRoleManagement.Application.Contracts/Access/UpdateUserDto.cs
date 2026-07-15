using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserRoleManagement.Access;

public class UpdateUserDto
{
    [Required]
    [EmailAddress]
    [StringLength(128)]
    public string Email { get; set; } = null!;

    [StringLength(128)]
    public string FullName { get; set; } = null!;

    public bool IsActive { get; set; }

    public List<Guid> RoleIds { get; set; } = new();
}