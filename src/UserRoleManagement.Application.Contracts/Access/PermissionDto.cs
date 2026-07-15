using System;
using Volo.Abp.Application.Dtos;

namespace UserRoleManagement.Access;

public class PermissionDto : EntityDto<Guid>
{
    public string Code { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public string Group { get; set; } = null!;
}