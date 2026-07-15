using Xunit;

namespace UserRoleManagement.EntityFrameworkCore;

[CollectionDefinition(UserRoleManagementTestConsts.CollectionDefinitionName)]
public class UserRoleManagementEntityFrameworkCoreCollection : ICollectionFixture<UserRoleManagementEntityFrameworkCoreFixture>
{

}
