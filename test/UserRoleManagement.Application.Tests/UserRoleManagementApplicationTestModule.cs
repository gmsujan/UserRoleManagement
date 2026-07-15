using Volo.Abp.Modularity;

namespace UserRoleManagement;

[DependsOn(
    typeof(UserRoleManagementApplicationModule),
    typeof(UserRoleManagementDomainTestModule)
)]
public class UserRoleManagementApplicationTestModule : AbpModule
{

}
