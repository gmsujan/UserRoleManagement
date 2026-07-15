using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Volo.Abp.DependencyInjection;
using UserRoleManagement.Access;

namespace UserRoleManagement.Blazor.Access;

public class CurrentAccessUser : IScopedDependency
{
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly UserPermissionChecker _permissionChecker;

    public CurrentAccessUser(
        AuthenticationStateProvider authStateProvider,
        UserPermissionChecker permissionChecker)
    {
        _authStateProvider = authStateProvider;
        _permissionChecker = permissionChecker;
    }

    public async Task<Guid?> GetUserIdAsync()
    {
        var state = await _authStateProvider.GetAuthenticationStateAsync();

        var idClaim = state.User.FindFirst(ClaimTypes.NameIdentifier);

        if (idClaim == null || !Guid.TryParse(idClaim.Value, out var id))
        {
            return null;
        }

        return id;
    }

    public async Task<bool> IsGrantedAsync(string permissionCode)
    {
        var userId = await GetUserIdAsync();

        if (userId == null)
        {
            return false;   // not logged in → nothing is granted
        }

        return await _permissionChecker.IsGrantedAsync(userId.Value, permissionCode);
    }
}