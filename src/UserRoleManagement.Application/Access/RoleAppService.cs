using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace UserRoleManagement.Access;

public class RoleAppService : ApplicationService, IRoleAppService
{
    private readonly IRepository<AppRole, Guid> _roleRepository;
    private readonly IRepository<AppPermission, Guid> _permissionRepository;
    private readonly IDataFilter _dataFilter;
    private readonly UserPermissionChecker _permissionChecker;
    private readonly CurrentAccessUserAccessor _currentUser;

    public RoleAppService(
        IRepository<AppRole, Guid> roleRepository,
        IRepository<AppPermission, Guid> permissionRepository,
        IDataFilter dataFilter,
        UserPermissionChecker permissionChecker,
        CurrentAccessUserAccessor currentUser)
    {
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
        _dataFilter = dataFilter;
        _permissionChecker = permissionChecker;
        _currentUser = currentUser;
    }

    // ---------- authorization gate ----------

    private async Task CheckPermissionAsync(string permissionCode)
    {
        var userId = await _currentUser.GetUserIdAsync();   // ← await + Async

        if (userId == null ||
            !await _permissionChecker.IsGrantedAsync(userId.Value, permissionCode))
        {
            throw new AbpAuthorizationException("You are not authorized to perform this action.");
        }
    }

    // ---------- CRUD ----------

    public async Task<RoleDto> GetAsync(Guid id)
    {
        await CheckPermissionAsync(AppPermissions.RolesView);

        var queryable = await _roleRepository.GetQueryableAsync();

        var role = await queryable
            .Include(r => r.RolePermissions)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (role == null)
        {
            throw new EntityNotFoundException(typeof(AppRole), id);
        }

        return MapToDto(role);
    }

    public async Task<PagedResultDto<RoleDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        await CheckPermissionAsync(AppPermissions.RolesView);

        var queryable = await _roleRepository.GetQueryableAsync();

        var totalCount = await queryable.CountAsync();

        var roles = await queryable
            .Include(r => r.RolePermissions)
            .OrderBy(r => r.Name)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount)
            .ToListAsync();

        return new PagedResultDto<RoleDto>(
            totalCount,
            roles.Select(MapToDto).ToList()
        );
    }

    public async Task<RoleDto> CreateAsync(CreateUpdateRoleDto input)
    {
        await CheckPermissionAsync(AppPermissions.RolesCreate);

        await CheckNameNotTakenAsync(input.Name, excludeId: null);

        var role = new AppRole(GuidGenerator.Create(), input.Name, input.Description)
        {
            IsActive = input.IsActive
        };

        foreach (var permissionId in input.PermissionIds.Distinct())
        {
            role.RolePermissions.Add(new AppRolePermission(role.Id, permissionId));
        }

        await _roleRepository.InsertAsync(role, autoSave: true);

        return MapToDto(role);
    }

    public async Task<RoleDto> UpdateAsync(Guid id, CreateUpdateRoleDto input)
    {
        await CheckPermissionAsync(AppPermissions.RolesEdit);

        var queryable = await _roleRepository.GetQueryableAsync();

        var role = await queryable
            .Include(r => r.RolePermissions)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (role == null)
        {
            throw new EntityNotFoundException(typeof(AppRole), id);
        }

        await CheckNameNotTakenAsync(input.Name, excludeId: id);

        role.Name = input.Name;
        role.Description = input.Description;
        role.IsActive = input.IsActive;

        var desiredIds = input.PermissionIds.Distinct().ToList();
        var currentIds = role.RolePermissions.Select(rp => rp.PermissionId).ToList();

        var toRemove = role.RolePermissions
            .Where(rp => !desiredIds.Contains(rp.PermissionId))
            .ToList();

        foreach (var rp in toRemove)
        {
            role.RolePermissions.Remove(rp);
        }

        foreach (var permissionId in desiredIds.Where(pid => !currentIds.Contains(pid)))
        {
            role.RolePermissions.Add(new AppRolePermission(role.Id, permissionId));
        }

        await _roleRepository.UpdateAsync(role, autoSave: true);

        return MapToDto(role);
    }

    public async Task DeleteAsync(Guid id)
    {
        await CheckPermissionAsync(AppPermissions.RolesDelete);

        await _roleRepository.DeleteAsync(id);
    }

    // ---------- supporting queries ----------

    public async Task<DeletedRoleInfoDto> CheckDeletedRoleAsync(string name)
    {
        await CheckPermissionAsync(AppPermissions.RolesCreate);

        using (_dataFilter.Disable<ISoftDelete>())
        {
            var queryable = await _roleRepository.GetQueryableAsync();

            var deleted = await queryable
                .Where(r => r.Name == name && r.IsDeleted)
                .OrderByDescending(r => r.DeletionTime)
                .FirstOrDefaultAsync();

            if (deleted == null)
            {
                return new DeletedRoleInfoDto { Exists = false };
            }

            return new DeletedRoleInfoDto
            {
                Exists = true,
                DeletionTime = deleted.DeletionTime,
                Description = deleted.Description
            };
        }
    }

    public async Task<List<PermissionDto>> GetAllPermissionsAsync()
    {
        await CheckPermissionAsync(AppPermissions.RolesView);

        var permissions = await _permissionRepository.GetListAsync();

        return permissions
            .OrderBy(p => p.Group)
            .ThenBy(p => p.DisplayName)
            .Select(p => new PermissionDto
            {
                Id = p.Id,
                Code = p.Code,
                DisplayName = p.DisplayName,
                Group = p.Group
            })
            .ToList();
    }

    // ---------- helpers ----------

    private async Task CheckNameNotTakenAsync(string name, Guid? excludeId)
    {
        var queryable = await _roleRepository.GetQueryableAsync();

        var exists = await queryable.AnyAsync(r =>
            r.Name == name && (excludeId == null || r.Id != excludeId));

        if (exists)
        {
            throw new UserFriendlyException($"A role named '{name}' already exists.");
        }
    }

    private static RoleDto MapToDto(AppRole role)
    {
        return new RoleDto
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description,
            IsActive = role.IsActive,
            CreationTime = role.CreationTime,
            LastModificationTime = role.LastModificationTime,
            PermissionIds = role.RolePermissions.Select(rp => rp.PermissionId).ToList()
        };
    }
}