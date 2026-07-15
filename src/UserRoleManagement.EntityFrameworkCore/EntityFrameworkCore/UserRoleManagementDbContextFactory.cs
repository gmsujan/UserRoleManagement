using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace UserRoleManagement.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class UserRoleManagementDbContextFactory : IDesignTimeDbContextFactory<UserRoleManagementDbContext>
{
    public UserRoleManagementDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        UserRoleManagementEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<UserRoleManagementDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new UserRoleManagementDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../UserRoleManagement.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables();

        return builder.Build();
    }
}
