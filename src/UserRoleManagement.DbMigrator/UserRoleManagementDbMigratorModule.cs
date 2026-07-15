using UserRoleManagement.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace UserRoleManagement.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(UserRoleManagementEntityFrameworkCoreModule),
    typeof(UserRoleManagementApplicationContractsModule)
)]
public class UserRoleManagementDbMigratorModule : AbpModule
{
}
