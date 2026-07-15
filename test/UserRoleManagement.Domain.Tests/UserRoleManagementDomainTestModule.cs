using Volo.Abp.Modularity;

namespace UserRoleManagement;

[DependsOn(
    typeof(UserRoleManagementDomainModule),
    typeof(UserRoleManagementTestBaseModule)
)]
public class UserRoleManagementDomainTestModule : AbpModule
{

}
