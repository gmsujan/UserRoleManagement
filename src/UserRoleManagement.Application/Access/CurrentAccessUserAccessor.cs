using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Volo.Abp.DependencyInjection;

namespace UserRoleManagement.Access;

public class CurrentAccessUserAccessor : IScopedDependency
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentAccessUserAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<Guid?> GetUserIdAsync()
    {
        var principal = _httpContextAccessor.HttpContext?.User;

        var idClaim = principal?.FindFirst(ClaimTypes.NameIdentifier);

        if (idClaim == null || !Guid.TryParse(idClaim.Value, out var id))
        {
            return Task.FromResult<Guid?>(null);
        }

        return Task.FromResult<Guid?>(id);
    }
}