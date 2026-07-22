# Graph Report - .  (2026-07-22)

## Corpus Check
- 228 files · ~74,939 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 1168 nodes · 1699 edges · 79 communities (71 shown, 8 thin omitted)
- Extraction: 98% EXTRACTED · 2% INFERRED · 0% AMBIGUOUS · INFERRED: 29 edges (avg confidence: 0.85)
- Token cost: 44,708 input · 0 output

## Community Hubs (Navigation)
- Access User App Service
- Access Role App Service
- Author App Service
- Data Seed Contributors
- Books Blazor Page
- Sample Service Tests
- Authors Blazor Page
- Project Documentation Concepts
- Access Permission Checking
- Users Page Markup
- Roles Page Markup
- Blazor Host Dependencies
- DbMigrator Host
- ABP Permission Definitions
- Blazor Module Wiring
- Module Class Graph
- Access Permission Codes
- EF Core DbContext
- Health Check Registration
- Book Service Contracts
- Book App Service
- Blazor Branding And Base
- Console API Client Demo
- DB Migration Service
- HttpApi Client Dependencies
- DbMigrator Dependencies
- Domain Layer Dependencies
- Setting Definition Providers
- EF Core Dependencies
- TestBase Dependencies
- Test Project Dependencies
- EF Core Test Module
- Access User Role Entities
- Domain Shared Dependencies
- Blazor Routing Options
- Book Mapperly Mappings
- Access Entities Migrations
- Application Layer Dependencies
- LeptonX Theme Packages
- Localization And Bundling
- Menu Contributor
- EF Core Test Fixture
- Book Excel Export DTOs
- Book App Service Tests
- Access Auth State Provider
- Domain Module And Multitenancy
- EF Core Module Mappings
- Access Login Page
- Access Permission Entities
- HttpApi Dependencies
- Domain Shared Module
- Book DTO Mapper
- Fake Principal Accessor
- ABP Permission Provider
- TestBase Module Seeding
- Design Time DbContext Factory
- DTO Extension Config
- Module Extension Configurator
- DbContext Model Snapshot
- Blazor Index Page
- Filtered Unique Index Migration
- HttpApi Client Module
- HttpApi Test Model
- Page Layout Component
- Initial Migration Designer
- Access Migration Designer
- Index Migration Designer
- Test Constants
- Graphify Workflow

## God Nodes (most connected - your core abstractions)
1. `UserRoleManagement.Access` - 31 edges
2. `UserRoleManagementDbContext` - 28 edges
3. `System.Collections.Generic` - 27 edges
4. `Roles` - 24 edges
5. `UserRoleManagement` - 22 edges
6. `Users` - 21 edges
7. `UserAppService` - 19 edges
8. `UserDto` - 18 edges
9. `RoleDto` - 17 edges
10. `RoleAppService` - 17 edges

## Surprising Connections (you probably didn't know these)
- `Layered DDD Startup Solution` --semantically_similar_to--> `ABP Layered Architecture (Domain.Shared → Domain → Application.Contracts → Application → HttpApi/Blazor)`  [INFERRED] [semantically similar]
  README.md → CLAUDE.md
- `OpenIddict Signing Certificate Setup` --conceptually_related_to--> `ABP Stock Auth Stack (Identity + OpenIddict + PermissionManagement)`  [INFERRED]
  README.md → CLAUDE.md
- `BookAppService_Tests` --references--> `IBookAppService`  [EXTRACTED]
  test/UserRoleManagement.Application.Tests/Books/BookAppService_Tests .cs → src/UserRoleManagement.Application.Contracts/Books/IBookAppService.cs
- `UserRoleManagement.Blazor application` --conceptually_related_to--> `Blazor Server + Blazorise + LeptonX Lite UI stack`  [INFERRED]
  README.md → CLAUDE.md
- `UserRoleManagement.DbMigrator console application` --conceptually_related_to--> `DbMigrator Migration Workflow`  [INFERRED]
  README.md → CLAUDE.md

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Access System Login/Authentication Flow** — claude_login_razor, claude_iauthappservice, claude_passwordhasher, claude_accesscookie_scheme, claude_userrolemanagementblazormodule, claude_currentaccessuser [EXTRACTED 1.00]
- **Access Domain Entity Model (users, roles, permissions and joins)** — claude_appuser, claude_approle, claude_apppermission, claude_appuserrole, claude_approlepermission, claude_accessdataseedcontributor [EXTRACTED 1.00]
- **Access Authorization Enforcement Points** — claude_manual_page_guarding, claude_userrolemanagementmenucontributor, claude_userpermissionchecker, claude_users_razor_cs, claude_apppermissions [INFERRED 0.95]

