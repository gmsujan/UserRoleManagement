using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using UserRoleManagement.Authors;
using UserRoleManagement.Books;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using UserRoleManagement.Access;
using Volo.Abp.Identity;
using Volo.Abp.OpenIddict.Applications;
using Volo.Abp.OpenIddict.Authorizations;
using Volo.Abp.OpenIddict.Scopes;
using Volo.Abp.OpenIddict.Tokens;

namespace UserRoleManagement.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ConnectionStringName("Default")]
public class UserRoleManagementDbContext :
    AbpDbContext<UserRoleManagementDbContext>,
    IIdentityDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    public DbSet<Author> Authors { get; set; }

    public DbSet<Book> Books { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<AppRole> AppRoles { get; set; }
    public DbSet<AppPermission> AppPermissions { get; set; }
    public DbSet<AppUserRole> AppUserRoles { get; set; }
    public DbSet<AppRolePermission> AppRolePermissions { get; set; }
    public UserRoleManagementDbContext(DbContextOptions<UserRoleManagementDbContext> options)
        : base(options)
    {

    }

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }

    // OpenIddict
    public DbSet<OpenIddictApplication> Applications { get; set; }
    public DbSet<OpenIddictAuthorization> Authorizations { get; set; }
    public DbSet<OpenIddictScope> Scopes { get; set; }
    public DbSet<OpenIddictToken> Tokens { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureFeatureManagement();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureBlobStoring();

        builder.Entity<Author>(b =>
        {
            b.ToTable(UserRoleManagementConsts.DbTablePrefix + "Authors",
                UserRoleManagementConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Name).IsRequired().HasMaxLength(AuthorConsts.MaxNameLength);
            b.Property(x => x.ShortBio).HasMaxLength(AuthorConsts.MaxShortBioLength);
        });

        builder.Entity<Book>(b =>
        {
            b.ToTable(UserRoleManagementConsts.DbTablePrefix + "Books",
                UserRoleManagementConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            b.HasOne<Author>().WithMany().HasForeignKey(x => x.AuthorId).IsRequired();
        });

        // New Entities here

        builder.Entity<AppUser>(b =>
        {
            b.ToTable(UserRoleManagementConsts.DbTablePrefix + "AppUsers",
                      UserRoleManagementConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.UserName).IsRequired().HasMaxLength(64);
            b.Property(x => x.Email).IsRequired().HasMaxLength(128);
            b.Property(x => x.FullName).HasMaxLength(128);
            b.Property(x => x.PasswordHash).IsRequired().HasMaxLength(256);

            b.HasIndex(x => x.UserName).IsUnique();
        });

        builder.Entity<AppRole>(b =>
        {
            b.ToTable(UserRoleManagementConsts.DbTablePrefix + "AppRoles",
                      UserRoleManagementConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.Name).IsRequired().HasMaxLength(64);
            b.Property(x => x.Description).HasMaxLength(256);

            //b.HasIndex(x => x.Name).IsUnique();
            b.HasIndex(x => x.Name)
                .IsUnique()
                .HasFilter("[IsDeleted] = 0");
        });

        builder.Entity<AppPermission>(b =>
        {
            b.ToTable(UserRoleManagementConsts.DbTablePrefix + "AppPermissions",
                      UserRoleManagementConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.Code).IsRequired().HasMaxLength(128);
            b.Property(x => x.DisplayName).IsRequired().HasMaxLength(128);
            b.Property(x => x.Group).HasMaxLength(64);

            b.HasIndex(x => x.Code).IsUnique();
        });

        builder.Entity<AppUserRole>(b =>
        {
            b.ToTable(UserRoleManagementConsts.DbTablePrefix + "AppUserRoles",
                      UserRoleManagementConsts.DbSchema);
            b.ConfigureByConvention();

            // Composite primary key — the PAIR is the identity
            b.HasKey(x => new { x.UserId, x.RoleId });

            b.HasOne(x => x.User)
             .WithMany(u => u.UserRoles)
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.Role)
             .WithMany(r => r.UserRoles)
             .HasForeignKey(x => x.RoleId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<AppRolePermission>(b =>
        {
            b.ToTable(UserRoleManagementConsts.DbTablePrefix + "AppRolePermissions",
                      UserRoleManagementConsts.DbSchema);
            b.ConfigureByConvention();

            b.HasKey(x => new { x.RoleId, x.PermissionId });

            b.HasOne(x => x.Role)
             .WithMany(r => r.RolePermissions)
             .HasForeignKey(x => x.RoleId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.Permission)
             .WithMany()
             .HasForeignKey(x => x.PermissionId)
             .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
