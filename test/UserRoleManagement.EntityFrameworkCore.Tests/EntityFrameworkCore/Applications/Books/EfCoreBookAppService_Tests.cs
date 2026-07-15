using UserRoleManagement.Books;
using Xunit;

namespace UserRoleManagement.EntityFrameworkCore.Applications.Books;

[Collection(UserRoleManagementTestConsts.CollectionDefinitionName)]
public class EfCoreBookAppService_Tests : BookAppService_Tests<UserRoleManagementEntityFrameworkCoreTestModule>
{

}