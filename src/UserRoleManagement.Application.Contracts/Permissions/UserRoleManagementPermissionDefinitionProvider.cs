using UserRoleManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace UserRoleManagement.Permissions;

public class UserRoleManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(UserRoleManagementPermissions.GroupName);

        var booksPermission = myGroup.AddPermission(UserRoleManagementPermissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(UserRoleManagementPermissions.Books.Create, L("Permission:Books.Create"));
        booksPermission.AddChild(UserRoleManagementPermissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(UserRoleManagementPermissions.Books.Delete, L("Permission:Books.Delete"));

        var authorsPermission = myGroup.AddPermission(UserRoleManagementPermissions.Authors.Default, L("Permission:Authors"));
        authorsPermission.AddChild(UserRoleManagementPermissions.Authors.Create, L("Permission:Authors.Create"));
        authorsPermission.AddChild(UserRoleManagementPermissions.Authors.Edit, L("Permission:Authors.Edit"));
        authorsPermission.AddChild(UserRoleManagementPermissions.Authors.Delete, L("Permission:Authors.Delete"));
        //Define your own permissions here. Example:
        //myGroup.AddPermission(UserRoleManagementPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<UserRoleManagementResource>(name);
    }
}
