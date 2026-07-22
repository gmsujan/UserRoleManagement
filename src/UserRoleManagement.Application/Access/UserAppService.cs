using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace UserRoleManagement.Access;

public class UserAppService : ApplicationService, IUserAppService
{
    private readonly IRepository<AppUser, Guid> _userRepository;
    private readonly IRepository<AppRole, Guid> _roleRepository;
    private readonly PasswordHasher _passwordHasher;
    private readonly UserPermissionChecker _permissionChecker;
    private readonly CurrentAccessUserAccessor _currentUser;

    public UserAppService(
        IRepository<AppUser, Guid> userRepository,
        IRepository<AppRole, Guid> roleRepository,
        PasswordHasher passwordHasher,
        UserPermissionChecker permissionChecker,
        CurrentAccessUserAccessor currentUser)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _passwordHasher = passwordHasher;
        _permissionChecker = permissionChecker;
        _currentUser = currentUser;
    }

    private async Task CheckPermissionAsync(string permissionCode)
    {
        var userId = await _currentUser.GetUserIdAsync();   // ← await + Async

        if (userId == null ||
            !await _permissionChecker.IsGrantedAsync(userId.Value, permissionCode))
        {
            throw new AbpAuthorizationException("You are not authorized to perform this action.");
        }
    }

    public async Task<UserDto> GetAsync(Guid id)
    {
        await CheckPermissionAsync(AppPermissions.UsersView);

        var user = await GetUserWithRolesAsync(id);
        return MapToDto(user);
    }

    public async Task<PagedResultDto<UserDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        await CheckPermissionAsync(AppPermissions.UsersView);

        var queryable = await _userRepository.GetQueryableAsync();

        var totalCount = await queryable.CountAsync();

        var users = await queryable
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .OrderBy(u => u.UserName)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount)
            .ToListAsync();

        return new PagedResultDto<UserDto>(totalCount, users.Select(MapToDto).ToList());
    }

    public async Task<UserDto> CreateAsync(CreateUserDto input)
    {
        await CheckPermissionAsync(AppPermissions.UsersCreate);

        await CheckUserNameNotTakenAsync(input.UserName, excludeId: null);
        await CheckRolesExistAsync(input.RoleIds);

        var user = new AppUser(
            GuidGenerator.Create(),
            input.UserName,
            input.Email,
            input.FullName,
            _passwordHasher.HashPassword(input.Password)
        )
        {
            IsActive = input.IsActive
        };

        foreach (var roleId in input.RoleIds.Distinct())
        {
            user.UserRoles.Add(new AppUserRole(user.Id, roleId));
        }

        await _userRepository.InsertAsync(user, autoSave: true);

        return MapToDto(await GetUserWithRolesAsync(user.Id));
    }

    public async Task<UserDto> UpdateAsync(Guid id, UpdateUserDto input)
    {
        await CheckPermissionAsync(AppPermissions.UsersEdit);

        var user = await GetUserWithRolesAsync(id);

        await CheckRolesExistAsync(input.RoleIds);

        // UserName is deliberately not updatable.
        user.Email = input.Email;
        user.FullName = input.FullName;
        user.IsActive = input.IsActive;

        ReplaceRoles(user, input.RoleIds);

        await _userRepository.UpdateAsync(user, autoSave: true);

        return MapToDto(await GetUserWithRolesAsync(id));
    }

    public async Task DeleteAsync(Guid id)
    {
        await CheckPermissionAsync(AppPermissions.UsersDelete);

        var currentUserId = await _currentUser.GetUserIdAsync();   // ← await + Async

        if (currentUserId == id)
        {
            throw new UserFriendlyException("You cannot delete your own account.");
        }

        await _userRepository.DeleteAsync(id);
    }

    public async Task<UserDto> AssignRolesAsync(Guid id, AssignRolesDto input)
    {
        await CheckPermissionAsync(AppPermissions.UsersEdit);

        var user = await GetUserWithRolesAsync(id);

        await CheckRolesExistAsync(input.RoleIds);

        ReplaceRoles(user, input.RoleIds);

        await _userRepository.UpdateAsync(user, autoSave: true);

        return MapToDto(await GetUserWithRolesAsync(id));
    }

    private async Task<AppUser> GetUserWithRolesAsync(Guid id)
    {
        var queryable = await _userRepository.GetQueryableAsync();

        var user = await queryable
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            throw new EntityNotFoundException(typeof(AppUser), id);
        }

        return user;
    }

    private static void ReplaceRoles(AppUser user, List<Guid> roleIds)
    {
        user.UserRoles.Clear();

        foreach (var roleId in roleIds.Distinct())
        {
            user.UserRoles.Add(new AppUserRole(user.Id, roleId));
        }
    }

    private async Task CheckUserNameNotTakenAsync(string userName, Guid? excludeId)
    {
        var queryable = await _userRepository.GetQueryableAsync();

        var exists = await queryable.AnyAsync(u =>
            u.UserName == userName && (excludeId == null || u.Id != excludeId));

        if (exists)
        {
            throw new UserFriendlyException($"The username '{userName}' is already taken.");
        }
    }

    private async Task CheckRolesExistAsync(List<Guid> roleIds)
    {
        if (!roleIds.Any())
        {
            return;
        }

        var queryable = await _roleRepository.GetQueryableAsync();

        var foundCount = await queryable.CountAsync(r => roleIds.Contains(r.Id));

        if (foundCount != roleIds.Distinct().Count())
        {
            throw new UserFriendlyException("One or more of the specified roles do not exist.");
        }
    }

    private static UserDto MapToDto(AppUser user)
    {
        return new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            FullName = user.FullName,
            IsActive = user.IsActive,
            CreationTime = user.CreationTime,
            LastModificationTime = user.LastModificationTime,
            RoleIds = user.UserRoles.Select(ur => ur.RoleId).ToList(),
            RoleNames = user.UserRoles
                .Where(ur => ur.Role != null)
                .Select(ur => ur.Role.Name)
                .ToList()
        };
    }
}