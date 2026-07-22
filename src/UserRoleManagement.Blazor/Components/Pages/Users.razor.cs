using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Components;
using UserRoleManagement.Access;
using UserRoleManagement.Blazor.Access;
using Volo.Abp.Application.Dtos;

namespace UserRoleManagement.Blazor.Components.Pages;

public partial class Users
{
    [Inject] private CurrentAccessUser CurrentUser { get; set; }
    [Inject] private NavigationManager Navigation { get; set; }

    private IReadOnlyList<UserDto> UserList { get; set; } = new List<UserDto>();
    private IReadOnlyList<RoleDto> AllRoles { get; set; } = new List<RoleDto>();

    private int TotalCount { get; set; }
    private const int PageSize = 10;

    private bool _canCreate;
    private bool _canEdit;
    private bool _canDelete;

    private Modal _modal;
    private Guid? _editingId;

    private string _userName = "";
    private string _password = "";
    private string _fullName = "";
    private string _email = "";
    private bool _isActive = true;
    private List<Guid> _roleIds = new();

    protected override async Task OnInitializedAsync()
    {
        if (!await CurrentUser.IsGrantedAsync(AppPermissions.UsersView))
        {
            Navigation.NavigateTo("/access-login");
            return;
        }

        _canCreate = await CurrentUser.IsGrantedAsync(AppPermissions.UsersCreate);
        _canEdit = await CurrentUser.IsGrantedAsync(AppPermissions.UsersEdit);
        _canDelete = await CurrentUser.IsGrantedAsync(AppPermissions.UsersDelete);

        await LoadRolesAsync();
        await LoadUsersAsync();
    }

    private async Task LoadRolesAsync()
    {
        if (!await CurrentUser.IsGrantedAsync(AppPermissions.RolesView))
        {
            AllRoles = new List<RoleDto>();
            return;
        }

        var result = await RoleAppService.GetListAsync(
            new PagedAndSortedResultRequestDto { MaxResultCount = 100, SkipCount = 0 });

        AllRoles = result.Items;
    }

    private async Task LoadUsersAsync()
    {
        var result = await UserAppService.GetListAsync(
            new PagedAndSortedResultRequestDto { MaxResultCount = PageSize, SkipCount = 0 });

        UserList = result.Items;
        TotalCount = (int)result.TotalCount;
    }

    private async Task OpenCreateModalAsync()
    {
        _editingId = null;
        _userName = "";
        _password = "";
        _fullName = "";
        _email = "";
        _isActive = true;
        _roleIds = new List<Guid>();

        await _modal.Show();
    }

    private async Task OpenEditModalAsync(UserDto user)
    {
        _editingId = user.Id;
        _userName = user.UserName;
        _password = "";                    
        _fullName = user.FullName;
        _email = user.Email;
        _isActive = user.IsActive;
        _roleIds = user.RoleIds.ToList();  

        await _modal.Show();
    }

    private async Task CloseModalAsync()
    {
        await _modal.Hide();
    }

    private void ToggleRole(Guid roleId, bool isChecked)
    {
        if (isChecked)
        {
            if (!_roleIds.Contains(roleId))
            {
                _roleIds.Add(roleId);
            }
        }
        else
        {
            _roleIds.Remove(roleId);
        }
    }

    private async Task SaveAsync()
    {
        try
        {
            if (_editingId == null)
            {
                await UserAppService.CreateAsync(new CreateUserDto
                {
                    UserName = _userName,
                    Password = _password,
                    FullName = _fullName,
                    Email = _email,
                    IsActive = _isActive,
                    RoleIds = _roleIds
                });
            }
            else
            {
                await UserAppService.UpdateAsync(_editingId.Value, new UpdateUserDto
                {
                    FullName = _fullName,
                    Email = _email,
                    IsActive = _isActive,
                    RoleIds = _roleIds
                });
            }

            await _modal.Hide();
            await LoadUsersAsync();
        }
        catch (Exception ex)
        {
            await UiMessageService.Error(ex.Message);
        }
    }

    private async Task DeleteAsync(UserDto user)
    {
        var confirmed = await UiMessageService.Confirm($"Delete user '{user.UserName}'?");
        if (!confirmed)
        {
            return;
        }

        try
        {
            await UserAppService.DeleteAsync(user.Id);
            await LoadUsersAsync();
        }
        catch (Exception ex)
        {
            await UiMessageService.Error(ex.Message);   // e.g. "You cannot delete your own account."
        }
    }
}