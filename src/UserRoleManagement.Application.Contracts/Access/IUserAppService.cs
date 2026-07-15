using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace UserRoleManagement.Access;

public interface IUserAppService : IApplicationService
{
    Task<UserDto> GetAsync(Guid id);
    Task<PagedResultDto<UserDto>> GetListAsync(PagedAndSortedResultRequestDto input);
    Task<UserDto> CreateAsync(CreateUserDto input);
    Task<UserDto> UpdateAsync(Guid id, UpdateUserDto input);
    Task DeleteAsync(Guid id);

    Task<UserDto> AssignRolesAsync(Guid id, AssignRolesDto input);
}