using System.ComponentModel.DataAnnotations;

namespace UserRoleManagement.Access;

public class LoginDto
{
    [Required]
    public string UserName { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}