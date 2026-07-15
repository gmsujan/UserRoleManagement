using Blazorise;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRoleManagement.Access;
using UserRoleManagement.Blazor.Access;
using Volo.Abp.Application.Dtos;

namespace UserRoleManagement.Blazor.Components.Pages;

public partial class Roles
{
    private IReadOnlyList<RoleDto> RoleList { get; set; } = new List<RoleDto>();
    private List<PermissionDto> AllPermissions { get; set; } = new();

    private Modal _warningModal;
    private DeletedRoleInfoDto _deletedInfo;
    private int TotalCount { get; set; }
    private const int PageSize = 10;

    private Modal _modal;
    private Guid? _editingId;
    private CreateUpdateRoleDto _editing = new();

    [Inject] private CurrentAccessUser CurrentUser { get; set; }
    [Inject] private NavigationManager Navigation { get; set; }

    private bool _canCreate;
    private bool _canEdit;
    private bool _canDelete;
    protected override async Task OnInitializedAsync()
    {
        if (!await CurrentUser.IsGrantedAsync(AppPermissions.RolesView))
        {
            Navigation.NavigateTo("/access-login");
            return;
        }

        _canCreate = await CurrentUser.IsGrantedAsync(AppPermissions.RolesCreate);
        _canEdit = await CurrentUser.IsGrantedAsync(AppPermissions.RolesEdit);
        _canDelete = await CurrentUser.IsGrantedAsync(AppPermissions.RolesDelete);

        AllPermissions = await RoleAppService.GetAllPermissionsAsync();
        await LoadRolesAsync();
    }

    private async Task LoadRolesAsync()
    {
        var result = await RoleAppService.GetListAsync(
            new PagedAndSortedResultRequestDto
            {
                MaxResultCount = PageSize,
                SkipCount = 0
            });

        RoleList = result.Items;
        TotalCount = (int)result.TotalCount;
    }

    private async Task OpenCreateModalAsync()
    {
        _editingId = null;
        _editing = new CreateUpdateRoleDto { IsActive = true };
        await _modal.Show();
    }

    private async Task OpenEditModalAsync(RoleDto role)
    {
        _editingId = role.Id;
        _editing = new CreateUpdateRoleDto
        {
            Name = role.Name,
            Description = role.Description,
            IsActive = role.IsActive,
            PermissionIds = role.PermissionIds.ToList()   // copy, not reference
        };
        await _modal.Show();
    }

    private async Task CloseModalAsync()
    {
        await _modal.Hide();
    }

    private void TogglePermission(Guid permissionId, bool isChecked)
    {
        if (isChecked)
        {
            if (!_editing.PermissionIds.Contains(permissionId))
            {
                _editing.PermissionIds.Add(permissionId);
            }
        }
        else
        {
            _editing.PermissionIds.Remove(permissionId);
        }
    }

    private async Task SaveAsync()
    {
        try
        {
            // Only warn on create, and only if the name was used before.
            if (_editingId == null)
            {
                _deletedInfo = await RoleAppService.CheckDeletedRoleAsync(_editing.Name);

                if (_deletedInfo.Exists)
                {
                    await _warningModal.Show();
                    return;   // stop here — wait for the user's answer
                }
            }

            await PersistAsync();
        }
        catch (Exception ex)
        {
            await UiMessageService.Error(ex.Message);
        }
    }

    private async Task ConfirmCreateAsync()
    {
        await _warningModal.Hide();

        try
        {
            await PersistAsync();
        }
        catch (Exception ex)
        {
            await UiMessageService.Error(ex.Message);
        }
    }

    private async Task CancelWarningAsync()
    {
        await _warningModal.Hide();
        // The edit modal stays open — the user can change the name.
    }

    private async Task PersistAsync()
    {
        if (_editingId == null)
        {
            await RoleAppService.CreateAsync(_editing);
        }
        else
        {
            await RoleAppService.UpdateAsync(_editingId.Value, _editing);
        }

        await _modal.Hide();
        await LoadRolesAsync();
    }

    private async Task DeleteAsync(RoleDto role)
    {
        var confirmed = await UiMessageService.Confirm($"Delete role '{role.Name}'?");
        if (!confirmed)
        {
            return;
        }

        await RoleAppService.DeleteAsync(role.Id);
        await LoadRolesAsync();
    }
}