## Communities (79 total, 8 thin omitted)

### Community 0 - "Access User App Service"
Cohesion: 0.06
Nodes (38): IHttpContextAccessor, Guid, Task, CurrentAccessUserAccessor, Guid, IRepository, List, PagedAndSortedResultRequestDto (+30 more)

### Community 1 - "Access Role App Service"
Cohesion: 0.07
Nodes (34): EntityDto, IDataFilter, Guid, IRepository, List, PagedAndSortedResultRequestDto, PagedResultDto, Task (+26 more)

### Community 2 - "Author App Service"
Cohesion: 0.06
Nodes (37): UserRoleManagement.Authors, FullAuditedEntityDto, ICrudAppService, MapperBase, AllowAnonymous, Authorize, Guid, IDistributedCache (+29 more)

### Community 3 - "Data Seed Contributors"
Cohesion: 0.05
Nodes (33): UserRoleManagement.OpenIddict, UserRoleManagement.Data, HashAlgorithmName, IDataSeedContributor, IGuidGenerator, IServiceProvider, ITransientDependency, OpenIddictDataSeedContributorBase (+25 more)

### Community 4 - "Books Blazor Page"
Cohesion: 0.04
Nodes (48): AbpCrudPageBase<IBookAppService, NumericInput, SelectItem, ExportBooksAsync, OnInitializedAsync, AbpBlazorMessageLocalizerHelper<UserRoleManagementResource>, AuthorDto, Card (+40 more)

### Community 5 - "Sample Service Tests"
Cohesion: 0.04
Nodes (33): AbpApplicationCreationOptions, AbpIntegratedTest, AbpUnitOfWorkOptions, UserRoleManagement.EntityFrameworkCore.Applications, UserRoleManagement.EntityFrameworkCore.Domains, UserRoleManagement.Samples, UserRoleManagement.EntityFrameworkCore.Samples, Func (+25 more)

### Community 6 - "Authors Blazor Page"
Cohesion: 0.05
Nodes (43): AbpCrudPageBase<IAuthorAppService, MemoInput, ExportAuthorsAsync, AbpBlazorMessageLocalizerHelper<UserRoleManagementResource>, Card, CardBody, CardHeader, CardTitle (+35 more)

### Community 7 - "Project Documentation Concepts"
Cohesion: 0.08
Nodes (38): ABP Layered Architecture (Domain.Shared → Domain → Application.Contracts → Application → HttpApi/Blazor), ABP Stock Auth Stack (Identity + OpenIddict + PermissionManagement), Access Login Flow (/access-login → /access-signin), Hand-rolled Access System, AccessCookie authentication scheme, AccessDataSeedContributor, AppPermission entity, AppPermissions (permission code constants) (+30 more)

### Community 8 - "Access Permission Checking"
Cohesion: 0.08
Nodes (23): ApplicationService, IApplicationService, IScopedDependency, IUnitOfWorkManager, IRepository, Task, AuthAppService, Task (+15 more)

### Community 9 - "Users Page Markup"
Cohesion: 0.07
Nodes (27): Blazorise, Card, CardBody, CardHeader, CloseButton, Column, DataGrid, DataGridColumn (+19 more)

### Community 10 - "Roles Page Markup"
Cohesion: 0.07
Nodes (26): Blazorise, Card, CardBody, CardHeader, CloseButton, Column, DataGrid, DataGridColumn (+18 more)

### Community 11 - "Blazor Host Dependencies"
Cohesion: 0.08
Nodes (24): AspNetCore.HealthChecks.UI (9.0.0), AspNetCore.HealthChecks.UI.Client (9.0.0), AspNetCore.HealthChecks.UI.InMemory.Storage (9.0.0), Blazorise.Bootstrap5 (2.0.4), Blazorise.Icons.FontAwesome (2.0.4), IdentityModel (7.0.0), KubernetesClient (18.0.5), Microsoft.AspNetCore.Components.WebAssembly.Server (10.0.7) (+16 more)

### Community 12 - "DbMigrator Host"
Cohesion: 0.10
Nodes (14): UserRoleManagement.DbMigrator, IHostApplicationLifetime, IHostBuilder, IHostedService, Microsoft.Extensions.Hosting, Task, Program, CancellationToken (+6 more)

