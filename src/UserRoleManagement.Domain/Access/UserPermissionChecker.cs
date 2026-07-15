using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace UserRoleManagement.Access;

public class UserPermissionChecker : ITransientDependency
{
    private readonly IRepository<AppUser, Guid> _userRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public UserPermissionChecker(
        IRepository<AppUser, Guid> userRepository,
        IUnitOfWorkManager unitOfWorkManager)
    {
        _userRepository = userRepository;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task<List<string>> GetPermissionCodesAsync(Guid userId)
    {
        // Blazor circuits outlive the HTTP request, so the request's
        // DbContext may already be disposed. Open our own.
        using var uow = _unitOfWorkManager.Begin(requiresNew: true, isTransactional: false);

        var queryable = await _userRepository.GetQueryableAsync();

        var user = await queryable
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                    .ThenInclude(r => r.RolePermissions)
                        .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null || !user.IsActive)
        {
            await uow.CompleteAsync();
            return new List<string>();
        }

        var codes = user.UserRoles
            .Where(ur => ur.Role != null && ur.Role.IsActive)
            .SelectMany(ur => ur.Role.RolePermissions)
            .Where(rp => rp.Permission != null)
            .Select(rp => rp.Permission.Code)
            .Distinct()
            .ToList();

        await uow.CompleteAsync();

        return codes;
    }

    public async Task<bool> IsGrantedAsync(Guid userId, string permissionCode)
    {
        var codes = await GetPermissionCodesAsync(userId);
        return codes.Contains(permissionCode);
    }
}