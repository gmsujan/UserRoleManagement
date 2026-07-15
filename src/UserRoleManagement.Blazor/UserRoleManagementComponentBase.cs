using UserRoleManagement.Localization;
using Volo.Abp.AspNetCore.Components;

namespace UserRoleManagement.Blazor;

public abstract class UserRoleManagementComponentBase : AbpComponentBase
{
    protected UserRoleManagementComponentBase()
    {
        LocalizationResource = typeof(UserRoleManagementResource);
    }
}