### Community 13 - "ABP Permission Definitions"
Cohesion: 0.10
Nodes (18): Blazorise.Components, Blazorise.DataGrid, UserRoleManagement.Permissions, Microsoft.AspNetCore.Authorization, Microsoft.AspNetCore.Components.Forms, Microsoft.AspNetCore.Components.Routing, Microsoft.AspNetCore.Components.Web, Microsoft.AspNetCore.Components.Web.Virtualization (+10 more)

### Community 14 - "Blazor Module Wiring"
Cohesion: 0.18
Nodes (7): IWebHostEnvironment, IHostEnvironment, ApplicationInitializationContext, IConfiguration, IServiceCollection, ServiceConfigurationContext, UserRoleManagementBlazorModule

### Community 15 - "Module Class Graph"
Cohesion: 0.13
Nodes (11): AbpModule, UserRoleManagement, UserRoleManagementApplicationContractsModule, UserRoleManagementApplicationModule, UserRoleManagementDomainErrorCodes, string, UserRoleManagementConsts, ServiceConfigurationContext (+3 more)

### Community 16 - "Access Permission Codes"
Cohesion: 0.18
Nodes (5): UserRoleManagement.Blazor.Components.Pages, UserRoleManagement.Access, string, AppPermissions, System.Collections.Generic

### Community 17 - "EF Core DbContext"
Cohesion: 0.12
Nodes (17): AbpDbContext, DbSet, IdentityClaimType, IdentityLinkUser, IdentityRole, IdentitySecurityLog, IdentitySession, IdentityUser (+9 more)

### Community 18 - "Health Check Registration"
Cohesion: 0.12
Nodes (12): Action, UserRoleManagement.Blazor.HealthChecks, HealthCheckContext, HealthCheckResult, IHealthCheck, IIdentityRoleRepository, Options, IServiceCollection (+4 more)

### Community 19 - "Book Service Contracts"
Cohesion: 0.13
Nodes (10): UserRoleManagement.Shared, AllowAnonymous, IRemoteStreamContent, BookExcelDownloadDto, Guid, IRemoteStreamContent, PagedAndSortedResultRequestDto, Task (+2 more)

### Community 20 - "Book App Service"
Cohesion: 0.23
Nodes (9): IReadOnlyCollection, Authorize, Guid, IDistributedCache, IRepository, PagedAndSortedResultRequestDto, PagedResultDto, Task (+1 more)

### Community 21 - "Blazor Branding And Base"
Cohesion: 0.14
Nodes (10): AbpComponentBase, AbpControllerBase, UserRoleManagement.Controllers, UserRoleManagement.Localization, UserRoleManagement.Blazor, DefaultBrandingProvider, IStringLocalizer, UserRoleManagementBrandingProvider (+2 more)

### Community 22 - "Console API Client Demo"
Cohesion: 0.13
Nodes (9): UserRoleManagement.HttpApi.Client.ConsoleTestApp, IProfileAppService, IIdentityUserAppService, Task, ClientDemoService, Task, Program, ServiceConfigurationContext (+1 more)

### Community 23 - "DB Migration Service"
Cohesion: 0.25
Nodes (6): IDataSeeder, IEnumerable, ILogger, ICurrentTenant, Task, UserRoleManagementDbMigrationService

### Community 24 - "HttpApi Client Dependencies"
Cohesion: 0.13
Nodes (13): Microsoft.Extensions.Http.Polly (10.0.7), Volo.Abp.Account.HttpApi.Client (10.4.1), Volo.Abp.FeatureManagement.HttpApi.Client (10.4.1), Volo.Abp.Http.Client.IdentityModel (10.4.1), Volo.Abp.Identity.HttpApi.Client (10.4.1), Volo.Abp.PermissionManagement.HttpApi.Client (10.4.1), Volo.Abp.SettingManagement.HttpApi.Client (10.4.1), net10.0 (+5 more)

### Community 25 - "DbMigrator Dependencies"
Cohesion: 0.13
Nodes (13): Volo.Abp.Account.Application.Contracts (10.4.1), Volo.Abp.FeatureManagement.Application.Contracts (10.4.1), Volo.Abp.Identity.Application.Contracts (10.4.1), Volo.Abp.PermissionManagement.Application.Contracts (10.4.1), Volo.Abp.SettingManagement.Application.Contracts (10.4.1), net10.0, Microsoft.NET.Sdk, net10.0 (+5 more)

