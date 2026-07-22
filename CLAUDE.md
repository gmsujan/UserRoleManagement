# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Solution

ABP Framework 10.4 layered DDD solution on .NET 10, single Blazor Server host. Solution file is `UserRoleManagement.slnx` (slnx format, not .sln).

## Commands

```powershell
dotnet build UserRoleManagement.slnx
dotnet test UserRoleManagement.slnx
dotnet test test/UserRoleManagement.Application.Tests            # one project
dotnet test UserRoleManagement.slnx --filter "FullyQualifiedName~BookAppService_Tests"

abp install-libs                                                  # restore wwwroot client libs (run after clone)
dotnet run --project src/UserRoleManagement.DbMigrator            # apply migrations + seed data
dotnet run --project src/UserRoleManagement.Blazor                # app at https://localhost:44345
```

EF Core migrations are added from the EntityFrameworkCore project with Blazor as startup:

```powershell
dotnet ef migrations add <Name> --project src/UserRoleManagement.EntityFrameworkCore --startup-project src/UserRoleManagement.Blazor
```

Never apply migrations with `dotnet ef database update` — run `DbMigrator`, which also runs the data seed contributors.

Tests run against in-memory SQLite (`UserRoleManagementEntityFrameworkCoreTestModule`), so a real SQL Server is not needed; the dev connection string in `src/UserRoleManagement.Blazor/appsettings.json` points at a local named instance and will differ per machine.

## Two parallel authorization systems

This is the single most important thing to understand before touching auth, menus, or pages.

**1. ABP's stock stack** — ABP Identity + OpenIddict + PermissionManagement. Governs the Books/Authors feature and ABP's own admin pages (Identity, Setting Management, Feature Management). Permissions are declared in `UserRoleManagementPermissionDefinitionProvider` / `UserRoleManagementPermissions`, checked with `[Authorize(...)]` and `.RequirePermissions(...)`.

**2. A hand-rolled "Access" system** — the actual subject of this project. Its own entities, password hashing, cookie scheme, and permission checks, completely independent of ABP Identity:

- Entities in `src/UserRoleManagement.Domain/Access/`: `AppUser`, `AppRole`, `AppPermission`, plus join entities `AppUserRole` and `AppRolePermission` (composite keys). Tables are `App*` prefixed alongside ABP's `AbpUsers`/`AbpRoles`, which still exist and are unrelated.
- Permission codes are plain strings in `AppPermissions` (`Users.View`, `Roles.Edit`, …) stored as rows in `AppPermissions`, **not** ABP permission definitions. They are seeded by `AccessDataSeedContributor`, which also creates the `Administrator` role and the `accessadmin` user.
- Passwords: `PasswordHasher` (PBKDF2-SHA256, format `iterations.salt.hash`). Not ASP.NET Identity's hasher — hashes are not interchangeable.
- Login flow: `Login.razor` (`/access-login`) calls `IAuthAppService.ValidateCredentialsAsync`, then hard-navigates to the `/access-signin` minimal API endpoint (mapped in `UserRoleManagementBlazorModule.OnApplicationInitialization`) because a Blazor circuit cannot write a cookie. That endpoint signs into the `AccessCookie` scheme; `/access-signout` clears it. Middleware in the same method merges the AccessCookie identity into `ctx.User` so both stacks coexist on one principal.
- Reading identity/permissions in UI code: inject `CurrentAccessUser` and `await CurrentUser.IsGrantedAsync(AppPermissions.X)`. Do **not** use ABP's `ICurrentUser` or `IPermissionChecker` for Access features — they resolve against the other system and will return the wrong answer.
- `UserPermissionChecker.GetPermissionCodesAsync` deliberately opens its own unit of work (`requiresNew: true`) because Blazor circuits outlive the HTTP request whose DbContext would otherwise be used. Keep that when editing it.

Page-level guarding is done manually in `OnInitializedAsync` (check `UsersView`, redirect to `/access-login` if denied) and per-button via `_canCreate/_canEdit/_canDelete` flags — see [Users.razor.cs](src/UserRoleManagement.Blazor/Components/Pages/Users.razor.cs). Follow that pattern for new Access pages rather than `[Authorize]`. Menu items are added conditionally in `UserRoleManagementMenuContributor` using the same checker.

## Layering

Standard ABP layout under `src/`: `Domain.Shared` → `Domain` → `Application.Contracts` → `Application` → `HttpApi`/`Blazor`, with `EntityFrameworkCore` implementing persistence and `HttpApi.Client` for remote consumption. App services in `Application` are auto-exposed as REST controllers (`ConfigureAutoApiControllers`) and injected directly into Blazor components in-process via their `I*AppService` interface.

Object mapping uses **Mapperly** (`Volo.Abp.Mapperly`), not AutoMapper — see `UserRoleManagementApplicationMappers.cs` and `UserRoleManagementBlazorMappers.cs`.

`UserRoleManagementDbContext` is registered with `[ReplaceDbContext(typeof(IIdentityDbContext))]`, so ABP Identity and OpenIddict tables live in the same database and migration set as the Access and Books/Authors entities.

UI is Blazor Server with Blazorise (Bootstrap 5) under the LeptonX Lite theme; `_modal`/`UiMessageService` CRUD modals are the established page shape. `BlankLayout.razor` exists for chrome-free pages such as login.

## graphify

This project has a knowledge graph at graphify-out/ with god nodes, community structure, and cross-file relationships.

Rules:
- For codebase questions, first run `graphify query "<question>"` when graphify-out/graph.json exists. Use `graphify path "<A>" "<B>"` for relationships and `graphify explain "<concept>"` for focused concepts. These return a scoped subgraph, usually much smaller than GRAPH_REPORT.md or raw grep output.
- If graphify-out/wiki/index.md exists, use it for broad navigation instead of raw source browsing.
- Read graphify-out/GRAPH_REPORT.md only for broad architecture review or when query/path/explain do not surface enough context.
- After modifying code, run `graphify update .` to keep the graph current (AST-only, no API cost).
