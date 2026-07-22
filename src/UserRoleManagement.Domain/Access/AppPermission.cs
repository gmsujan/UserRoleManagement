using System;
using Volo.Abp.Domain.Entities;

namespace UserRoleManagement.Access;

public class AppPermission : Entity<Guid>
{
    public string Code { get; set; }    // Users.Create    
    public string DisplayName { get; set; } // Create Users
    public string Group { get; set; }    // Users or Roles

    private AppPermission() { }

    public AppPermission(Guid id, string code, string displayName, string group) : base(id)
    {
        Code = code;
        DisplayName = displayName;
        Group = group;
    }
}