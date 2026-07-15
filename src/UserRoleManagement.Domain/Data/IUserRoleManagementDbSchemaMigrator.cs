using System.Threading.Tasks;

namespace UserRoleManagement.Data;

public interface IUserRoleManagementDbSchemaMigrator
{
    Task MigrateAsync();
}
