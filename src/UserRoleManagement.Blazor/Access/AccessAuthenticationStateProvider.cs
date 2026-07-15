using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;

namespace UserRoleManagement.Blazor.Access;

public class AccessAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly AuthenticationState _state;

    public AccessAuthenticationStateProvider(IHttpContextAccessor httpContextAccessor)
    {
        var principal = httpContextAccessor.HttpContext?.User
                        ?? new ClaimsPrincipal(new ClaimsIdentity());

        _state = new AuthenticationState(principal);
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return Task.FromResult(_state);
    }
}