using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace UserRoleManagement.Access;

public interface IRoleAppService : IApplicationService
{
    Task<RoleDto> GetAsync(Guid id);
    Task<PagedResultDto<RoleDto>> GetListAsync(PagedAndSortedResultRequestDto input);
    Task<RoleDto> CreateAsync(CreateUpdateRoleDto input);
    Task<RoleDto> UpdateAsync(Guid id, CreateUpdateRoleDto input);
    Task<DeletedRoleInfoDto> CheckDeletedRoleAsync(string name);
    Task DeleteAsync(Guid id);
    Task<List<PermissionDto>> GetAllPermissionsAsync();
}