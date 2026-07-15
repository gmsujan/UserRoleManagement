using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace UserRoleManagement.Access;

public interface IAuthAppService : IApplicationService
{
    Task<LoginResultDto> ValidateCredentialsAsync(LoginDto input);
}