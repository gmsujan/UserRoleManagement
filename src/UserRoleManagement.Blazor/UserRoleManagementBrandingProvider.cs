using Microsoft.Extensions.Localization;
using UserRoleManagement.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace UserRoleManagement.Blazor;

[Dependency(ReplaceServices = true)]
public class UserRoleManagementBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<UserRoleManagementResource> _localizer;

    public UserRoleManagementBrandingProvider(IStringLocalizer<UserRoleManagementResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
