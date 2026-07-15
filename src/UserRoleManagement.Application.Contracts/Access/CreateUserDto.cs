using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserRoleManagement.Access;

public class CreateUserDto
{
    [Required]
    [StringLength(64)]
    public string UserName { get; set; } = null!;

    [Required]
    [EmailAddress]
    [StringLength(128)]
    public string Email { get; set; } = null!;

    [StringLength(128)]
    public string FullName { get; set; } = null!;

    [Required] 
    [StringLength(128, MinimumLength = 6)]
    public string Password { get; set; } = null!;

    public bool IsActive { get; set; } = true;

    public List<Guid> RoleIds { get; set; } = new();
}