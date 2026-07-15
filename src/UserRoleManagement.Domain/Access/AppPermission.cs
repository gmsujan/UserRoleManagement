using System;
using Volo.Abp.Domain.Entities;

namespace UserRoleManagement.Access;

public class AppPermission : Entity<Guid>
{
    public string Code { get; set; }        // "WorkOrders.Delete" — what code checks
    public string DisplayName { get; set; } // "Delete Work Order" — what humans read
    public string Group { get; set; }       // "Work Orders" — for grouping on the UI

    private AppPermission() { }

    public AppPermission(Guid id, string code, string displayName, string group) : base(id)
    {
        Code = code;
        DisplayName = displayName;
        Group = group;
    }
}