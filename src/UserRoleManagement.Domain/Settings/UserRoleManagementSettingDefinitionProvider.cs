using Volo.Abp.Settings;

namespace UserRoleManagement.Settings;

public class UserRoleManagementSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(UserRoleManagementSettings.MySetting1));
    }
}
