using UserRoleManagement.Samples;
using Xunit;

namespace UserRoleManagement.EntityFrameworkCore.Applications;

[Collection(UserRoleManagementTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<UserRoleManagementEntityFrameworkCoreTestModule>
{

}
