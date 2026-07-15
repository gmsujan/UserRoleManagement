using Volo.Abp.Modularity;

namespace UserRoleManagement;

public abstract class UserRoleManagementApplicationTestBase<TStartupModule> : UserRoleManagementTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