### Community 26 - "Domain Layer Dependencies"
Cohesion: 0.13
Nodes (14): Volo.Abp.AuditLogging.Domain (10.4.1), Volo.Abp.BackgroundJobs.Domain (10.4.1), Volo.Abp.BlobStoring.Database.Domain (10.4.1), Volo.Abp.Caching (10.4.1), Volo.Abp.Emailing (10.4.1), Volo.Abp.FeatureManagement.Domain (10.4.1), Volo.Abp.Identity.Domain (10.4.1), Volo.Abp.OpenIddict.Domain (10.4.1) (+6 more)

### Community 27 - "Setting Definition Providers"
Cohesion: 0.14
Nodes (9): UserRoleManagement.Settings, UserRoleManagement.Identity, SettingDefinitionProvider, ISettingDefinitionContext, ChangeIdentityPasswordPolicySettingDefinitionProvider, ISettingDefinitionContext, UserRoleManagementSettingDefinitionProvider, string (+1 more)

### Community 28 - "EF Core Dependencies"
Cohesion: 0.14
Nodes (13): Microsoft.EntityFrameworkCore.Design (10.0.7), Microsoft.EntityFrameworkCore.Tools (10.0.7), Volo.Abp.AuditLogging.EntityFrameworkCore (10.4.1), Volo.Abp.BackgroundJobs.EntityFrameworkCore (10.4.1), Volo.Abp.BlobStoring.Database.EntityFrameworkCore (10.4.1), Volo.Abp.EntityFrameworkCore.SqlServer (10.4.1), Volo.Abp.FeatureManagement.EntityFrameworkCore (10.4.1), Volo.Abp.Identity.EntityFrameworkCore (10.4.1) (+5 more)

### Community 29 - "TestBase Dependencies"
Cohesion: 0.14
Nodes (13): NSubstitute (5.3.0), NSubstitute.Analyzers.CSharp (1.0.17), Shouldly (4.3.0), Volo.Abp.Authorization (10.4.1), Volo.Abp.BackgroundJobs.Abstractions (10.4.1), Volo.Abp.TestBase (10.4.1), xunit (2.9.3), xunit.extensibility.execution (2.9.3) (+5 more)

### Community 30 - "Test Project Dependencies"
Cohesion: 0.16
Nodes (10): Volo.Abp.EntityFrameworkCore.Sqlite (10.4.1), net10.0, Microsoft.NET.Test.Sdk (17.14.1), Microsoft.NET.Sdk, net10.0, Microsoft.NET.Test.Sdk (17.14.1), Microsoft.NET.Sdk, net10.0 (+2 more)

### Community 31 - "EF Core Test Module"
Cohesion: 0.21
Nodes (7): ApplicationShutdownContext, SqliteConnection, UserRoleManagementEntityFrameworkCoreTestBase, IServiceCollection, ServiceConfigurationContext, UserRoleManagementEntityFrameworkCoreTestModule, UserRoleManagementTestBase

### Community 32 - "Access User Role Entities"
Cohesion: 0.17
Nodes (9): FullAuditedAggregateRoot, Guid, ICollection, AppRole, Guid, ICollection, AppUser, Guid (+1 more)

### Community 33 - "Domain Shared Dependencies"
Cohesion: 0.15
Nodes (12): Microsoft.Extensions.FileProviders.Embedded (10.0.7), Volo.Abp.AuditLogging.Domain.Shared (10.4.1), Volo.Abp.BackgroundJobs.Domain.Shared (10.4.1), Volo.Abp.BlobStoring.Database.Domain.Shared (10.4.1), Volo.Abp.FeatureManagement.Domain.Shared (10.4.1), Volo.Abp.GlobalFeatures (10.4.1), Volo.Abp.Identity.Domain.Shared (10.4.1), Volo.Abp.OpenIddict.Domain.Shared (10.4.1) (+4 more)

### Community 34 - "Blazor Routing Options"
Cohesion: 0.18
Nodes (10): AbpRouterOptions, AuthorizeRouteView, Found, IOptions<AbpRouterOptions>, Microsoft.Extensions.Options, NotAuthorized, RedirectToLogin, Router (+2 more)

### Community 35 - "Book Mapperly Mappings"
Cohesion: 0.21
Nodes (9): AuditedAggregateRoot, DateTime, Guid, CreateUpdateBookDto, UserRoleManagementCreateUpdateBookDtoToBookMapper, UserRoleManagementBlazorMappers, DateTime, Guid (+1 more)

### Community 36 - "Access Entities Migrations"
Cohesion: 0.21
Nodes (6): UserRoleManagement.Migrations, Migration, MigrationBuilder, Initial, MigrationBuilder, Added_Access_Entities

