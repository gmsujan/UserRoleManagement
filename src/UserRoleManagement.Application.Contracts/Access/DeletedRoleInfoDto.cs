using System;

namespace UserRoleManagement.Access;

public class DeletedRoleInfoDto
{
    public bool Exists { get; set; }
    public DateTime? DeletionTime { get; set; }
    public string Description { get; set; }
}