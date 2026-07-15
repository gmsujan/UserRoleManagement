using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using UserRoleManagement.Access;
using UserRoleManagement.Blazor.Access;
using UserRoleManagement.Localization;
using UserRoleManagement.MultiTenancy;
using UserRoleManagement.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Blazor;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.UI.Navigation;

namespace UserRoleManagement.Blazor.Menus;

public class UserRoleManagementMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<UserRoleManagementResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                UserRoleManagementMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fas fa-home",
                order: 1
            )
        );

        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 6;
        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);

        context.Menu.AddItem(
            new ApplicationMenuItem(
                "BooksStore",
                l["Menu:UserRoleManagement"],
                icon: "fa fa-book"
            ).AddItem(
                new ApplicationMenuItem(
                    "BooksStore.Books",
                    l["Menu:Books"],
                    url: "/books"
                ).RequirePermissions(UserRoleManagementPermissions.Books.Default)
            ).AddItem(
                new ApplicationMenuItem(
                    "BooksStore.Authors",
                    l["Menu:Authors"],
                    url: "/authors"
                ).RequirePermissions(UserRoleManagementPermissions.Authors.Default)
            )
        );

        // --- Our custom access-controlled menu items ---

        var currentUser = context.ServiceProvider.GetRequiredService<CurrentAccessUser>();

        if (await currentUser.IsGrantedAsync(AppPermissions.RolesView))
        {
            context.Menu.AddItem(
                new ApplicationMenuItem("Access.Roles", "Roles", "/roles", icon: "fa fa-shield")
            );
        }

        if (await currentUser.IsGrantedAsync(AppPermissions.UsersView))
        {
            context.Menu.AddItem(
                new ApplicationMenuItem("Access.Users", "Users", "/users", icon: "fa fa-users")
            );
        }
    }
}