### Community 37 - "Application Layer Dependencies"
Cohesion: 0.17
Nodes (11): Microsoft.AspNetCore.Authentication.Abstractions (2.3.11), Microsoft.AspNetCore.Http.Abstractions (2.3.11), MiniExcel (1.41.4), Volo.Abp.Account.Application (10.4.1), Volo.Abp.FeatureManagement.Application (10.4.1), Volo.Abp.Identity.Application (10.4.1), Volo.Abp.PermissionManagement.Application (10.4.1), Volo.Abp.SettingManagement.Application (10.4.1) (+3 more)

### Community 38 - "LeptonX Theme Packages"
Cohesion: 0.18
Nodes (10): @abp/aspnetcore.components.server.leptonxlitetheme, @abp/aspnetcore.mvc.ui.theme.leptonxlite, dependencies, @abp/aspnetcore.components.server.leptonxlitetheme, @abp/aspnetcore.mvc.ui.theme.leptonxlite, name, private, resolutions (+2 more)

### Community 39 - "Localization And Bundling"
Cohesion: 0.18
Nodes (8): AbpScripts, AbpStyles, HeadOutlet, Routes, UserRoleManagementResource, Volo.Abp.AspNetCore.Components.Server.LeptonXLiteTheme.Bundling, Volo.Abp.AspNetCore.Components.Web.Theming.Bundling, Volo.Abp.Localization

### Community 40 - "Menu Contributor"
Cohesion: 0.24
Nodes (7): UserRoleManagement.Blazor.Menus, IMenuContributor, MenuConfigurationContext, Task, UserRoleManagementMenuContributor, string, UserRoleManagementMenus

### Community 41 - "EF Core Test Fixture"
Cohesion: 0.24
Nodes (6): UserRoleManagement.EntityFrameworkCore, ICollectionFixture, IDisposable, UserRoleManagementEntityFrameworkCoreCollection, UserRoleManagementEntityFrameworkCoreCollectionFixtureBase, UserRoleManagementEntityFrameworkCoreFixture

### Community 42 - "Book Excel Export DTOs"
Cohesion: 0.20
Nodes (5): UserRoleManagement.Books, BookExcelDownloadTokenCacheItem, DateTime, BookExcelDto, BookType

### Community 43 - "Book App Service Tests"
Cohesion: 0.29
Nodes (6): UserRoleManagement.EntityFrameworkCore.Applications.Books, Fact, Task, BookAppService_Tests, UserRoleManagementEntityFrameworkCoreTestModule, EfCoreBookAppService_Tests

### Community 44 - "Access Auth State Provider"
Cohesion: 0.28
Nodes (6): AuthenticationState, AuthenticationStateProvider, UserRoleManagement.Blazor.Access, Microsoft.AspNetCore.Components.Authorization, Task, AccessAuthenticationStateProvider

### Community 45 - "Domain Module And Multitenancy"
Cohesion: 0.22
Nodes (5): UserRoleManagement.MultiTenancy, bool, MultiTenancyConsts, ServiceConfigurationContext, UserRoleManagementDomainModule

### Community 46 - "EF Core Module Mappings"
Cohesion: 0.25
Nodes (4): OneTimeRunner, UserRoleManagementEfCoreEntityExtensionMappings, ServiceConfigurationContext, UserRoleManagementEntityFrameworkCoreModule

### Community 47 - "Access Login Page"
Cohesion: 0.25
Nodes (7): Alert, Blazorise, IAuthAppService, NavigationManager, TextInput, UserRoleManagement.Access, SubmitAsync

### Community 48 - "Access Permission Entities"
Cohesion: 0.29
Nodes (5): Entity, Guid, AppPermission, Guid, AppRolePermission

### Community 49 - "HttpApi Dependencies"
Cohesion: 0.25
Nodes (7): Volo.Abp.Account.HttpApi (10.4.1), Volo.Abp.FeatureManagement.HttpApi (10.4.1), Volo.Abp.Identity.HttpApi (10.4.1), Volo.Abp.PermissionManagement.HttpApi (10.4.1), Volo.Abp.SettingManagement.HttpApi (10.4.1), net10.0, Microsoft.NET.Sdk

### Community 50 - "Domain Shared Module"
Cohesion: 0.29
Nodes (4): ServiceConfigurationContext, UserRoleManagementDomainSharedModule, OneTimeRunner, UserRoleManagementGlobalFeatureConfigurator

