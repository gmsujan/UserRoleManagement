using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace UserRoleManagement.Access;

public class AppUser : FullAuditedAggregateRoot<Guid>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public string PasswordHash { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<AppUserRole> UserRoles { get; set; } = new List<AppUserRole>();
    private AppUser() { } 
    public AppUser(Guid id, string userName, string email, string fullName, string passwordHash)
        : base(id)
    {
        UserName = userName;
        Email = email;
        FullName = fullName;
        PasswordHash = passwordHash;
    }
}