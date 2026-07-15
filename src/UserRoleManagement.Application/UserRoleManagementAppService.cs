using UserRoleManagement.Localization;
using Volo.Abp.Application.Services;

namespace UserRoleManagement;

/* Inherit your application services from this class.
 */
public abstract class UserRoleManagementAppService : ApplicationService
{
    protected UserRoleManagementAppService()
    {
        LocalizationResource = typeof(UserRoleManagementResource);
    }
}
