using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserRoleManagement.Data;
using Volo.Abp.DependencyInjection;

namespace UserRoleManagement.EntityFrameworkCore;

public class EntityFrameworkCoreUserRoleManagementDbSchemaMigrator
    : IUserRoleManagementDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreUserRoleManagementDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the UserRoleManagementDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<UserRoleManagementDbContext>()
            .Database
            .MigrateAsync();
    }
}
