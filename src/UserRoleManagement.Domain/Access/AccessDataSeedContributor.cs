using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace UserRoleManagement.Access;

public class AccessDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<AppPermission, Guid> _permissionRepository;
    private readonly IRepository<AppRole, Guid> _roleRepository;
    private readonly IRepository<AppUser, Guid> _userRepository;
    private readonly PasswordHasher _passwordHasher;
    private readonly IGuidGenerator _guidGenerator;

    public AccessDataSeedContributor(
        IRepository<AppPermission, Guid> permissionRepository,
        IRepository<AppRole, Guid> roleRepository,
        IRepository<AppUser, Guid> userRepository,
        PasswordHasher passwordHasher,
        IGuidGenerator guidGenerator)
    {
        _permissionRepository = permissionRepository;
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _guidGenerator = guidGenerator;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        await SeedPermissionsAsync();
        var adminRole = await SeedAdminRoleAsync();
        await SeedAdminUserAsync(adminRole);
    }

    private async Task SeedPermissionsAsync()
    {
        var definitions = new List<(string Code, string DisplayName, string Group)>
        {
            (AppPermissions.UsersView,   "View Users",   "Users"),
            (AppPermissions.UsersCreate, "Create User",  "Users"),
            (AppPermissions.UsersEdit,   "Edit User",    "Users"),
            (AppPermissions.UsersDelete, "Delete User",  "Users"),

            (AppPermissions.RolesView,   "View Roles",   "Roles"),
            (AppPermissions.RolesCreate, "Create Role",  "Roles"),
            (AppPermissions.RolesEdit,   "Edit Role",    "Roles"),
            (AppPermissions.RolesDelete, "Delete Role",  "Roles"),
        };

        var existingCodes = (await _permissionRepository.GetListAsync())
            .Select(p => p.Code)
            .ToList();

        foreach (var (code, displayName, group) in definitions)
        {
            if (existingCodes.Contains(code))
            {
                continue;
            }

            await _permissionRepository.InsertAsync(
                new AppPermission(_guidGenerator.Create(), code, displayName, group),
                autoSave: true);
        }
    }

    private async Task<AppRole> SeedAdminRoleAsync()
    {
        const string adminRoleName = "Administrator";

        var existing = await _roleRepository.FirstOrDefaultAsync(r => r.Name == adminRoleName);

        if (existing != null)
        {
            return existing;
        }

        var role = new AppRole(
            _guidGenerator.Create(),
            adminRoleName,
            "Full access to users and roles.");
        
        var allPermissions = await _permissionRepository.GetListAsync();

        foreach (var permission in allPermissions)
        {
            role.RolePermissions.Add(new AppRolePermission(role.Id, permission.Id));
        }

        await _roleRepository.InsertAsync(role, autoSave: true);

        return role;
    }

    private async Task SeedAdminUserAsync(AppRole adminRole)
    {
        const string adminUserName = "accessadmin";

        var existing = await _userRepository.FirstOrDefaultAsync(u => u.UserName == adminUserName);

        if (existing != null)
        {
            return;
        }

        var user = new AppUser(
            _guidGenerator.Create(),
            adminUserName,
            "accessadmin@buddhaair.com",
            "Access Administrator",
            _passwordHasher.HashPassword("Admin@123")   
        );

        user.UserRoles.Add(new AppUserRole(user.Id, adminRole.Id));

        await _userRepository.InsertAsync(user, autoSave: true);
    }
}