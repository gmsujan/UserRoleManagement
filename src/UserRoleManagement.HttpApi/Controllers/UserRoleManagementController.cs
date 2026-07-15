using UserRoleManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace UserRoleManagement.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class UserRoleManagementController : AbpControllerBase
{
    protected UserRoleManagementController()
    {
        LocalizationResource = typeof(UserRoleManagementResource);
    }
}
