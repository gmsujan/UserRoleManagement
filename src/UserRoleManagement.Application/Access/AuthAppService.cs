using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace UserRoleManagement.Access;

public class AuthAppService : ApplicationService, IAuthAppService
{
    private readonly IRepository<AppUser, Guid> _userRepository;
    private readonly PasswordHasher _passwordHasher;
    private readonly UserPermissionChecker _permissionChecker;

    public AuthAppService(
        IRepository<AppUser, Guid> userRepository,
        PasswordHasher passwordHasher,
        UserPermissionChecker permissionChecker)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _permissionChecker = permissionChecker;
    }

    public async Task<LoginResultDto> ValidateCredentialsAsync(LoginDto input)
    {
        var queryable = await _userRepository.GetQueryableAsync();

        var user = await queryable
            .FirstOrDefaultAsync(u => u.UserName == input.UserName);

        if (user == null ||
            !user.IsActive ||
            !_passwordHasher.VerifyPassword(input.Password, user.PasswordHash))
        {
            return new LoginResultDto { Success = false };
        }

        var permissions = await _permissionChecker.GetPermissionCodesAsync(user.Id);

        return new LoginResultDto
        {
            Success = true,
            UserId = user.Id,
            UserName = user.UserName,
            PermissionCodes = permissions
        };
    }
}