### Community 51 - "Book DTO Mapper"
Cohesion: 0.33
Nodes (6): AuditedEntityDto, MapperIgnoreTarget, DateTime, Guid, BookDto, UserRoleManagementBookToBookDtoMapper

### Community 52 - "Fake Principal Accessor"
Cohesion: 0.38
Nodes (4): ClaimsPrincipal, UserRoleManagement.Security, FakeCurrentPrincipalAccessor, ThreadCurrentPrincipalAccessor

### Community 53 - "ABP Permission Provider"
Cohesion: 0.33
Nodes (4): IPermissionDefinitionContext, LocalizableString, PermissionDefinitionProvider, UserRoleManagementPermissionDefinitionProvider

### Community 54 - "TestBase Module Seeding"
Cohesion: 0.38
Nodes (3): ApplicationInitializationContext, ServiceConfigurationContext, UserRoleManagementTestBaseModule

### Community 55 - "Design Time DbContext Factory"
Cohesion: 0.40
Nodes (3): IConfigurationRoot, IDesignTimeDbContextFactory, UserRoleManagementDbContextFactory

### Community 56 - "DTO Extension Config"
Cohesion: 0.33
Nodes (3): ServiceConfigurationContext, OneTimeRunner, UserRoleManagementDtoExtensions

### Community 58 - "DbContext Model Snapshot"
Cohesion: 0.40
Nodes (3): ModelSnapshot, ModelBuilder, UserRoleManagementDbContextModelSnapshot

### Community 61 - "HttpApi Client Module"
Cohesion: 0.40
Nodes (3): ServiceConfigurationContext, string, UserRoleManagementHttpApiClientModule

### Community 62 - "HttpApi Test Model"
Cohesion: 0.50
Nodes (3): UserRoleManagement.Models.Test, DateTime, TestModel

### Community 63 - "Page Layout Component"
Cohesion: 0.50
Nodes (3): PageLayout, UserRoleManagementComponentBase, Volo.Abp.AspNetCore.Components.Web.Theming.Layout

## Knowledge Gaps
- **331 isolated node(s):** `net10.0`, `Volo.Abp.PermissionManagement.Application.Contracts (10.4.1)`, `Volo.Abp.FeatureManagement.Application.Contracts (10.4.1)`, `Volo.Abp.SettingManagement.Application.Contracts (10.4.1)`, `Volo.Abp.Identity.Application.Contracts (10.4.1)` (+326 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **8 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `System.Collections.Generic` connect `Access Permission Codes` to `Access User Role Entities`, `Blazor Routing Options`, `Data Seed Contributors`, `Books Blazor Page`, `ABP Permission Definitions`, `Domain Module And Multitenancy`, `Access Permission Entities`, `Fake Principal Accessor`, `Blazor Index Page`?**
  _High betweenness centrality (0.132) - this node is a cross-community bridge._
- **Why does `UserRoleManagement` connect `Module Class Graph` to `Author App Service`, `Data Seed Contributors`, `Test Constants`, `Sample Service Tests`, `Localization And Bundling`, `Domain Module And Multitenancy`, `Domain Shared Module`, `Blazor Branding And Base`, `TestBase Module Seeding`, `DTO Extension Config`, `Module Extension Configurator`, `HttpApi Client Module`?**
  _High betweenness centrality (0.073) - this node is a cross-community bridge._
- **Why does `UserRoleManagement.EntityFrameworkCore` connect `EF Core Test Fixture` to `Initial Migration Designer`, `Access Migration Designer`, `Author App Service`, `Blazor Routing Options`, `Data Seed Contributors`, `Index Migration Designer`, `DbMigrator Host`, `EF Core Module Mappings`, `Design Time DbContext Factory`, `DbContext Model Snapshot`, `EF Core Test Module`?**
  _High betweenness centrality (0.068) - this node is a cross-community bridge._
- **What connects `net10.0`, `Volo.Abp.PermissionManagement.Application.Contracts (10.4.1)`, `Volo.Abp.FeatureManagement.Application.Contracts (10.4.1)` to the rest of the system?**
  _331 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Access User App Service` be split into smaller, more focused modules?**
  _Cohesion score 0.0636523266022827 - nodes in this community are weakly interconnected._
- **Should `Access Role App Service` be split into smaller, more focused modules?**
  _Cohesion score 0.06597222222222222 - nodes in this community are weakly interconnected._
- **Should `Author App Service` be split into smaller, more focused modules?**
  _Cohesion score 0.056107539450613676 - nodes in this community are weakly interconnected._