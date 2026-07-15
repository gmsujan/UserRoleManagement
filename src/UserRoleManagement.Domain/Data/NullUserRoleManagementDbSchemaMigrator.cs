using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace UserRoleManagement.Data;

/* This is used if database provider does't define
 * IUserRoleManagementDbSchemaMigrator implementation.
 */
public class NullUserRoleManagementDbSchemaMigrator : IUserRoleManagementDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
