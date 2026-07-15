using UserRoleManagement.Samples;
using Xunit;

namespace UserRoleManagement.EntityFrameworkCore.Domains;

[Collection(UserRoleManagementTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<UserRoleManagementEntityFrameworkCoreTestModule>
{

}
