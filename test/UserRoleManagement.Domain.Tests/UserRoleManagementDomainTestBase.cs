using Volo.Abp.Modularity;

namespace UserRoleManagement;

/* Inherit from this class for your domain layer tests. */
public abstract class UserRoleManagementDomainTestBase<TStartupModule> : UserRoleManagementTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
